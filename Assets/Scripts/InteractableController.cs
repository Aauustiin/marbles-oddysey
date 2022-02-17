using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] NPCData npcData;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            uiManager.DisplayDialog(npcData.dialog);
        }
    }
}
