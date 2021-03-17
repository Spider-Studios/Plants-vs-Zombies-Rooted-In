using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
    
}
public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject startWaveButton;
    public GameObject waveSpawner;
   

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
   

    private bool canSpawn = true;
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Zombie");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber+1 != waves.Length)
        {
            startWaveButton.SetActive(true);
            currentWaveNumber++;
            waveSpawner.SetActive(false);
            canSpawn = true;
        }
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
   
    }

    void SpawnWave()
    {

        startWaveButton.SetActive(false);

        if (canSpawn && nextSpawnTime < Time.time)
        {

            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
             
            }
        }

    }
        
}
