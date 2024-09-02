using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public float fireRate = 2f; // Shots per second
    public float maxAmmo = 12f;
    public float currentAmmo = 12f;
    public float reloadTime = 1f; // Ttime in seconds
    public float bulletDamage = 1.5f;
    public float bulletSpeed = 1200f; // Force applied using Rigidbody2D.AddForce();
    public float bulletLifetime = 3f; // Time in seconds


    public WeaponStats()
    {
        fireRate = 2f;
        maxAmmo = 12f;
        currentAmmo = 12f;
        reloadTime = 1f;
        bulletDamage = 1.5f;
        bulletSpeed = 1200f;
        bulletLifetime = 3f;
    }
}
