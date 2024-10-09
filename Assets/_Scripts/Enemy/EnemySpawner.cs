using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public float spawnRate = 1f;

    public float spawnDistance = 5f;

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
                float angle = Random.Range(0, 2 * Mathf.PI);
                Vector3 randomPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Mathf.Sqrt(Random.Range(0, spawnDistance));
                BasicEnemyController enemy = Instantiate(enemyToSpawn, transform.position + randomPos, Quaternion.identity).GetComponent<BasicEnemyController>();
                enemy.player = player;
            }
        }

    }
}
