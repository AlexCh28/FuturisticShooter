using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FPSInput : NetworkBehaviour
{
    private float _horizontalDirection;
    private float _verticalDirection;

    public float HorizontalDirection => _horizontalDirection;
    public float VerticalDirection => _verticalDirection;
    
    public KeyCode runningKey = KeyCode.LeftShift;

    public bool runningKeyIsPressed => Input.GetKey(runningKey);

    private void Update() {
        if (!IsOwner) return;

        _horizontalDirection = Input.GetAxis("Horizontal");
        _verticalDirection = Input.GetAxis("Vertical");
    }
}
