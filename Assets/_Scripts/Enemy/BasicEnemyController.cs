using System.Collections;
using UnityEngine;
using static PlayerStats;


public class BasicEnemyController : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
	public ParticleSystem particleTrail;
	public GameObject player;

	public bool touchingPlayer = false;

	public bool despawn = true;
	public float despawnRadius = 160; // viewDistance (5) * tileSize (32) unity meters
	[Tooltip("Values <= 0 do not despawn")] public float despawnTimer = 60; // 60 seconds


    private void Start()
    {
		if (despawn && despawnTimer > 0)
			Destroy(gameObject, despawnTimer);
    }

    private void Update()
	{
		Chase();

		if (despawn && Vector3.Distance(player.transform.position, transform.position) > despawnRadius)
			Destroy(gameObject);

		if (touchingPlayer)
			PlayerDamage(stats.damage * Time.deltaTime);
	}

	public void EnemyDamage(float damage)
    {
        stats.health -= damage;

        if (stats.health > 0) return;

        ChangeMoney(stats.GetReward());
		particleTrail.transform.parent.DetachChildren();
		Destroy(particleTrail.gameObject, particleTrail.main.startLifetime.constant);
		particleTrail.Stop();
        Destroy(gameObject);
    }

	private void Chase()
	{
		Vector2 direction = player.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, stats.speed * Time.deltaTime);
		transform.rotation = Quaternion.Euler(Vector3.forward * angle);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
			touchingPlayer = true;
	}
	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
            touchingPlayer = false;
    }
}
