using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal = 0f;
    private float vertical = 0f;
    private SpriteRenderer spriteRenderer;
    private float health = 100f;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;
    [SerializeField] private Sprite spriteUpLeft;
    [SerializeField] private Sprite spriteUpRight;
    [SerializeField] private Sprite spriteDownLeft;
    [SerializeField] private Sprite spriteDownRight;
    [SerializeField] private Sprite spriteIdle;
    [SerializeField] private Transform gunParent;
    bool isImmmne = false;
    int frameesAlive = 0;
    public static Player instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical > 0)
        {
            if (horizontal > 0)
                spriteRenderer.sprite = spriteUpRight;
            else if (horizontal < 0)
                spriteRenderer.sprite = spriteUpLeft;
            else
                spriteRenderer.sprite = spriteUp;
        }
        else if (vertical < 0)
        {
            if (horizontal > 0)
                spriteRenderer.sprite = spriteDownRight;
            else if (horizontal < 0)
                spriteRenderer.sprite = spriteDownLeft;
            else
                spriteRenderer.sprite = spriteDown;
        }
        else
        {
            if (horizontal > 0)
                spriteRenderer.sprite = spriteRight;
            else if (horizontal < 0)
                spriteRenderer.sprite = spriteLeft;
            else
                spriteRenderer.sprite = spriteIdle;
        }

        // Rotate the gun to face the mouse
        Mouse mouse = Mouse.current;
        Vector2 mousePosition = mouse.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        gunParent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x) * Mathf.Rad2Deg - 90);
        frameesAlive++;
        if (frameesAlive % 10 == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * 10f;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (isImmmne)
        {
            return;
        }
        health -= damage;
        StartCoroutine(HitCooldown());
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitCooldown()
    {
        isImmmne = true;
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        isImmmne = false;
    }
}
