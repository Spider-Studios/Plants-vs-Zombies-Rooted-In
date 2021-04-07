using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainsTracker : MonoBehaviour
{
    public int brains = 0;
    public Text brainsDisplay;
    public GameObject startButton;
    public GameObject spawnPoint;
    public GameObject gameOver;
    public GameObject daytimeMusic;
    public AudioSource screamSound;

    private bool audioPlayed = false;
    private void Update()
    {
        brainsDisplay.text = "Brains: " + brains;
        if (brains <= 0)
        {
            gameOver.SetActive(true);
            daytimeMusic.SetActive(false);
            Destroy(startButton);
            Destroy(spawnPoint);
            if (audioPlayed == false)
            {
                screamSound.PlayDelayed(3);
                audioPlayed = true;
            }
        }
    }
    
        public void Minushealth(int amount)
    {
        brains -= amount;
    }

    }




