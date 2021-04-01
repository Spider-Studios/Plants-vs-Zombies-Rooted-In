using PvZRI.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PvZRI.Zombies;

namespace PvZRI.Towers
{
    public class Tower : MonoBehaviour
    {
        [Tooltip("Cost of the plant")]
        public int cost;

        [Space]
        [Header("Attacking")]
        public float range = 1;
        public float timeBetweenAttacks = 1;
        float timeSinceLastAttack;

        [Space]
        [Header("Projectile")]
        public GameObject projectile = null;
        public float damage;
        public float projectileSpeed;
        [Tooltip("How many zombies can the projectile pass through")]
        public int projectileHealth;
        public float slowAmount = 0;
        public float slowTime;
        public Transform[] projectileSpawn = new Transform[1];

        [Tooltip("Sun given at the end of each wave")]
        public int sunReward = 0;

        [SerializeField]
        public enum CanBePlacedOn { Grass, Water, Track };
        [Space]
        public CanBePlacedOn canBePlacedOn;

        [Space]
        [Header("Upgrades")]
        public Upgrade[] upgradePath1;

        public int path1Purchased;

        public int killCount = 0;

        [HideInInspector]
        public List<GameObject> targets = new List<GameObject>();
        Transform shootingAt = null;
                
        CircleCollider2D sightRange = null;

        SelectTower selectTower = null;

        [HideInInspector]
        public GameObject rangeDisplay = null;

        void Start()
        {
            sightRange = transform.Find("Sight Range").GetComponent<CircleCollider2D>();

            rangeDisplay = sightRange.transform.GetChild(0).gameObject;

            selectTower = GameObject.FindWithTag("GameMaster").GetComponent<SelectTower>();

               
        }

        void Update()
        {
            if (projectile != null)
            {
                if (targets.Count == 0) shootingAt = null;

                LookAtTargets();
                if (shootingAt != null)
                    ShootAtTarget(shootingAt);
            }

            if(selectTower.selected != this)
            {
                rangeDisplay.SetActive(false);
            }

            

            sightRange.radius = range;
            rangeDisplay.transform.localScale = new Vector3(range * 2, range * 2, 0);
        }

        private void LookAtTargets()
        {
            //if the list is not empty
            if (targets.Count != 0)
            {
                GameObject t = targets[0];
                for (int i = 0; i < targets.Count; i++)
                {
                    //target the zombie that has moved the furthest
                    if (t.GetComponent<ZombieControl>().distanceTravelled < targets[i].GetComponent<ZombieControl>().distanceTravelled)
                    {
                        t = targets[i];
                    }
                }

                shootingAt = t.transform;

                Vector3 lookat = transform.right = shootingAt.position - transform.position;
                lookat.z = 0;

                transform.right = lookat;
                //shoot 
            }
        }

        public void ShootAtTarget(Transform target)
        {
            if (Time.time - timeSinceLastAttack > 1 / timeBetweenAttacks)
            {
                timeSinceLastAttack = Time.time;
                for (int i = 0; i < projectileSpawn.Length; i++)
                {
                    GameObject shot = Instantiate(projectile, projectileSpawn[i].position, Quaternion.identity);
                    shot.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projectileSpeed;
                    Projectile proj = shot.GetComponent<Projectile>();
                    proj.damage = damage;
                    proj.health = projectileHealth;
                    proj.slow = slowAmount;
                    proj.slowTime = slowTime;
                    proj.firedFrom = this;
                }
            }
        }

        private void OnMouseOver()
        {
            if (canBePlacedOn.ToString() != "Track")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectTower.selected = this;
                    selectTower.ShowSelectedPanel();
                }
            }
        }

    }
}
