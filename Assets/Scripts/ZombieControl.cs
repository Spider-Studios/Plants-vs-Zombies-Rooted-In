using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2f;
    private int waypointIndex = 0;

    public int health = 0;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        move();

        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.tag == ("Player"))
            {
                --health;
                Debug.Log("Health = " + health);
            }
    }

}





