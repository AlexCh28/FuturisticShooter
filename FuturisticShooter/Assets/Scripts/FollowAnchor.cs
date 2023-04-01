using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FollowAnchor : NetworkBehaviour
{
    [SerializeField] Transform anchor;

    [SerializeField] bool followPosition = true;
    [SerializeField] bool followRotation = true;

    Vector3 cachedPosition;
    Quaternion cachedRotation;

    public override void OnNetworkSpawn(){
        if (!anchor) return;
        AdjustPosition();
        AdjustRotation();
    }

    void Update()
    {
        if (!anchor) return;

        if (followPosition && cachedPosition != anchor.position) AdjustPosition();   

        if (followRotation && cachedRotation != anchor.rotation) AdjustRotation();   
    }

    private void AdjustPosition(){
        transform.position = anchor.position;
        cachedPosition = transform.position;
    }
    private void AdjustRotation(){
        transform.rotation = anchor.rotation;
        cachedRotation = transform.rotation;
    }
}
