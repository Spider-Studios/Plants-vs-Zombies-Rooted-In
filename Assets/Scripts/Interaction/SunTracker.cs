using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunTracker : MonoBehaviour
{
    public int sun = 0;
    
    public Text sunDisplay;

    private void Update()
    {
        sunDisplay.text = "Sun: " + sun;
    }

    public void AddSun(int amount)
    {
        sun += amount;
    }

    public void MinusSun(int amount)
    {
        sun -= amount;
    }

    public bool HaveEnoughSun(int amount)
    {
        if(sun >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

}