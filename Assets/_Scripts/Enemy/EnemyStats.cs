using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public float health ;
    public float maxHealth = 5;
    public float damage = 0.05f;
    public float speed = 1f;
    public int moneyReward = 5;
    public int rewardVariance = 1;
    private static System.Random random = new System.Random();

    public EnemyStats()
    {
        health = 3f;
        damage = 1f;
        speed = 1f;
        moneyReward = 5;
        rewardVariance = 1;
    }

    public int GetReward()
    {
        return moneyReward + random.Next(-rewardVariance, rewardVariance);
    }
}