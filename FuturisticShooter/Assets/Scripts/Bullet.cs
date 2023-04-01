using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 5;

    void FixedUpdate()
    {
        transform.Translate(0,0,speed*Time.fixedDeltaTime);
    }
}
