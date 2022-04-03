using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; set; }

    [SerializeField] bool[] trackCompletion;
    [SerializeField] int trackCount;

    private bool gameFinished;

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
        trackCompletion = new bool[trackCount];
        
        for(var i = 0; i < trackCompletion.Length; i++)
        {
            trackCompletion[i] = false;
        }

        gameFinished = false;
    }

    public void ParTimeAchieved(int trackID)
    {
        trackCompletion[trackID] = true;

        bool flag = true;
        foreach (var track in trackCompletion)
        {
            flag = flag && track;
        }

        if (flag && !gameFinished)
        {
            gameFinished = true;
            StartCoroutine(UIManager.Instance.ShowGameEndScreen());
        }
    }
}
