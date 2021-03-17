﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PvZRI.Zombies
{
    public class ZombieControl : MonoBehaviour
    {
        public GameObject[] waypoints;
        [SerializeField]
        private float moveSpeed = 2f;
        private int waypointIndex = 0;

        public int health = 0;
        public int reward;
        public int damageToPlayer;
        SunTracker sunTracker;
        BrainsTracker brainsTracker;

        void Start()
        {
            GetWaypoints();
            transform.position = waypoints[waypointIndex].transform.position;
            sunTracker = GameObject.Find("GameMaster").GetComponent<SunTracker>();
            brainsTracker = GameObject.Find("GameMaster").GetComponent<BrainsTracker>();
        }

        public void GetWaypoints()
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        }

        private void Update()
        {
            move();

            if (health <= 0)
            {
                Destroy(gameObject);
                sunTracker.AddSun(reward);
            }  
        }

        public void move()
        {
            if (waypointIndex <= waypoints.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

                Vector2 lookat = transform.right = -waypoints[waypointIndex].transform.position - -transform.position;

                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    waypointIndex += 1;
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == ("House"))
            {
                Destroy(gameObject);
                brainsTracker.Minushealth(damageToPlayer);
            }
        }
    }
}





