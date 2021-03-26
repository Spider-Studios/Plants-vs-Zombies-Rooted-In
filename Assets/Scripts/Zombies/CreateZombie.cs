using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateZombie : MonoBehaviour
{
    public GameObject zombieToSpawn;
    public Transform spawnLocation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(zombieToSpawn, spawnLocation.position, Quaternion.identity);
        }
    }
}
