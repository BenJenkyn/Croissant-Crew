using UnityEngine;

public class EnemyBeetle : MonoBehaviour
{
    Rigidbody2D rb;
    void FixedUpdate()
    {
        // Get the Rigidbody2D component of the enemy
        rb = this.GetComponent<Rigidbody2D>();

        // Set the enemy's velocity to move towards the player
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        rb.linearVelocity = direction * 2f; // Adjust speed as necessary

    }
}
