using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 2f;
    public float lifetime = 5f;
    public ParticleSystem explosionParticals;


    private void Start()
    {
        

        if (gameObject != null && lifetime <= 0)
        {
            explode();
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<BasicEnemyController>().EnemyDamage(damage);
        explode();
    }
    private void explode()
    {
        explosionParticals.transform.SetParent(null, true);
        Destroy(explosionParticals.gameObject, explosionParticals.main.startLifetime.constant);
        explosionParticals.Play();

        Destroy(gameObject);
    }
}
