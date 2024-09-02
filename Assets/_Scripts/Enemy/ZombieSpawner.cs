using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject[] ZombiePrefabs;

    [SerializeField] private bool canSpawn = true;

    public GameObject player;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true) {
            yield return wait;
            int rand = Random.Range(0, ZombiePrefabs.Length);
            GameObject enemyToSpawn = ZombiePrefabs[rand];

            Chase enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity).GetComponent<Chase>();
            enemy.player = player;
        }

    }
}
