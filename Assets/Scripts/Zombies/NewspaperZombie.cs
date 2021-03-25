using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Zombies
{
    public class NewspaperZombie : ZombieControl
    {
        
        // Start is called before the first frame update
        void Start()
        {
            GetWaypoints();
        }

        // Update is called once per frame
        void Update()
        {
            if (health <= 5)
            {
                transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, 3f, Input.GetAxis("Vertical") * Time.deltaTime);
            }
        }
    }
}
