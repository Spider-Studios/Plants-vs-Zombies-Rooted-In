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

        GameObject upgrade1;
        GameObject upgrade2;

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

            if (selected != null)
            {
                //set the buttons text to the next upgrade
                if (selected.path1Purchased != selected.upgradePath1.Length)
                {
                    upgrade1.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath1[selected.path1Purchased].name;
                }
                else
                {
                    upgrade1.GetComponent<Button>().interactable = false;
                }

                if (selected.path2Purchased != selected.upgradePath2.Length)
                {
                    upgrade2.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath2[selected.path2Purchased].name;
                }
                else
                {
                    upgrade2.GetComponent<Button>().interactable = false;
                }
            }
        }

        public void ShowSelectedPanel()
        {
            //find the upgrade buttons and remove their click events
            upgrade1 = selectedPanel.transform.Find("Upgrade 1").gameObject;
            upgrade1.GetComponent<Button>().onClick.RemoveAllListeners();
            
            upgrade2 = selectedPanel.transform.Find("Upgrade 2").gameObject;
            upgrade2.GetComponent<Button>().onClick.RemoveAllListeners();

            //show the range of the selected tower
            selected.rangeDisplay.SetActive(true);

            //show the selected tower's info
            selectedPanel.SetActive(true);

            //show the tower's name
            selectedPanel.transform.Find("Tower Name").GetComponent<Text>().text = selected.name;
            
            //set what upgrade the buttons link to            
            upgrade1.GetComponent<Button>().onClick.AddListener(selected.upgradePath1[selected.path1Purchased].AddUpgrades);            
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
