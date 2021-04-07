using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingFollower : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;

    void Start()
    {

    }

    void Update()
    {
        transform.position = objectToFollow.position + offset;
    }
}
