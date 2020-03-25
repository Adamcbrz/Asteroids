using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetWithOffset : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void Update()
    {
        transform.position = target.position + offset;
    }
}
