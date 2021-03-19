using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PvZRI.Zombies;

namespace PvZRI.Towers
{
    public class Projectile : MonoBehaviour
    {
        public float damage;
        public int health;

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
                other.GetComponent<ZombieControl>().health -= (int)damage;

                health--;

                if(other.GetComponent<SpriteRenderer>().color == Color.white)
                other.GetComponent<ZombieControl>().hasBeenHit = true;
            }
        }
    }
}
