using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Zombies
{
    public class DancingZombie : ZombieControl
    {

        [SerializeField]
        private GameObject followerPrefab;

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

            //creating waypoints to follow zombie, spawn 4 dancing zombies behind it at selected points on the map 

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
        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Dancing Spawn")
            {
                //spawn followers
                GameObject spawned = Instantiate(followerPrefab, transform.GetChild(0).transform.position, Quaternion.identity, null);
                spawned.GetComponent<ZombieControl>().waypointIndex = this.waypointIndex -1;
            }
        }
    }
}

