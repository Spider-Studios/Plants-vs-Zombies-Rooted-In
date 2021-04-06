using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateZombie : MonoBehaviour
{
    public GameObject zombieToSpawn, zombieToSpawn2;
    public Transform spawnLocation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(zombieToSpawn, spawnLocation.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(zombieToSpawn2, spawnLocation.position, Quaternion.identity);
        }
    }
}
