using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Zombies
{
    public class Gargantuar : ZombieControl
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

            //if (health <= 10)
            //{
                //spawn in Imp
            //}

            if (health <= 0)
            {
                Destroy(gameObject);
                sunTracker.AddSun(reward);
            }

            if (hasBeenHit == true)
            {
               // StartCoroutine(timer());
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
