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
                selected = null;
            }

           if(selected != null)
            {
                ShowSelectedPanel();
            }
           else
            {
                HideSelectedPanel();
            }
        }

        private void ShowSelectedPanel()
        {
            selectedPanel.SetActive(true);
            selectedPanel.transform.Find("Tower Name").GetComponent<Text>().text = selected.name;
        }

        private void HideSelectedPanel()
        {
            selectedPanel.SetActive(false);
        }
    }
}
