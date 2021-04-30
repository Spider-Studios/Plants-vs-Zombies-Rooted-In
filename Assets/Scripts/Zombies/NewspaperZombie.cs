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

            if (currentSpeed < 1)
            {
                currentSpeed = 1;
            }
            

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
               StartCoroutine(ColourTimer());
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            distanceTravelled += Vector2.Distance(transform.position, previousPosition);
            previousPosition = transform.position;
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "House")
            {
                BrainsTracker.instance.Minushealth(damageToPlayer);
                Destroy(gameObject);
                if (BrainsTracker.instance.brains < 0)
                {
                    BrainsTracker.instance.brains = 0;
                }
            }
        }
    }
}
