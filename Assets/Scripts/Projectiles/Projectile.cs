using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PvZRI.Zombies;

namespace PvZRI.Towers
{
    public class Projectile : MonoBehaviour
    {
        public int damage = 1;
        [Tooltip("How many zombies can the projectile pass through")]
        public int health = 1;
        public float speed = 1;

        [Tooltip("What tower was the projectile fired from")]
        public Tower firedFrom = null;

        private void Start()
        {
            Destroy(gameObject, 5);
        }

        private void Update()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == ("Zombie"))
            {
                other.GetComponent<ZombieControl>().health-= damage;

                health--;
            }
        }
    }
}
