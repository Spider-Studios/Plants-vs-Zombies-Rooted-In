using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Zombies
{
    public class DancingZombie : ZombieControl
    {
        SunTracker sunTracker;
        BrainsTracker brainsTracker;
        // Start is called before the first frame update
        void Start()
        {
            GetWaypoints();
            transform.position = waypoints[waypointIndex].transform.position;
            sunTracker = GameObject.Find("GameMaster").GetComponent<SunTracker>();
            brainsTracker = GameObject.Find("GameMaster").GetComponent<BrainsTracker>();
        }

        // Update is called once per frame
        void Update()
        {
            move();

            //creating waypoints to follow zombie, spawn 4 dancing zombies behind it at selected points on the map 

            if (health <= 0)
            {
                Destroy(gameObject);
                sunTracker.AddSun(reward);
            }

            if (hasBeenHit == true)
            {
                StartCoroutine(timer());
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}

