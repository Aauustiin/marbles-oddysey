using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

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
        countdown.enabled = false;
        stopwatch.enabled = false;
    }

    // PAUSING

    [SerializeField] private GameObject pauseMenu;
    private bool paused = false;

    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // TIMER UI

    [SerializeField] private TextMeshProUGUI countdown, stopwatch;

    public void UpdateCountdown(string text)
    {
        if (text == "")
        {
            countdown.enabled = false;
        }
        else
        {
            countdown.enabled = true;
            countdown.text = text;
        }
    }

    public void UpdateStopwatch(string text)
    {
        if (text == "")
        {
            stopwatch.enabled = false;
        }
        else
        {
            stopwatch.enabled = true;
            stopwatch.text = text;
        }
    }

    // RACE END SCREEN

    [SerializeField] private GameObject raceEndScreen;
    [SerializeField] private TextMeshProUGUI timeText;
    private RaceTrack currentRaceTrack;

    public void ShowRaceEndScreen(float time, RaceTrack raceTrack)
    {
        Time.timeScale = 0f;
        currentRaceTrack = raceTrack;
        raceEndScreen.SetActive(true);
        float roundedTime = Mathf.Round(time * 100f) * 0.01f;
        timeText.text = "Final Time: " + roundedTime.ToString() + "s";
    }

    public void RetryRace()
    {
        Time.timeScale = 1f;
        raceEndScreen.SetActive(false);
        currentRaceTrack.InitializeRace();
    }

    public void ContinueFromRace()
    {
        Time.timeScale = 1f;
        raceEndScreen.SetActive(false);
    }

    

    //[SerializeField] GameObject dialogBox
    //[SerializeField] TextMeshProUGUI dialogText;

    //public void DisplayDialog(string text)
    //{
    //    dialogBox.SetActive(true);
    //    dialogText.text = text;
    //    StartCoroutine(RemoveAfterSeconds(5, dialogBox));
    //}

    //IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    obj.SetActive(false);
    //}
}
