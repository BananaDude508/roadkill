using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

public class Chase : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    public GameObject player;

    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, stats.speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth -= stats.damage;
        }
        
    }
}
