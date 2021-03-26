using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoubleTime();
        }
        }
    public void DoubleTime()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale++;
        }
        else
        {
            Time.timeScale--;
        }
    }
}
