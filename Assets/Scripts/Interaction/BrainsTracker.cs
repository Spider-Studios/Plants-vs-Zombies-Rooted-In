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

    private void Update()
    {
        brainsDisplay.text = "Brains: " + brains;
        if (brains <= 0)
        {
            gameOver.SetActive(true);
            Destroy(startButton);
            Destroy(spawnPoint);
            
        }
    }


    public void Minushealth(int amount)
    {
        brains -= amount;
    }

    }




