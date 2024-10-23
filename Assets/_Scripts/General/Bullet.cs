using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 2f;
    public float lifetime = 5f;
    public ParticleSystem explosionParticals;


    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (lifetime <= 0)
            Explode();
        lifetime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<BasicEnemyController>().EnemyDamage(damage);
        Explode();
    }
    private void Explode()
    {
        explosionParticals.transform.SetParent(null, true);
        Destroy(explosionParticals.gameObject, explosionParticals.main.startLifetime.constant);
        explosionParticals.Play();

        Destroy(gameObject);
    }
}
