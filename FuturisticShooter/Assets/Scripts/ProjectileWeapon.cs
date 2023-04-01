using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] GameObject bulletPrefab;

    public override void DoShot(){
        Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
    }
}
