using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithOffset : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;

    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
