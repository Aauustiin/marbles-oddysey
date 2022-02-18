using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Vector3 collisionNormal; // The normal of the last surface we collided with.
    bool grounded;
    int health = 3;
    Ray ray;
    RaycastHit hitData;

    [SerializeField] float brakeDrag, normalDrag, jumpSpeed, groundedThreshold;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        UIManager.Instance.SetHealth(health);
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

    public void OnJump(InputAction.CallbackContext value)
    {
        if (grounded & value.performed)
        {
            playerRigidbody.velocity += jumpSpeed * collisionNormal;
        }
        else if (value.canceled)
        {
            playerRigidbody.velocity -= Vector3.Dot(playerRigidbody.velocity, collisionNormal) * collisionNormal;
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

    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            OnDeath();
        }
        UIManager.Instance.SetHealth(health);
    }

    public void Heal()
    {
        if (health < 3)
        {
            health++;
        }
        UIManager.Instance.SetHealth(health);
    }

    public void FullHeal()
    {
        health = 3;
        UIManager.Instance.SetHealth(health);
    }

    public void OnDeath()
    {
        transform.position = MyGameManager.Instance.currentCheckpoint;
        health = 3;
        UIManager.Instance.SetHealth(health);
    }
}
