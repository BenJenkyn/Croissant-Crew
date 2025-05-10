using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float damage = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Get the Rigidbody2D component of the bullet
    }

    public void Reflect(string axis)
    {
        if (axis == "X")
        {
            rb.linearVelocityX = -rb.linearVelocityX;
        }
        if (axis == "Y")
        {
            rb.linearVelocityY = -rb.linearVelocityY;
        }
    }

    public float getDamage()
    {
        return damage;
    }
}
