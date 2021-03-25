using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Zombies
{
    public class NewspaperZombie : ZombieControl
    {
        private int waypointIndex = 0;

        void Start()
        {
            GetWaypoints();
            transform.position = waypoints[waypointIndex].transform.position;
        }

        void Update()
        {
            move();

            if (health <= 5)
            {
                moveSpeed = 3;
            }
        }
    }
}
