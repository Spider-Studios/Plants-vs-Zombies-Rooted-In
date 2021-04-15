using PvZRI.Interaction;
using PvZRI.Towers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchwoodScript : Tower
{
    public float damageIncrease;
    public Sprite firePeaSprite;

    void Start()
    {
        sightRange = transform.Find("Sight Range").GetComponent<CircleCollider2D>();
        rangeDisplay = sightRange.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        if (projectile != null)
        {
            if (targets.Count == 0) shootingAt = null;

            LookAtTargets();
            if (shootingAt != null)
                CreateRing();
        }

        if (SelectTower.instance.selected != this)
        {
            rangeDisplay.SetActive(false);
        }
        sightRange.radius = range;
        rangeDisplay.transform.localScale = new Vector3(range * 2, range * 2, 0);
    }

    public void CreateRing()
    {
        if (Time.time - timeSinceLastAttack > 1 / timeBetweenAttacks)
        {
            timeSinceLastAttack = Time.time;
            Projectile shot = Instantiate(projectile, new Vector3(projectileSpawn[0].position.x, projectileSpawn[0].position.y, -6), projectileSpawn[0].transform.rotation);
            Projectile proj = shot.GetComponent<Projectile>();
            proj.damage = damage;
            proj.speed = projectileSpeed;
            proj.health = projectileHealth;
            proj.slow = slowAmount;
            proj.slowTime = slowTime;
            proj.firedFrom = this;
        }
    }

    private void OnMouseOver()
    {
        if (canBePlacedOn.ToString() != "Track")
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectTower.instance.selected = this;
                SelectTower.instance.ShowSelectedPanel();
            }
        }
    }
}
