using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    float spawnBorder = 9;
    public GameObject enemy, powerUp;
    int enemyCount;
    int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            Instantiate(powerUp, GenerateSpawnLocation(), powerUp.transform.rotation);
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
    }

    Vector3 GenerateSpawnLocation()
    {
        float xPos = Random.Range(-spawnBorder, spawnBorder);
        float zPos = Random.Range(-spawnBorder, spawnBorder);
        Vector3 spawnPos = new Vector3(xPos, 0, zPos);
        return spawnPos;
    }

    void SpawnEnemyWave(int numOfEnemy)
    {
        for(int i = 0; i < numOfEnemy; i++)
        {
            Instantiate(enemy, GenerateSpawnLocation(), enemy.gameObject.transform.rotation);
        }
    }

}
