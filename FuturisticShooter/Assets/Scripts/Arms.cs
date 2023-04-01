using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using DG.Tweening;

public class Arms : NetworkBehaviour
{
    // [Header("Test section")]
    // [SerializeField] GameObject blaster;
    // [SerializeField] Transform blasterTargetL, blasterTargetR;

    [Header("Idle animation:")]
    [SerializeField] Transform anchorL;
    [SerializeField] Transform anchorR;
    [SerializeField] float amplitudeModifierL = 0.3f, amplitudeModifierR = 0.3f;
    [SerializeField] float animationSpeedL = 1f, animationSpeedR = 1f;
    [SerializeField] float animationOffsetL = 0f, animationOffsetR = 0f;


    float leftStartY = 0, rightStartY = 0;
    float leftNewY = 0, rightNewY = 0;

    float currentAnimationSpeedL = 0, currentAnimationSpeedR = 0;

    bool wantToChangeWeapon = false;
    bool holdingGun = false;

    public override void OnNetworkSpawn(){
        leftStartY = anchorL.localPosition.y;
        rightStartY = anchorR.localPosition.y;
    }

    private void Update() {
        // if (Input.GetKeyDown(KeyCode.T) && !wantToChangeWeapon) wantToChangeWeapon = true;
        if (FirstPersonMovement.RunMode) {
            currentAnimationSpeedL = 4*animationSpeedL;
            currentAnimationSpeedR = 4*animationSpeedR;
        }
        else if (FirstPersonMovement.IsWalking){
            currentAnimationSpeedL = 2*animationSpeedL;
            currentAnimationSpeedR = 2*animationSpeedR;
        }
        else if (!FirstPersonMovement.IsWalking && !FirstPersonMovement.RunMode){
            currentAnimationSpeedL = animationSpeedL;
            currentAnimationSpeedR = animationSpeedR;
        }
        print(currentAnimationSpeedL);
        print(currentAnimationSpeedR);
        if (!wantToChangeWeapon && !holdingGun){
            leftNewY = leftStartY + amplitudeModifierL*Mathf.Sin(Time.time*currentAnimationSpeedL+animationOffsetL);
            anchorL.localPosition = new Vector3(anchorL.localPosition.x, leftNewY, anchorL.localPosition.z);

            rightNewY = rightStartY + amplitudeModifierR*Mathf.Sin(Time.time*currentAnimationSpeedR+animationOffsetR);
            anchorR.localPosition = new Vector3(anchorR.localPosition.x, rightNewY, anchorR.localPosition.z);
        }
        // if(wantToChangeWeapon && !holdingGun){
        //     anchorL.DOMove(blasterTargetL.position, 0.01f);
        //     anchorR.DOMove(blasterTargetR.position, 0.01f).OnComplete(()=>{
        //         blaster.gameObject.GetComponent<MeshRenderer>().enabled=true;
        //         holdingGun=true;
        //         wantToChangeWeapon=false;
        //     });
        // }
        // if(holdingGun){
        //     anchorL.position = blasterTargetL.position;
        //     anchorR.position = blasterTargetR.position;
        // }
    }
}
