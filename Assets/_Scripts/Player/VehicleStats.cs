using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class VehicleStats
{
    public float acceleration = 35f;
    public float maxSpeed = 13f;
    public float turnSpeed = 150f;
    public float drag = 2f;
    public float angularDrag = 1f;
    public float tyreGrip = 10f;
    public float driftGrip = -3f;
    public float gainGripMult = .2f;
    public float loseGripMult = 1f;
    public KeyCode driftKey = KeyCode.LeftShift;
    public float driftBodyRotation = 35f;
    public float bodyRotaPerSec = 70f;

    public VehicleStats()
    {
        acceleration = 35f;
        maxSpeed = 13f;
        turnSpeed = 150f;
        drag = 2f;
        angularDrag = 1f;
        tyreGrip = 10f;
        driftGrip = -3f;
        gainGripMult = .2f;
        loseGripMult = 1f;
        driftKey = KeyCode.LeftShift;
        driftBodyRotation = 35f;
        bodyRotaPerSec = 70f;
    }
}