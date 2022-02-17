using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public bool found = false;

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            found = true;
            MyGameManager.Instance.FoundCollectable();
        }
    }
}
