using PvZRI.Towers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PvZRI.Interaction
{
    public class SelectTower : MonoBehaviour
    {
        #region Singleton
        public static SelectTower instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one instance of select tower found");
                return;
            }
            instance = this;
        }
        #endregion

        public Tower selected = null;

        public GameObject selectedPanel = null;

        void Start()
        {

        }
        
        void Update()
        {
           if(Input.GetMouseButtonDown(1))
            {
                if (selected != null)
                {
                    HideSelectedPanel();
                }
            }
        }

        public void ShowSelectedPanel()
        {
            //find the upgrade buttons and remove their click events
            GameObject upgrade1;
            upgrade1 = selectedPanel.transform.Find("Upgrade 1").gameObject;
            upgrade1.GetComponent<Button>().onClick.RemoveAllListeners();

            GameObject upgrade2;
            upgrade2 = selectedPanel.transform.Find("Upgrade 2").gameObject;
            upgrade2.GetComponent<Button>().onClick.RemoveAllListeners();

            //show the range of the selected tower
            selected.rangeDisplay.SetActive(true);

            //show the selected tower's info
            selectedPanel.SetActive(true);

            //show the tower's name
            selectedPanel.transform.Find("Tower Name").GetComponent<Text>().text = selected.name;
            
            //set the upgrade buttons text and what upgrade they link to
            upgrade1.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath1[0].name;
            upgrade1.GetComponent<Button>().onClick.AddListener(selected.upgradePath1[0].AddUpgrades);
            
            upgrade2.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath2[0].name;
            upgrade2.GetComponent<Button>().onClick.AddListener(selected.upgradePath2[0].AddUpgrades);
        }

        private void HideSelectedPanel()
        {
            selected.rangeDisplay.SetActive(false);
            selectedPanel.SetActive(false);
            selected = null;
        }
    }
}
