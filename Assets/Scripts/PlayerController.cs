using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    [SerializeField] private float START_JUMP_SPEED; // The player's jump velocity when they press the jump button.
    [SerializeField] private float END_JUMP_SPEED; //  The player's jump velocity when they release the jump button.
    [SerializeField] private float GROUNDED_THRESHOLD; // The distance from the ground beneath which the player is considered grounded.
    [SerializeField] MyGameManager gameManager;

    Vector3 collisionNormal; // The normal of the last surface we collided with.
    bool grounded;
    int health = 3;

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
        if (grounded & value.performed)
        {
            playerRigidbody.velocity += START_JUMP_SPEED * collisionNormal;
        }
    }

    public void OnJumpCancel(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            playerRigidbody.velocity += (END_JUMP_SPEED - Vector3.Dot(playerRigidbody.velocity, collisionNormal)) * collisionNormal;
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
    }

    public void Heal()
    {
        if (health < 3)
        {
            health++;
        }
    }

    public void FullHeal()
    {
        health = 3;
    }

    public void OnDeath()
    {
        transform.position = gameManager.currentCheckpoint;
        health = 3;
    }
}
