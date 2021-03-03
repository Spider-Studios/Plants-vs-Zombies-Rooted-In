using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvZRI.Towers
{
    public class SightRange : MonoBehaviour
    {
        Tower parent = null;
        private void Start()
        {
            parent = transform.parent.GetComponent<Tower>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Zombie")
            {
                //if the new object isnt in the list, add it
                if (!parent.targets.Contains(collision.gameObject))
                {
                    parent.targets.Add(collision.gameObject);
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            parent.targets.Remove(collision.gameObject);
        }
    }
}
