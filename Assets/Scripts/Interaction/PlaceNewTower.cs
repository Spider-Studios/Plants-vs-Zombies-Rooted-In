using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using PvZRI.Towers;

namespace PvZRI.Interaction
{
    public class PlaceNewTower : MonoBehaviour
    {
        [SerializeField] Tower towerToSpawn = null;
        [SerializeField] bool showSprite = false;
        GameObject sprite = null;
        void Start()
        {

        }

        void Update()
        {
            if (sprite != null)
            {
                if (sprite.active)
                {
                    sprite.active = showSprite;
                    sprite.transform.localScale = towerToSpawn.transform.localScale;
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    position.z = -0.2f;
                    sprite.transform.position = position;
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                HoverTower();
            }
        }

        private void HoverTower()
        {
            sprite = new GameObject("hover sprite");
            sprite.AddComponent<SpriteRenderer>();
            sprite.GetComponent<SpriteRenderer>().sprite = towerToSpawn.GetComponent<SpriteRenderer>().sprite;            
            showSprite = true;
        }

        private void PlaceTower()
        {

            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = -0.2f;
            Instantiate(towerToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
