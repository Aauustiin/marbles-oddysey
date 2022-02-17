using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public Vector3 currentCheckpoint;
    int maxCollectables = 1;
    int currentCollectables = 0;
    [SerializeField] UIManager uiManager;

    void Start()
    {
        uiManager.SetCollectables(currentCollectables, maxCollectables);
    }

    public void FoundCollectable()
    {
        currentCollectables++;
        uiManager.SetCollectables(currentCollectables, maxCollectables);
    }
}
