using PvZRI.Towers;
using PvZRI.Zombies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingofFire : Projectile
{
    CircleCollider2D collider;
    public float expandSpeed;
    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        Destroy(gameObject,0.5f);
    }
    private void Update()
    {
        Vector3 scaleChange = new Vector3(expandSpeed, expandSpeed, expandSpeed);
        transform.localScale += scaleChange / 5;
        //collider.radius = transform.localScale.x * 2;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Zombie"))
        {
            ZombieControl hit = other.GetComponent<ZombieControl>();
            hit.health -= (int)damage;
            if (hit.health <= 0)
            {
                firedFrom.killCount++;
            }

            health--;

            if (other.GetComponent<SpriteRenderer>().color == Color.white)
                hit.hasBeenHit = true;
        }
    }
}
