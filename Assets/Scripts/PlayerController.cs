using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Ray ray;
    private RaycastHit hitData;
    private Vector3 collisionNormal; // The normal of the last surface we collided with.
    private bool grounded;
    public Vector3 RespawnPoint;

    [SerializeField] private float brakeDrag, normalDrag, jumpSpeed, groundedThreshold;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        RespawnPoint = Vector3.zero;
    }

    void Update()
    {
        ray = new Ray(transform.position, -collisionNormal);
    
        if (Physics.Raycast(ray, out hitData) & (hitData.distance < groundedThreshold))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
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
            // Of all the surfaces we are colliding with, we choose the one closest to being right underneath us.
            var d = Vector3.Distance(contacts[i].normal, transform.parent.up);
            if (d < lowestDistance)
            {
                lowestDistance = d;
                closestContact = i;
            }
        }
        collisionNormal = collisionInfo.GetContact(closestContact).normal;
    }

    public void OnDeath()
    {
        transform.position = RespawnPoint;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        playerRigidbody.velocity = Vector3.zero;
    }

    public void Freeze()
    {
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void Unfreeze()
    {
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (grounded & value.performed)
        {
            playerRigidbody.velocity += jumpSpeed * collisionNormal;
        }
        else if (value.canceled)
        {
            float jumpMagnitude = Vector3.Dot(playerRigidbody.velocity, collisionNormal);
            if (jumpMagnitude > 0)
            {
                playerRigidbody.velocity -= Vector3.Dot(playerRigidbody.velocity, collisionNormal) * collisionNormal;
            }
        }
    }

    public void OnBrake(InputAction.CallbackContext value)
    {
        if (grounded & value.performed)
        {
            playerRigidbody.drag = brakeDrag;
        }
        else if (value.canceled)
        {
            playerRigidbody.drag = normalDrag;
        }
    }
}
