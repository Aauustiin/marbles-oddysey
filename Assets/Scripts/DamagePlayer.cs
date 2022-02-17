using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float bonkMagnitude;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.TakeDamage();
            Vector3 bonkDirection = collisionInfo.transform.position - transform.position;
            collisionInfo.rigidbody.AddForce((bonkDirection.normalized + transform.up * 0.5f) * bonkMagnitude, ForceMode.Impulse);
        }
    }
}
