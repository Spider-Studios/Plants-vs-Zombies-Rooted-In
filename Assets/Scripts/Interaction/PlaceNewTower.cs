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
            print(CheckWhatMouseIsOver());
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

            if (showSprite)
            {
                if (CheckWhatMouseIsOver() == towerToSpawn.canBePlacedOn.ToString())
                {
                    sprite.GetComponent<SpriteRenderer>().color = Color.white;
                    if (Input.GetMouseButtonDown(1))
                    {
                        PlaceTower();
                    }
                }
                else
                {
                    sprite.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }

        public void ShowHoverSprite()
        {
            sprite = new GameObject("hover sprite");
            sprite.AddComponent<SpriteRenderer>();
            sprite.GetComponent<SpriteRenderer>().sprite = towerToSpawn.GetComponent<SpriteRenderer>().sprite;
            showSprite = true;
        }

        public void PlaceTower()
        {
            showSprite = false;
            Destroy(sprite);
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = -6f;
            Instantiate(towerToSpawn, spawnPosition, Quaternion.identity);
        }

        public string CheckWhatMouseIsOver()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit != false)
            {
                return hit.transform.tag;
            }
            return null;
        }
    }
}
