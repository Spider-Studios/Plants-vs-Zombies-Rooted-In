using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PvZRI.Zombies
{
    public class ZombieControl : MonoBehaviour
    {
        public GameObject[] waypoints;
        public GameObject waypointParent;
        [HideInInspector]
        public float currentSpeed = 2f;
        public float moveSpeed = 2f;
        public int waypointIndex = 0;

        public int health = 0;
        public bool hasBeenHit = false;
        public float colourTimer = 0;
        public bool isSlowed = false;
        public float slowTimer;
        public int reward;
        public int damageToPlayer;
        public AudioSource zombieHitSound;

        public float distanceTravelled = 0;
        [HideInInspector]
        public Vector3 previousPosition;

        SunTracker sunTracker;
        BrainsTracker brainsTracker;

        void Start()
        {
            GetWaypoints();
            transform.position = waypoints[waypointIndex].transform.position;
            sunTracker = GameObject.Find("GameMaster").GetComponent<SunTracker>();
            brainsTracker = GameObject.Find("GameMaster").GetComponent<BrainsTracker>();
            previousPosition = transform.position;
        }

        public void GetWaypoints()
        {
            waypointParent = GameObject.FindGameObjectWithTag("Waypoints");
            for (int i = 0; i < waypointParent.transform.childCount; i++)
            {
                waypoints[i] = waypointParent.transform.GetChild(i).gameObject;
            }
            //waypointParent.transform.GetChild(i);
            //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        }

        private void Update()
        {
            move();

            if (health <= 0)
            {
                Destroy(gameObject);
                sunTracker.AddSun(reward);
            }

            if (hasBeenHit == true)
            {
                StartCoroutine(ColourTimer());
                zombieHitSound.Play();
            }
            else 
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            if(isSlowed)
            {
                StartCoroutine(SlowTimer());
                GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else
            {
                currentSpeed = moveSpeed;
            }

            distanceTravelled += Vector2.Distance(transform.position, previousPosition);
            previousPosition = transform.position;
        }

        public void move()
        {
            if (waypointIndex <= waypoints.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, currentSpeed * Time.deltaTime);
                Vector2 lookat = transform.right = -waypoints[waypointIndex].transform.position - -transform.position;

                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    waypointIndex += 1;
                }
            }
        }

        public IEnumerator ColourTimer()
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(colourTimer);
            hasBeenHit = false;
        }

        public IEnumerator SlowTimer()
        {
            yield return new WaitForSeconds(slowTimer);
            isSlowed = false;
            slowTimer = 0;
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "House")
            {
                brainsTracker.Minushealth(damageToPlayer);
                Destroy(gameObject);
                if (brainsTracker.brains < 0)
                {
                    brainsTracker.brains = 0;
                }
            }
        }
    }
}





