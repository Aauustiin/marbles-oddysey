using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    [SerializeField] RaceTrack raceTrack;

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            raceTrack.InitializeRace();
        }
    }
}
