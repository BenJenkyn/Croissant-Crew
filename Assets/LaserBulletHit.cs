using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] private LaserBullet laserBullet;
    int framesAlive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        framesAlive++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && framesAlive > 120)
        {
            float damage = laserBullet.getDamage();
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        if (collision.CompareTag("Enemy"))
        {
            float damage = laserBullet.getDamage();
            collision.GetComponent<Enemy>().TakeDamage(damage, laserBullet.id);
        }
    }
}
