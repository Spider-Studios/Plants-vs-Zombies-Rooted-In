using PvZRI.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Towers
{
    public class Tower : MonoBehaviour
    {
        public float range = 1;
        public float timeBetweenAttacks = 1;
        float timeSinceLastAttack;

        public List<GameObject> targets = new List<GameObject>();
        public Transform shootingAt = null;
        public Transform projectileSpawn = null;

        public GameObject projectile = null;

        public CircleCollider2D sightRange = null;

        public SelectTower gm = null;

        void Start()
        {
            sightRange = transform.Find("Sight Range").GetComponent<CircleCollider2D>();
            sightRange.radius = range;


            gm = GameObject.FindWithTag("GameMaster").GetComponent<SelectTower>();
        }

        void Update()
        {
            if (targets.Count == 0) shootingAt = null;

            LookAtTargets();
            if (shootingAt != null)
                ShootAtTarget(shootingAt);

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
                shot.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * 5;
            }
        }


        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
              gm.selected = this;
            }
        }
        
    }
}
