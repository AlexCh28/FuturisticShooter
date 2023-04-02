using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FirstPersonMovement : NetworkBehaviour
{
    private Rigidbody _rigidbody;
    private FPSInput _input;

    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set;}
    public float runSpeed = 9;
    
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        _input = GetComponent<FPSInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;
        
        IsRunning = canRun && _input.runningKeyIsPressed;
        float targetMovingSpeed = IsRunning ? runSpeed : speed;

        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity =new Vector2( _input.HorizontalDirection * targetMovingSpeed, _input.VerticalDirection * targetMovingSpeed);

        _rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);
    }
}