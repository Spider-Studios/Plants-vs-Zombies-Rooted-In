using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    #region Singleton
    public static SpeedUp instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of speedup found");
            return;
        }
        instance = this;
    }
#endregion
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
