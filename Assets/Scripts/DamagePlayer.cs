using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<PlayerController>().TakeDamage();
        }
    }
}
