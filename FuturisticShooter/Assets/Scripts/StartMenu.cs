using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button StartHostButton, StartClientButton;
    [SerializeField] GameObject StartScreenCamera;

    private void Awake() {
        StartHostButton.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
            StartScreenCamera.SetActive(false);
        });
        StartClientButton.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
            StartScreenCamera.SetActive(false);
        });
    }
}
