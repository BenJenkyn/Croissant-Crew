using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float health;
    public static Enemy instance;
    List<int> bulletIds;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletIds = new();
        Player.instance.enemies.Add(this);
    }

    private void Update()
    {
        if (rb.linearVelocityX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.CompareTag("Player"))
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(10f);
    }
  }

  public void Die()
    {
        Player.instance.enemyDied.Play();
        GameObject splat = Resources.Load<GameObject>("Splat");
        Instantiate(splat, transform.position, Quaternion.Euler(0, 0, Random.Range(0f,359f)), null);
        Player.instance.enemies.Remove(this);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage, int bulletId)
    {
        if (bulletIds.Contains(bulletId))
        {
            return;
        }
        health -= damage;
        StartCoroutine(BulletImmunity(bulletId));
        StartCoroutine(HurtFlash());
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator BulletImmunity(int bulletId)
    {
        bulletIds.Add(bulletId);
        yield return new WaitForSecondsRealtime(0.5f);
        bulletIds.Remove(bulletId);
    }

    IEnumerator HurtFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i <= 10; i++)
        {
            sr.color = new Color(1, i * 0.1f, i * 0.1f);
            yield return new WaitForSecondsRealtime(1 / 60f);
        }

    }
}
