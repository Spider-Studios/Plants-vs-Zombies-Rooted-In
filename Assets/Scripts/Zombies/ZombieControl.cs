using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PvZRI.Zombies
{
    public class ZombieControl : MonoBehaviour
    {
        [SerializeField] Transform waypointParent;
        private GameObject[] waypoints;
        [SerializeField]
        private float moveSpeed = 2f;
        private int waypointIndex = 0;

        public int health = 0;

        private void Start()
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            transform.position = waypoints[waypointIndex].transform.position;
        }

        private void Update()
        {
            move();

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void move()
        {
            if (waypointIndex <= waypoints.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    waypointIndex += 1;
                }
            }
        }
    }
}





