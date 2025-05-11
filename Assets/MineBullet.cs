using UnityEngine;
using System.Collections.Generic;

public class MineBullet : MonoBehaviour
{
    private float damage = 20f;
    public float explosionRadius = 5f; // Set this in the Inspector or here
    public int id;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Explode(collision);
    }

    void Explode(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float damage = getDamage();
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        if (collision.CompareTag("Enemy"))
        {
            float damage = getDamage();
            collision.GetComponent<Enemy>().TakeDamage(damage, id);
        }

        // Destroy the mine bullet after explosion
        Destroy(gameObject);
    }

    public float getDamage()
    {
        return damage;
    }
}
