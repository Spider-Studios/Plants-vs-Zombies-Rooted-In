using PvZRI.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Towers
{
    public class Tower : MonoBehaviour
    {
        [Tooltip("Cost of the plant")]
        public int cost;

        [Space]
        public float range = 1;
        public float timeBetweenAttacks = 1;
        float timeSinceLastAttack;

        [Space]
        public GameObject projectile = null;
        public Transform projectileSpawn = null;

        [SerializeField]
        public enum CanBePlacedOn { Grass, Water, Track };
        [Space]
        public CanBePlacedOn canBePlacedOn;

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
            sightRange.radius = range;

            rangeDisplay = sightRange.transform.GetChild(0).gameObject;
            rangeDisplay.transform.localScale = new Vector3(range * 2, range * 2, 0);

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

        }

        private void LookAtTargets()
        {
            //if the list is not empty
            if (targets.Count != 0)
            {
                shootingAt = targets[0].transform;
                //look at the first target

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
                GameObject shot = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
                shot.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projectile.GetComponent<Projectile>().speed;
            }
        }


        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectTower.selected = this;
                selectTower.ShowSelectedPanel();
            }
        }

    }
}
