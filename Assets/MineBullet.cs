using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MineBullet : MonoBehaviour
{
    private float damage = 20f;
    public float explosionRadius = 5f; // Set this in the Inspector or here
    public int id;

    int framesAlive;

    [SerializeField] Explosion explosion;

    void Start()
    {

    }

    void Update()
    {
        framesAlive++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Explode(collision);
    }

    void Explode(Collider2D collision)
    {
        if (collision.CompareTag("Player") && framesAlive > 180)
        {
            StartCoroutine(ExplodeShow());
        }
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(ExplodeShow());
        }

        // Destroy the mine bullet after explosion
    }

    public float getDamage()
    {
        return damage;
    }

    IEnumerator ExplodeShow() {
            explosion.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.25f);
            explosion.gameObject.SetActive(false);
            Destroy(gameObject);
    }
}
