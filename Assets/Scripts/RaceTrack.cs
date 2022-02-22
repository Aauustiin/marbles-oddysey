using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private GameObject startLine, finishLine;

    private float startTime;
    private bool raceUnderway;

    public void InitializeRace()
    {
        finishLine.SetActive(true);
        startLine.SetActive(false);
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        FindObjectOfType<PlayerController>().transform.position = startPosition;
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
        UIManager.Instance.UpdateStopwatch("");
        UIManager.Instance.ShowRaceEndScreen(Time.time - startTime, this);
    }
}
