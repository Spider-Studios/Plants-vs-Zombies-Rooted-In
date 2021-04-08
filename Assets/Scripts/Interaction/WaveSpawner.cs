using UnityEngine;
using UnityEngine.UI;
using PvZRI.Towers;

[System.Serializable]

public class Wave
{
    public string waveName;
    public GameObject[] enemies;
    public float spawnInterval;
    public int reward;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject startWaveButton;
    public GameObject waveSpawner;
    public SunTracker sunTracker;
    public AudioSource sunSound;
    public GameObject brainSound;
    public GameObject dayTimeSound;
    public GameObject idleSound;
    public GameObject winMessage;

    private int positionInWave = 0;
    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    public Text currentWaveDisplay;

    private bool canSpawn = true;
    public bool isSpawning = false; 

    private void Update()
    {
        if (isSpawning == true)
        {
            currentWave = waves[currentWaveNumber];
            currentWaveDisplay.text = "Wave: " + currentWave.waveName;
            SpawnWave();
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Zombie");

            //end of the wave
            if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
            {
                positionInWave = 0;
                startWaveButton.SetActive(true);
                sunTracker.AddSun(currentWave.reward);
                currentWaveNumber++;
                isSpawning = false;
                canSpawn = true;
                sunSound.Play();
                brainSound.SetActive(false);
                idleSound.SetActive(true);
                dayTimeSound.SetActive(false);

                //add the sun rewards for towers that have them
                foreach (Tower tower in FindObjectsOfType<Tower>())
                {
                    if (tower.sunReward > 0)
                    {
                        sunTracker.AddSun(tower.sunReward);
                    }
                }
            }
            if (totalEnemies.Length == 0 && currentWaveNumber == waves.Length)
            {
                winMessage.SetActive(true);
                Destroy(idleSound);
                Destroy(dayTimeSound);
            }
        }
    }

        void SpawnWave()
    {
        startWaveButton.SetActive(false);
        idleSound.SetActive(false);
        dayTimeSound.SetActive(true);

        if (canSpawn && nextSpawnTime < Time.time)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(currentWave.enemies[positionInWave], randomPoint.position, Quaternion.identity);
            positionInWave++;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (positionInWave >= currentWave.enemies.Length)
            {
                canSpawn = false;
            }
        }
    }

    public void setSpawning()
    {
        isSpawning = !isSpawning;
    
    }
}
