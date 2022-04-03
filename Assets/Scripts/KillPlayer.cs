using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<PlayerController>().OnDeath();
        }
    }
}
