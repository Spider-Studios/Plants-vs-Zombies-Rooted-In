using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseTime()
    {


        Time.timeScale = 0;

    }

    public void Unpause()
    { 
        if (Time.timeScale == 0 && SpeedUp.instance.speedup == true)
        {
            Time.timeScale = 2;
        }
        if (Time.timeScale == 0 && SpeedUp.instance.speedup == false)
        {
            Time.timeScale = 1;
        }
    }
}
