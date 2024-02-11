using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab; // R�f�rence au prefab du boss
    public List<Transform> spawnPoints;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5.0f;
    public float difficultyMultiplier = 1.2f;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
            SpawnWave();

            // V�rifiez si c'est le moment de g�n�rer un boss
            if (currentWave % 5 == 0)
            {
                SpawnBoss();
            }

            enemiesPerWave = Mathf.CeilToInt(enemiesPerWave * difficultyMultiplier);
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemyAtRandomPoint();
        }
    }

    void SpawnEnemyAtRandomPoint()
    {
        int pointIndex = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPosition = spawnPoints[pointIndex].position;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnBoss()
    {
        int pointIndex = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPosition = spawnPoints[pointIndex].position;
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity); // G�n�rez le boss
    }
}
