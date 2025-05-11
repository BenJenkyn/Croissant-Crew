using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private float spawnInterval, timer;
    void Start()
    {
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            timer = spawnInterval;
        }

        if (Player.instance.progress >= Player.instance.maxProgress)
        {
            Destroy(this);
        }
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation).gameObject;
        enemy.transform.position = transform.position + new Vector3(Random.Range(-2,2), Random.Range(-2,2));
    }
}
