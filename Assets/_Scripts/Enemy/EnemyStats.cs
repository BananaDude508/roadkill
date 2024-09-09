using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyStats
{
    public float health ;
    public float maxHealth = 5;
    public float damage = 1f;
    public float speed = 1f;
    public int moneyReward = 5;

    public EnemyStats()
    {
        health = 3f;
        damage = 1f;
        speed = 1f;
        moneyReward = 5;
    }
    

}
