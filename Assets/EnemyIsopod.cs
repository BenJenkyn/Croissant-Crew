using System.Collections;
using UnityEngine;

public class EnemyIsopod : MonoBehaviour
{
    Rigidbody2D rb;
    bool charging;
    int framesAlive = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Charge());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (framesAlive % 10 == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    IEnumerator Charge()
    {
        Vector3 startPos = transform.position;
        for (int i = 0; i < 20; i++)
        {
            transform.position = startPos + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        Vector3 playerPos = Player.instance.transform.position;
        Vector3 direction = playerPos - transform.position;

        charging = true;
        while (charging)
        {
            rb.linearVelocity = direction * 1.5f;
            yield return new WaitForSecondsRealtime(1 / 60f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!charging) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(8);
            StartCoroutine(Charge());
            charging = false;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(Charge());
            charging = false;
        }
    }
}
