using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float damage = 10f;
    private float lifeTime = 10f;
    public int id;

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
        if (lifeTime > 0)
        {
            lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
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
