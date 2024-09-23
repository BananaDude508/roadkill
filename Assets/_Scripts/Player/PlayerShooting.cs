using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

public class PlayerShooting : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletObj;
    public Transform turret;
    public Transform spawnPos;
    public float speed;
    public float shootOffset;
    private Vector3 mousePos;


    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        turret.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
            Shoot(angle);
    }

    void Shoot(float angle)
    {
        Bullet bullet = Instantiate(bulletObj, spawnPos.position, Quaternion.Euler(0, 0, angle)).GetComponent<Bullet>();
        bullet.speed = speed;
        bullet.damage = activeWeaponStats.bulletDamage;
    }
}
