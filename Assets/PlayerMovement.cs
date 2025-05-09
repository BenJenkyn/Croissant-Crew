using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal = 0f;
    private float vertical = 0f;
    private SpriteRenderer spriteRenderer;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    public Sprite spriteUpLeft;
    public Sprite spriteUpRight;
    public Sprite spriteDownLeft;
    public Sprite spriteDownRight;
    public Sprite spriteIdle;

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
