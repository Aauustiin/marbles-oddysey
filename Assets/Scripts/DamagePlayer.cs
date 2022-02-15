using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] PlayerController player;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.TakeDamage();
        }
    }
}
