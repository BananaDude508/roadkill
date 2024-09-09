using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;


public class BasicEnemyController : MonoBehaviour
{
    EnemyStats stats = new EnemyStats();


    public void TakeDamage(float damage)
    {
        stats.health -= damage;

        if (stats.health > 0) return;

        money += stats.GetReward();
        Destroy(gameObject);
    }
}
