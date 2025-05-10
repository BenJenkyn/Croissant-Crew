using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private float health = 100f;
    public static Enemy instance;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        rb.linearVelocity = direction * 5f; // Adjust speed as necessary
        // Rotate the enemy to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void FixedUpdate()
    {
        // Get the Rigidbody2D component of the enemy
        rb = this.GetComponent<Rigidbody2D>();

        // Set the enemy's velocity to move towards the player
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        rb.linearVelocity = direction * 1f; // Adjust speed as necessary
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
