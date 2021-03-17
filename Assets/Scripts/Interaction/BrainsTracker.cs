using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainsTracker : MonoBehaviour
{
    public int brains = 0;
    public Text brainsDisplay;

    private void Update()
    {
        brainsDisplay.text = "Brains: " + brains;
    }


    public void Minushealth(int amount)
    {
        brains -= amount;
    }

   
    }




