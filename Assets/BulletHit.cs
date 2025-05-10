using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float damage = bulletPrefab.getDamage();
        }
        if (collision.CompareTag("Enemy"))
        {
            float damage = bulletPrefab.getDamage();
            Debug.Log("Hit enemy");
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
