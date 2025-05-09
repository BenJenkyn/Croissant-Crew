using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal = 0f;
    private float vertical = 0f;
    private SpriteRenderer spriteRenderer;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vertical > 0)
        {
            if(horizontal > 0)
                spriteRenderer.sprite = spriteUpRight;
            else if(horizontal < 0)
                spriteRenderer.sprite = spriteUpLeft;
            else
                spriteRenderer.sprite = spriteUp;
        }
        else if(vertical < 0)
        {
            if(horizontal > 0)
                spriteRenderer.sprite = spriteDownRight;
            else if(horizontal < 0)
                spriteRenderer.sprite = spriteDownLeft;
            else
                spriteRenderer.sprite = spriteDown;
        }
        else
        {
            if(horizontal > 0)
                spriteRenderer.sprite = spriteRight;
            else if(horizontal < 0)
                spriteRenderer.sprite = spriteLeft;
            else
                spriteRenderer.sprite = spriteIdle;
        }

        // Rotate the gun to face the mouse
        Mouse mouse = Mouse.current;
        Vector2 mousePosition = mouse.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        gunParent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * 10f;
    }
}
