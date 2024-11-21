using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    public int totalKill = 0;
    [SerializeField] 
    private int minimumKillsToIncreaseSpawnCount = 3;

    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    private List<Enemy> spawnedObjects = new List<Enemy>(); 

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;    
    public bool isSpawning = false;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
    }

    public IEnumerator SpawnEnemy()
    {
        if (spawnedEnemy != null && isSpawning == true)
        {
            //isSpawning = true;
            spawnCount = defaultSpawnCount;
            for(int i = 0; i< defaultSpawnCount; i++)
            {
                Enemy newEnemy = Instantiate(spawnedEnemy);
                spawnedObjects.Add(newEnemy);
                returnlevel(newEnemy.level);
                yield return new WaitForSeconds(spawnInterval);

                HealthComponent healthComponent = newEnemy.GetComponent<HealthComponent>();
                if(healthComponent != null)
                {
                    healthComponent.onDestroyed.AddListener(() => OnEnemyDestroyed(newEnemy));
                }
            }

            Debug.Log("Objects Spawned: " + spawnedObjects.Count);
        }
        else
        {
            Debug.LogWarning("Missing object to spawn or spawn location.");
            yield return null;
        }
        isSpawning = false;
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        --spawnCount;
        totalKillWave++;
        totalKill++;
        Debug.Log("Enemy destroyed! Total kills this wave: " + totalKillWave);
        Debug.Log("Total " + enemy + " Enemy killed: " + totalKill);
    }

    public void Update()
    {
        bool apaboolean = false;

        if (totalKill >= minimumKillsToIncreaseSpawnCount && apaboolean == false)
        {
            apaboolean = true;
            minimumKillsToIncreaseSpawnCount += minimumKillsToIncreaseSpawnCount;
            defaultSpawnCount += spawnCountMultiplier;

            if (totalKillWave >= multiplierIncreaseCount)
            {
                spawnCountMultiplier++;
                totalKillWave = 0;
            }
        }

        // Only spawn a new wave when all enemies from the previous wave are killed
        // if (GetActiveObjectCount() == 0 && !isSpawning)
        // {
        //     isSpawning = true;
        //     StartCoroutine(SpawnEnemy());
        // }
    }

    public int GetActiveObjectCount()
    {
        spawnedObjects.RemoveAll(item => item == null);

        Debug.Log("Active Objects: " + spawnedObjects.Count);

        return spawnedObjects.Count;
    }

    public void returnlevel(int level)
    {
        //combatManager = level;
    }
}
