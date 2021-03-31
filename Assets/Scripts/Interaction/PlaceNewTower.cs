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
        Tower towerToSpawn = null;
        bool showSprite = false;
        GameObject sprite = null;

        public GameObject rangeDisp;
        GameObject disp;

        SunTracker sunTracker;
        void Start()
        {
            sunTracker = GetComponent<SunTracker>();
        }

        void Update()
        {
            // print(CheckWhatMouseIsOver());
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
                    disp.GetComponent<SpriteRenderer>().color = new Color(161, 161, 161, 0.5f);
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower();
                    }
                }
                else
                {
                    sprite.GetComponent<SpriteRenderer>().color = Color.red;
                    disp.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.5f);
                }
            }
        }

        public void ShowHoverSprite(Tower t)
        {
            towerToSpawn = t;
            if (sunTracker.HaveEnoughSun(towerToSpawn.cost))
            {
                sprite = new GameObject("hover sprite");
                sprite.AddComponent<SpriteRenderer>();
                sprite.GetComponent<SpriteRenderer>().sprite = towerToSpawn.GetComponent<SpriteRenderer>().sprite;
                disp = Instantiate(rangeDisp, sprite.transform);
                disp.transform.localScale = new Vector3(towerToSpawn.range * 2, towerToSpawn.range * 2, 0);
                showSprite = true;
            }
            else
            {
                print("Not enough sun");
            }
        }

        public void PlaceTower()
        {
            sunTracker.MinusSun(towerToSpawn.cost);
            showSprite = false;
            Destroy(sprite);
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = -6f;
            Tower t = Instantiate(towerToSpawn, spawnPosition, Quaternion.identity);
            t.name = t.name.Replace("(Clone)", "");
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
