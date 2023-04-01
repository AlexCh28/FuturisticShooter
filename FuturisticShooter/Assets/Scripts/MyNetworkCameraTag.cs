using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MyNetworkCameraTag : NetworkBehaviour
{
    public override void OnNetworkSpawn(){
        if (IsOwner) return;
        GetComponent<Camera>().enabled = false;
        print("hello"); 
    }
}
