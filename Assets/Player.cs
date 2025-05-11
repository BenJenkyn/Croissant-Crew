using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal = 0f;
    private float vertical = 0f;
    private SpriteRenderer spriteRenderer;

    private float health;

    // Sprite direction movements
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;
    [SerializeField] private Sprite spriteUpLeft;
    [SerializeField] private Sprite spriteUpRight;
    [SerializeField] private Sprite spriteDownLeft;
    [SerializeField] private Sprite spriteDownRight;
    [SerializeField] private Sprite spriteIdle;

    // Transforms and game objects
    [SerializeField] private Transform gunParent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject bulletPrefab;

    // Sound effects
    [SerializeField] private AudioSource lazerShoot;
    [SerializeField] public AudioSource enemyDied;
    [SerializeField] private AudioSource playerHurt;
    [SerializeField] private AudioSource leaveExit;
    [SerializeField] private AudioSource level1Music;
    [SerializeField] private AudioSource level2Music;
    [SerializeField] private AudioSource playerDead;

    AudioSource currentMusic;

    bool isImmmne = false;
    bool isDead;
    int framesAlive = 0;
    public static Player instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
        Init();
        DontDestroyOnLoad(this);
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

        if (!isDead)
        {
            // Rotate the gun to face the mouse
            Mouse mouse = Mouse.current;
            Vector2 mousePosition = mouse.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            gunParent.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x) * Mathf.Rad2Deg - 90);
            framesAlive++;
            if (framesAlive % 10 == 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * 10f;
    }

    public void Shoot()
    {
        lazerShoot.Play();
        // Instantiate the bullet at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Get the Rigidbody2D component of the bullet
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity to move forward
        rbBullet.linearVelocity = new Vector2(-Mathf.Sin(firePoint.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(firePoint.rotation.eulerAngles.z * Mathf.Deg2Rad)) * 10f; // Adjust speed as necessary
    }

    public void Die()
    {
        isDead = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Live()
    {
        Init();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void TakeDamage(float damage)
    {
        if (isImmmne)
        {
            return;
        }
        health -= damage;
        playerHurt.Play();
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

    private void Init()
    {
        health = 100f;
        isDead = false;
        transform.position = new Vector3(0, 0);
        SceneManager.LoadScene("Level1");
        PlayMusic(level1Music);
    }

    void PlayMusic(AudioSource source)
    {
        if (currentMusic != source)
        {
            if (currentMusic != null)
            {
                currentMusic.Stop();
            }
            source.Play();
            currentMusic = source;
        }

    }
}
