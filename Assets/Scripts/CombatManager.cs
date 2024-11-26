using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;

    private void Start()
    {
        // foreach(EnemySpawner enemySpawner in enemySpawners)
        // {
        //     totalEnemies += enemySpawner.spawnCount;
        // }
    }

    private void Update()
    {
        Debug.Log("Combatmanager: Total Enemies: " + totalEnemies);
        if (totalEnemies <= 0)   //entah kenapa gak ke-detect
        {
            timer += Time.deltaTime;

            if (timer > waveInterval)
            {
                timer = 0;
                waveNumber++;
                totalEnemies = 0;
                Debug.Log("Mulai Wave: " + waveNumber);

                foreach (EnemySpawner enemySpawner in enemySpawners)
                {
                    //StartCoroutine(enemySpawner.SpawnEnemy());
                    if(waveNumber >= enemySpawner.spawnedEnemy.level)
                    {
                        enemySpawner.isSpawning = true;
                        StartCoroutine(enemySpawner.SpawnEnemy());
                        enemySpawner.spawnCount = enemySpawner.defaultSpawnCount;
                        totalEnemies += enemySpawner.spawnCount;
                    }
                }
            }
        }
    }

    public void UpdateTotalEnemies()
    {
        totalEnemies--;

        // if (totalEnemies <= 0)
        // {
        //     timer = waveInterval;
        // }
    }

    public float getInterval()
    {
        return waveInterval;
    }
}