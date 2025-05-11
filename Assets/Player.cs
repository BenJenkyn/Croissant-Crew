using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal = 0f;
    private float vertical = 0f;
    private SpriteRenderer spriteRenderer;

    public float health, maxHealth;

    // Sprite direction movements
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;
    [SerializeField] private Sprite spriteIdle;

    // Transforms and game objects
    [SerializeField] private Transform gunParent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject laserBullet;
    [SerializeField] private GameObject mineBullet;

    // Sound effects
    [SerializeField] private AudioSource lazerShoot;
    [SerializeField] public AudioSource enemyDied;
    [SerializeField] private AudioSource playerHurt;
    [SerializeField] private AudioSource leaveExit;
    [SerializeField] private AudioSource level1Music;
    [SerializeField] private AudioSource level2Music;
    [SerializeField] private AudioSource levelWinMusic;
    [SerializeField] private AudioSource playerDeadMusic;

    // UI
    [SerializeField] private Image hpBar;
    [SerializeField] private Image progressBar;
    public TextMeshProUGUI levelText;

    // Game logic
    public float progress, maxProgress;
    public List<Enemy> enemies;
    public bool won;

    AudioSource currentMusic;

    bool isImmune = false;
    bool isDead;
    int framesAlive = 0;
    public static Player instance;

    [SerializeField] List<GameObject> bulletPrefabs;
    string nextBullet;
    bool bulletCd;

    [SerializeField] bool title;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;

        ;
        DontDestroyOnLoad(this);
        nextBullet = "laser";
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical > 0)
        {
            spriteRenderer.sprite = spriteUp;
        }
        else if (vertical < 0)
        {
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

        if (title)
        {
            Mouse mouse = Mouse.current;
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Level1");
                title = false;
            }
        }
        else if (!isDead)
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
            setNextBullet();
        }

        progress += Time.deltaTime;

        if (progress >= maxProgress)
        {
            if (!won && enemies.Count == 0)
            {
                won = true;
                PlayMusic(levelWinMusic);
                GameObject levelExitObj = FindObjectByName("LevelExitDoor");
                levelExitObj.SetActive(true);
            }
        }

        hpBar.transform.localScale = new Vector3(1, Mathf.Clamp(health / maxHealth, 0, 1));
        progressBar.transform.localScale = new Vector3(1, Mathf.Clamp(progress / maxProgress, 0, 1));
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * 10f;
    }

    public void Shoot()
    {
        if (bulletCd)
        {
            return;
        }

        if (nextBullet.Equals("laser"))
        {
            StartCoroutine(FireLaser());
        }
        if (nextBullet.Equals("mine"))
        {
            StartCoroutine(FireMine());
        }
    }

    IEnumerator FireLaser()
    {
        bulletCd = true;
        lazerShoot.Play();

        Vector2 gunPos = firePoint.position;
        Vector2 gunAngle = new Vector2(-Mathf.Sin(firePoint.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(firePoint.rotation.eulerAngles.z * Mathf.Deg2Rad));

        int bulletId = Random.Range(0, 100000);

        for (int i = 0; i < 8; i++)
        {
            // Instantiate the bullet at the gun's position and rotation
            GameObject bullet = Instantiate(laserBullet, gunPos, Quaternion.identity);
            bullet.GetComponent<LaserBullet>().id = bulletId;
            // Get the Rigidbody2D component of the bullet
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            // Set the bullet's velocity to move forward
            rbBullet.linearVelocity = gunAngle * 10f; // Adjust speed as necessary

            yield return new WaitForSecondsRealtime(2 / 60f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        bulletCd = false;
    }

    IEnumerator FireMine()
    {
        bulletCd = true;
        lazerShoot.Play();

        Vector2 gunPos = firePoint.position;
        Vector2 gunAngle = new Vector2(-Mathf.Sin(firePoint.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(firePoint.rotation.eulerAngles.z * Mathf.Deg2Rad));

        int bulletId = Random.Range(0, 100000);

        // Instantiate the bullet at the gun's position and rotation
        GameObject bullet = Instantiate(mineBullet, gunPos, Quaternion.identity);
        bullet.GetComponent<MineBullet>().id = bulletId;

        yield return new WaitForSecondsRealtime(0.5f);
        bulletCd = false;
    }

    public void Die()
    {
        isDead = true;
        gameOverScreen.SetActive(true);
        PlayMusic(playerDeadMusic);
        Time.timeScale = 0;
    }

    public void Live()
    {
        Destroy(this.gameObject);
        Destroy(Game.instance.gameObject);
        Init();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void TakeDamage(float damage)
    {
        if (isImmune)
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
        isImmune = true;
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSecondsRealtime(0.05f);
            spriteRenderer.enabled = true;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        isImmune = false;
    }

    private void Init()
    {
        title = true;
        health = 100f;
        maxHealth = 100f;
        isDead = false;
        won = false;
        enemies = new();
        transform.position = new Vector3(0, 0);
        progress = 0;
        SceneManager.LoadScene("Title");
    }

    public void PlayMusic(AudioSource source)
    {
        if (currentMusic == null || currentMusic?.clip != source.clip)
        {
            if (currentMusic != null)
            {
                currentMusic.Stop();
            }
            source.Play();
            currentMusic = source;
        }

    }

    public GameObject FindObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name == name && objs[i].gameObject.scene.name != null)
            {
                return objs[i].gameObject;
            }
        }
        return null;
    }

    public void setNextBullet()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextBullet = "laser";
            Debug.Log("Laser selected");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nextBullet = "mine";
            Debug.Log("Mine selected");
        }
        // else if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     nextBullet = "bomb";
        // }
    }
}
