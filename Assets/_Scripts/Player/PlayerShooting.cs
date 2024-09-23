using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

public class PlayerShooting : MonoBehaviour
{
    GameObject bulletObj;
    public GameObject turret;
    public float speed;
    private Vector3 direction;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
           Shoot();
    }

    void Shoot()
    {
        Vector3 direction = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Bullet bullet = Instantiate(bulletObj, transform).GetComponent<Bullet>();
        bullet.transform.up = bullet.transform.position + new Vector3(direction.x, direction.y);
        bullet.speed = speed;
        bullet.damage = activeWeaponStats.bulletDamage;
    }
}
