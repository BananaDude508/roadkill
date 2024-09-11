using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public float spawnRate = 1f;

	public GameObject[] ZombiePrefabs;

	public bool canSpawn = true;

    public GameObject player;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
		while (true)
		{
            yield return new WaitForSeconds(spawnRate); ;
            int rand = Random.Range(0, ZombiePrefabs.Length);
            GameObject enemyToSpawn = ZombiePrefabs[rand];

            if (canSpawn)
            {
                BasicEnemyController enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity).GetComponent<BasicEnemyController>();
                enemy.player = player;
            }
        }

    }
}
