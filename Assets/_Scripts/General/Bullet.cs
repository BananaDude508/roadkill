using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 2f;
    public float lifetime = 5f;


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<BasicEnemyController>().EnemyDamage(damage);
        Destroy(gameObject);
    }
}
