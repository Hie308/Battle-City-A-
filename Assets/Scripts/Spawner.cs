using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    // https: //www.youtube.com/watch?v=ajwRvAGKl_k&list=PLFt_AvWsXl0ctd4dgE1F8g3uec4zKNRV0&index=6
    public Wave[] waves;
    public Enemy enemy;
    public Vector3 Location;
    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    
    void Start()
    {
        NextWave();
    }

    void Update()
    {

        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
       
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Location, Quaternion.identity) as Enemy; // hàm spawn
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
       

    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        print("Wave: " + currentWaveNumber); // debug
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }

}