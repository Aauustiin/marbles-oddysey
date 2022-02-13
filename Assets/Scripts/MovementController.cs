using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collisionInfo.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collisionInfo.transform.SetParent(transform.parent.parent);
        }
    }
}
