using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    public float JUMP_THRUST;
    public float GROUNDED_THRESHOLD; // The distance from the ground beneath which the player is considered grounded.

    Vector3 collisionNormal; // The normal of the last surface we collided with.
    bool grounded;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, -collisionNormal);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData) & (hitData.distance < GROUNDED_THRESHOLD))
        {
            grounded = true;
        } else
        {
            grounded = false;
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (grounded)
        {
            playerRigidbody.AddForce(transform.parent.up * JUMP_THRUST);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        ContactPoint[] contacts = new ContactPoint[collisionInfo.contactCount];
        collisionInfo.GetContacts(contacts);
        float lowestDistance = 100f;
        int closestContact = 0;
        for (var i = 0; i < contacts.Length; i++)
        {
            var d = Vector3.Distance(contacts[i].normal, transform.parent.up);
            if (d < lowestDistance)
            {
                lowestDistance = d;
                closestContact = i;
            }
        }
        collisionNormal = collisionInfo.GetContact(closestContact).normal;
    }
}
