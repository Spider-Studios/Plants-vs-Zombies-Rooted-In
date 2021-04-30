using PvZRI.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PvZRI.Zombies;
using System.Text;

namespace PvZRI.Towers
{
    public class Tower : MonoBehaviour
    {
        [Tooltip("Cost of the plant")]
        public int cost;
        public int sellValue;
        [Multiline]
        public string description;

        [Space]
        [Header("Attacking")]
        public float range = 1;
        public float attackSpeed = 1;
        [HideInInspector]
        public float timeSinceLastAttack;
        public enum targetingType { first, last, strongest, weakest };
        public targetingType targeting;
        public bool rotates = true;

        [Space]
        [Header("Projectile")]
        public Projectile projectile = null;
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
        public bool u1Purchased, u2Purchased, u3Purchased, u4Purchased = false;

        public int killCount = 0;
        
        public List<GameObject> targets = new List<GameObject>();
        public Transform shootingAt = null;

        [HideInInspector]
        public CircleCollider2D sightRange = null;

        SelectTower selectTower = null;

        [HideInInspector]
        public GameObject rangeDisplay = null;

        void Start()
        { 
            sightRange = transform.Find("Sight Range").GetComponent<CircleCollider2D>();

            rangeDisplay = sightRange.transform.GetChild(0).gameObject;

            selectTower = GameObject.FindWithTag("GameMaster").GetComponent<SelectTower>();

            sellValue = cost / 2;
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

            if (selectTower.selected != this)
            {
                rangeDisplay.SetActive(false);
            }



            sightRange.radius = range;
            rangeDisplay.transform.localScale = new Vector3(range * 2, range * 2, 0);
        }

        public void LookAtTargets()
        {
            //if the list is not empty
            if (targets.Count != 0)
            {
                GameObject t = targets[0];
                for (int i = 0; i < targets.Count; i++)
                {
                    //target the zombie that has moved the furthest
                    if (targeting.ToString() == ("first"))
                    {
                        if (t.GetComponent<ZombieControl>().distanceTravelled < targets[i].GetComponent<ZombieControl>().distanceTravelled)
                        {
                            t = targets[i];
                        }
                    }

                    //target the zombie that has moved the least
                    if (targeting.ToString() == ("last"))
                    {
                        if (t.GetComponent<ZombieControl>().distanceTravelled > targets[i].GetComponent<ZombieControl>().distanceTravelled)
                        {
                            t = targets[i];
                        }
                    }

                    ////target the zombie with the most health
                    //if (targeting.ToString() == ("strongest"))
                    //{
                    //    print("A");
                    //    if (t.GetComponent<ZombieControl>().distanceTravelled < targets[i].GetComponent<ZombieControl>().distanceTravelled)
                    //    {
                    //        if (t.GetComponent<ZombieControl>().health > targets[i].GetComponent<ZombieControl>().health)
                    //        {

                    //            t = targets[i];
                    //        }
                    //    }
                    //}

                    ////target the zombie with the least health
                    //if (targeting.ToString() == ("weakest"))
                    //{
                    //    if (t.GetComponent<ZombieControl>().health < targets[i].GetComponent<ZombieControl>().health)
                    //    {
                    //        t = targets[i];
                    //    }
                    //}
                }

                shootingAt = t.transform;

                if (rotates)
                {
                    Vector3 lookat = transform.right = shootingAt.position - transform.position;
                    lookat.z = 0;

                transform.right = lookat;
                }
                //shoot 
            }
        }

        public void ShootAtTarget(Transform target)
        {
            if (Time.time - timeSinceLastAttack > 1 / attackSpeed)
            {
                timeSinceLastAttack = Time.time;
                for (int i = 0; i < projectileSpawn.Length; i++)
                {
                    Projectile shot = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].transform.rotation);
                    //shot.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projectileSpeed;
                    shot.GetComponent<Rigidbody2D>().velocity = projectileSpawn[i].right * projectileSpeed;
                    Projectile proj = shot.GetComponent<Projectile>();
                    proj.damage = damage;
                    proj.speed = projectileSpeed;
                    proj.health = projectileHealth;
                    proj.slow = slowAmount;
                    proj.slowTime = slowTime;
                    proj.firedFrom = this;
                }
            }
        }

        public void TargetFirst()
        {
            targeting = targetingType.first;
        }

        public void TargetLast()
        {
            targeting = targetingType.last;
        }

        public string GetShopInfoText()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(cost + " Sun").AppendLine();
            builder.Append(description);
            return builder.ToString();
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
