using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FPSAnimations : NetworkBehaviour
{
    private Animator _animatorArmature;
    private FPSInput _input;

    private bool IsWalking = false;
    private bool IsRunning = false;

    private void Awake() {
        _input = GetComponent<FPSInput>();
        _animatorArmature = GetComponentInChildren<Animator>();
    }

    private void Update() {
        if (!IsOwner) return;

        IsWalking = _input.VerticalDirection!=0 || _input.HorizontalDirection!=0;
        IsRunning = IsWalking && _input.runningKeyIsPressed; 
    }

    private void LateUpdate() {
        if (!IsOwner) return;

        _animatorArmature.SetBool("IsWalk", IsWalking);
        _animatorArmature.SetBool("IsRun", IsRunning);
    }
}
