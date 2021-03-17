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
            selected.rangeDisplay.SetActive(true);

            selectedPanel.SetActive(true);
            selectedPanel.transform.Find("Tower Name").GetComponent<Text>().text = selected.name;
        }

        private void HideSelectedPanel()
        {
            selected.rangeDisplay.SetActive(false);
            selectedPanel.SetActive(false);
            selected = null;
        }
    }
}
