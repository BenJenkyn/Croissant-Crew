using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private Transform gunParent;
    [SerializeField] private GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Instantiate the bullet at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, gunParent.position, gunParent.rotation);

        // Get the Rigidbody2D component of the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity to move forward
        rb.linearVelocity = gunParent.up * 10f; // Adjust speed as necessary
    }
}
