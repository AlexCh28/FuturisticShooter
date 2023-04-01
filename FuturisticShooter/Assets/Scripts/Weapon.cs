using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using DG.Tweening;

public class Weapon : NetworkBehaviour
{
    [SerializeField] Vector3 punchPosition = new Vector3(0,0,-0.5f);
    [SerializeField] Vector3 punchRotation = new Vector3(50,0,0);
    [SerializeField] float duration=1,elasticity=1;
    [SerializeField] int vibrato=1;

    [SerializeField] float aimDuration = 0.3f;

    [SerializeField] Camera playerMainCamera;
    [SerializeField] Camera playerWeaponCamera;
    [SerializeField] float aimFieldOfView = 50;

    Vector3 startLocalEulerAngles;
    float startLocalPositionX=0, startFieldOfView=0;

    protected bool canShoot = true;
    protected bool isShooting = false;

    protected bool canChangeAimMode = true;
    protected bool isInAimMode = false;

    bool wantToShoot=false, wantToAim=false;
    
    public override void OnNetworkSpawn() {
        startLocalPositionX = transform.localPosition.x;
        startLocalEulerAngles = transform.localEulerAngles;
        startFieldOfView = playerMainCamera.fieldOfView;
        gameObject.GetComponent<MeshRenderer>().enabled=false;
    }
    
    void Update(){
        if (!IsOwner) return;
        if (Input.GetMouseButtonDown(0)) wantToShoot=true;
        if (Input.GetMouseButtonDown(1)) wantToAim=true;
        if (!Input.GetMouseButton(1)) wantToAim=false;
    }

    void FixedUpdate()
    {
        if (wantToShoot) {DoShot();wantToShoot=false;}
        if (wantToAim) AimModeOnServerRpc();
        if (!wantToAim) AimModeOffServerRpc();
    }

    [ServerRpc]
    public virtual void DoShotServerRpc(){DoShot();}
    [ServerRpc]
    private void AimModeOnServerRpc(){AimModeOn();}
    [ServerRpc]
    private void AimModeOffServerRpc(){AimModeOff();}

    public virtual void DoShot(){
        canShoot = false;
        isShooting = true;
        canChangeAimMode = false;
        transform.DOPunchPosition(punchPosition,duration,vibrato,elasticity,false).OnComplete(()=>{canShoot = true;isShooting = false;canChangeAimMode = true;});
        transform.DOPunchRotation(punchRotation,duration,vibrato,elasticity);
    }

    public virtual void AimModeOn(){
        if (!canChangeAimMode || isShooting || isInAimMode) return;
        canShoot = false;
        isInAimMode = true;
        playerMainCamera.DOFieldOfView(aimFieldOfView,aimDuration);
        playerWeaponCamera.DOFieldOfView(aimFieldOfView,aimDuration);
        transform.DOLocalMoveX(0,aimDuration).OnComplete(()=>canShoot=true);
        transform.DOLocalRotate(new Vector3(startLocalEulerAngles.x,180,startLocalEulerAngles.z), aimDuration);
    }

    public virtual void AimModeOff(){
        if (!canChangeAimMode || isShooting || !isInAimMode) return;
        canShoot = false;
        isInAimMode = false;
        playerMainCamera.DOFieldOfView(startFieldOfView,aimDuration);
        playerWeaponCamera.DOFieldOfView(startFieldOfView,aimDuration);
        transform.DOLocalMoveX(startLocalPositionX,aimDuration).OnComplete(()=>{canShoot=true;});
        transform.DOLocalRotate(startLocalEulerAngles, aimDuration);
    }
}
