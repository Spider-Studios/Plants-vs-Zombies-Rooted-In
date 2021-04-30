using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using PvZRI.Towers;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PvZRI.Interaction
{
    public class PlaceNewTower : MonoBehaviour
    {
        #region Singleton
        public static PlaceNewTower instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one instance of place new tower found");
                return;
            }
            instance = this;
        }
        #endregion

        Tower towerToSpawn = null;
        bool showSprite = false;
        GameObject sprite = null;

        public GameObject rangeDisp;
        GameObject disp;

        SunTracker sunTracker;

        void Start()
        {
            sunTracker = SunTracker.instance;
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

                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Destroy(GameObject.Find("hover sprite"));
                    showSprite = false;
                }
            }
        }

        public void ShowHoverSprite(Tower t)
        {
            towerToSpawn = t;
            if (sunTracker.HaveEnoughSun(towerToSpawn.cost))
            {
                //stops a bug where there can be multiple hover sprites
                if(GameObject.Find("hover sprite"))
                {
                    Destroy(GameObject.Find("hover sprite"));
                }
                sprite = new GameObject("hover sprite");
                sprite.AddComponent<SpriteRenderer>();
                sprite.GetComponent<SpriteRenderer>().sprite = towerToSpawn.GetComponent<SpriteRenderer>().sprite;
                disp = Instantiate(rangeDisp, sprite.transform);
                disp.transform.localScale = new Vector3(towerToSpawn.range * 2, towerToSpawn.range * 2, 0);
                showSprite = true;
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

        public void DestroyHoverSprite()
        {
            Destroy(GameObject.Find("hover sprite"));
        }

        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
