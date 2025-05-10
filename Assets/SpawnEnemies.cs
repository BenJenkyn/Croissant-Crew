using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    private float spawnInterval = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnInterval > 0)
        {
            spawnInterval -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            spawnInterval = 2f;
        }
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation).gameObject;
        enemy.transform.position = transform.position;
    }
}
