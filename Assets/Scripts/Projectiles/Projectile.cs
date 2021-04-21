using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PvZRI.Zombies;

namespace PvZRI.Towers
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public float damage;
        public int health;
        public float slow;
        public float slowTime;
        public bool isOnFire = false;

        [Tooltip("What tower was the projectile fired from")]
        public Tower firedFrom = null;

        private void Start()
        {
            Destroy(gameObject, 5);
        }

        private void Update()
        {
          //GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == ("Zombie"))
            {
                ZombieControl hit = other.GetComponent<ZombieControl>();
                hit.health -= (int)damage;
                if(hit.health <= 0)
                {
                    firedFrom.killCount++;
                }

                health--;

                if(other.GetComponent<SpriteRenderer>().color == Color.white)
                    hit.hasBeenHit = true;

                if (slow > 0 && hit.isSlowed == false)
                {
                    hit.currentSpeed -= slow;
                    hit.isSlowed = true;
                    hit.slowTimer = slowTime;
                }

                if(isOnFire)
                {
                    hit.isSlowed = false;

                    hit.slowTimer = 0;
                }
            }
            
            if(other.tag == "Torchwood")
            {
                damage += other.transform.parent.GetComponent<TorchwoodScript>().damageIncrease;
                GetComponent<SpriteRenderer>().sprite = other.transform.parent.GetComponent<TorchwoodScript>().firePeaSprite;
                slow = 0;
                slowTime = 0;
                isOnFire = true;
            }
        }
    }
}
