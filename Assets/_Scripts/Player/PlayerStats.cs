using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int money = 0;
    public static float health = 10;


    // Vehicle Stats
    public static VehicleMovement vehicleMovement;

    public static VehicleStats vehicleStats = new VehicleStats();


    // Weapon Stats
    public static WeaponStats weaponStats = new WeaponStats();

    public static void ReInit()
    {
        money = 0;
        health = 10;
        vehicleStats = new VehicleStats();
        weaponStats = new WeaponStats();
    }
}