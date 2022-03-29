using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float parTime;
    [SerializeField] private int trackID;
    [SerializeField] private GameObject startLine, finishLine, parIndicator;

    private float startTime;
    private bool raceUnderway;

    public void InitializeRace()
    {
        FindObjectOfType<PlayerController>().RespawnPoint = startPosition;
        finishLine.SetActive(true);
        startLine.SetActive(false);
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        MovePlayerToStartPosition();
        FindObjectOfType<PlayerController>().Freeze();

        UIManager.Instance.UpdateCountdown("3");
        yield return new WaitForSeconds(1);
        UIManager.Instance.UpdateCountdown("2");
        yield return new WaitForSeconds(1);
        UIManager.Instance.UpdateCountdown("1");
        yield return new WaitForSeconds(1);
        UIManager.Instance.UpdateCountdown("Go!");

        FindObjectOfType<PlayerController>().Unfreeze();
        StartRace();

        yield return new WaitForSeconds(1);
        UIManager.Instance.UpdateCountdown("");
    }

    public void MovePlayerToStartPosition()
    {
        FindObjectOfType<PlayerController>().transform.position = startPosition;
    }

    public void StartRace()
    {
        
        startTime = Time.time;
        raceUnderway = true;
    }

    void Update()
    {
        if (raceUnderway) {
            UIManager.Instance.UpdateStopwatch((Time.time - startTime).ToString());
        }
    }

    public void EndRace()
    {
        startLine.SetActive(true);
        finishLine.SetActive(false);
        raceUnderway = false;
        float finishTime = Time.time - startTime;
        FindObjectOfType<PlayerController>().RespawnPoint = Vector3.zero;
        UIManager.Instance.UpdateStopwatch("");
        UIManager.Instance.ShowRaceEndScreen(finishTime, parTime, this);
        if (finishTime <= parTime)
        {
            parTimeAchieved();
        }
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void parTimeAchieved()
    {
        ProgressionManager.Instance.ParTimeAchieved(trackID);
        parIndicator.SetActive(true);
    }
}
