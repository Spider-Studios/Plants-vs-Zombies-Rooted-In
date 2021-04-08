using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Zombies
{
    public class NewspaperZombie : ZombieControl
    {

        // Start is called before the first frame update
        void Start()
        {
            GetWaypoints();
            transform.position = waypoints[waypointIndex].transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            move();

            if (health <= 5)
            {
                moveSpeed = 3;
            }

            if (health <= 0)
            {
                Destroy(gameObject);
                SunTracker.instance.AddSun(reward);
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
