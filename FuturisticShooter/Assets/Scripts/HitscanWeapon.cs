using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HitscanWeapon : Weapon
{
    [SerializeField] GameObject bulletHolePrefab;
    Camera camera;
    GameObject newBulletHole;


    private void Start() {
        camera = transform.parent.gameObject.GetComponent<Camera>();
    }


    public override void DoShot(){
        if (!canShoot || isShooting) return;
        
        base.DoShot();

        ShotRaycastServerRpc();
    }

    [ServerRpc]
    private void ShotRaycastServerRpc(){
        Vector3 point = new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0);
        //Ray ray = camera.ScreenPointToRay(point);
        //Debug.DrawRay(camera.ScreenToWorldPoint(point), camera.gameObject.transform.forward*100, Color.green, 30);
        Ray ray = new Ray(camera.ScreenToWorldPoint(point), camera.gameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            newBulletHole = Instantiate(bulletHolePrefab, hit.point+(hit.normal*0.01f), Quaternion.FromToRotation(Vector3.up, hit.normal));
            newBulletHole.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
