using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int money = 0;
    public static float playerHealth = 10;


    public static PlayerController playerController;

    // Vehicle Stats
    public static VehicleMovement vehicleMovement;

    public static VehicleStats vehicleStats = new VehicleStats();


    // Weapon Stats
    public static WeaponStats weaponStats = new WeaponStats();

    public static void ReInit()
    {
        money = 0;
        playerHealth = 10;
        vehicleStats = new VehicleStats();
        weaponStats = new WeaponStats();
    }
}