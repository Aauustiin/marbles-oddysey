using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float parTime;
    [SerializeField] private int trackID;
    [SerializeField] private GameObject startLine, finishLine, parIndicator;

    [SerializeField] private AudioClip countdownSFX, finishSFX, parSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float countdownVolume, finishVolume, parVolume;

    private float startTime;
    private bool raceUnderway;

    public void InitializeRace()
    {
        EventManager.PlayerDeath += onPlayerDeath;
        FindObjectOfType<LevelController>().transform.rotation = Quaternion.Euler(Vector3.zero);
        FindObjectOfType<PlayerController>().RespawnPoint = startPosition;
        finishLine.SetActive(true);
        startLine.SetActive(false);
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        MovePlayerToStartPosition();
        FindObjectOfType<PlayerController>().Freeze();

        audioSource.PlayOneShot(countdownSFX, countdownVolume);
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
            float roundedTime = Mathf.Round((Time.time - startTime) * 100f) * 0.01f;
            string stringTime = (roundedTime).ToString();
            UIManager.Instance.UpdateStopwatch(stringTime);
        }
    }

    private void onPlayerDeath()
    {
        startTime = Time.time;
    }

    public void EndRace()
    {
        startLine.SetActive(true);
        finishLine.SetActive(false);
        raceUnderway = false;
        float finishTime = Time.time - startTime;
        audioSource.PlayOneShot(finishSFX, finishVolume);
        UIManager.Instance.UpdateStopwatch("");
        UIManager.Instance.ShowRaceEndScreen(finishTime, parTime, this);
        if (finishTime <= parTime)
        {
            audioSource.PlayOneShot(parSFX, parVolume);
            parTimeAchieved();
        }
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().velocity = Vector3.zero;
        EventManager.PlayerDeath -= onPlayerDeath;
    }

    void parTimeAchieved()
    {
        ProgressionManager.Instance.ParTimeAchieved(trackID);
        parIndicator.SetActive(true);
    }
}
