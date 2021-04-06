using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunTracker : MonoBehaviour
{
    
    #region Singleton
    public static SunTracker instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of sun tracker found");
            return;
        }
        instance = this;
    }
    #endregion

    public int sun = 0;
    
    public Text sunDisplay;

    Color baseColor;

    private void Start()
    {
        baseColor = sunDisplay.color;
    }

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
            StartCoroutine(FlashText());
            sunDisplay.color = baseColor;
            return false;
        }
    }
       
    public IEnumerator FlashText()
    {
        for (int i = 0; i < 5; i++)
        {
            sunDisplay.color = Color.red;
            yield return new WaitForSeconds(.1f);
            sunDisplay.color = baseColor;
            yield return new WaitForSeconds(.1f);
        }
    }

}
