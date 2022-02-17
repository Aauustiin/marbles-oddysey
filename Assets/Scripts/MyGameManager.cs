using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance { get; set; }

    public Vector3 currentCheckpoint;
    int maxCollectables = 1;
    int currentCollectables = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UIManager.Instance.SetCollectables(currentCollectables, maxCollectables);
    }

    public void FoundCollectable()
    {
        currentCollectables++;
        UIManager.Instance.SetCollectables(currentCollectables, maxCollectables);
    }
}
