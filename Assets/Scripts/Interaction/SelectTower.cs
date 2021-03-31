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

        public GameObject upgrade1, upgrade2, upgrade3, upgrade4;

        void Start()
        {
            upgrade1 = selectedPanel.transform.Find("Upgrade 1").gameObject;
            upgrade2 = selectedPanel.transform.Find("Upgrade 2").gameObject;
            upgrade3 = selectedPanel.transform.Find("Upgrade 3").gameObject;
            upgrade4 = selectedPanel.transform.Find("Upgrade 4").gameObject;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (selected != null)
                {
                    HideSelectedPanel();
                }
            }

            if (selected != null)
            {

                selectedPanel.transform.Find("Kill Count").GetComponent<Text>().text = "Zombies Killed: " + selected.killCount;
                //set the buttons text to the next upgrade
                upgrade1.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath1[0].name + "\n Cost: " + selected.upgradePath1[0].cost;
                upgrade2.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath1[1].name + "\n Cost: " + selected.upgradePath1[1].cost;
                upgrade3.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath1[2].name + "\n Cost: " + selected.upgradePath1[2].cost;
                upgrade4.transform.GetChild(0).GetComponent<Text>().text = selected.upgradePath1[3].name + "\n Cost: " + selected.upgradePath1[3].cost;


                switch (selected.path1Purchased)
                {
                    case 1:
                        //if (sunTracker.sun >= selected.upgradePath1[0].cost)
                        //{
                        //    upgrade1.GetComponent<Button>().interactable = true;
                        //}
                        //else
                        //{
                        //    upgrade1.GetComponent<Button>().interactable = false;
                        //}
                        upgrade1.GetComponent<Button>().interactable = false;
                        upgrade2.GetComponent<Button>().interactable = true;
                        upgrade3.GetComponent<Button>().interactable = false;
                        upgrade4.GetComponent<Button>().interactable = false;
                        break;
                    case 2:
                        upgrade1.GetComponent<Button>().interactable = false;
                        upgrade2.GetComponent<Button>().interactable = false;
                        upgrade3.GetComponent<Button>().interactable = true;
                        upgrade4.GetComponent<Button>().interactable = false;
                        break;
                    case 3:
                        upgrade1.GetComponent<Button>().interactable = false;
                        upgrade2.GetComponent<Button>().interactable = false;
                        upgrade3.GetComponent<Button>().interactable = false;
                        upgrade4.GetComponent<Button>().interactable = true;
                        break;
                    case 0:
                        upgrade1.GetComponent<Button>().interactable = true;
                        upgrade2.GetComponent<Button>().interactable = false;
                        upgrade3.GetComponent<Button>().interactable = false;
                        upgrade4.GetComponent<Button>().interactable = false;
                        break;
                    default:
                        upgrade1.GetComponent<Button>().interactable = false;
                        upgrade2.GetComponent<Button>().interactable = false;
                        upgrade3.GetComponent<Button>().interactable = false;
                        upgrade4.GetComponent<Button>().interactable = false;
                        break;
                }
            }
        }

        public void ShowSelectedPanel()
        {
            //show the range of the selected tower
            selected.rangeDisplay.SetActive(true);

            //show the selected tower's info
            selectedPanel.SetActive(true);

            //show the tower's name
            selectedPanel.transform.Find("Tower Name").GetComponent<Text>().text = selected.name;
            
        }

        public void Button1Clicked()
        {
            selected.upgradePath1[0].AddUpgrades();
        }

        public void Button2Clicked()
        {
            selected.upgradePath1[1].AddUpgrades();            
        }

        public void Button3Clicked()
        {
            selected.upgradePath1[2].AddUpgrades();
        }

        public void Button4Clicked()
        {
            selected.upgradePath1[3].AddUpgrades();
        }

        private void HideSelectedPanel()
        {
            selected.rangeDisplay.SetActive(false);
            selectedPanel.SetActive(false);
            selected = null;
        }
    }
}
//TODO bug: clicking an upgrade button for a second time does not do the next upgrade. need to change the onclick somewhere else