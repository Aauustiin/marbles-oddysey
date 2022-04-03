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

    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float clickVolume;

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
        audioSource.PlayOneShot(clickSFX, clickVolume);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        audioSource.PlayOneShot(clickSFX, clickVolume);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // TIMER UI

    [SerializeField] private GameObject stopwatchParent;
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
            stopwatchParent.SetActive(false);
            stopwatch.enabled = false;
        }
        else
        {
            stopwatchParent.SetActive(true);
            stopwatch.enabled = true;
            stopwatch.text = text;
        }
    }

    // RACE END SCREEN

    [SerializeField] private GameObject raceEndScreen;
    [SerializeField] private TextMeshProUGUI finishTimeText, parTimeText;
    private RaceTrack currentRaceTrack;
    private bool raceEndScreenShowing;

    public void ShowRaceEndScreen(float time, float parTime, RaceTrack raceTrack)
    {
        raceEndScreenShowing = true;
        Time.timeScale = 0f;
        currentRaceTrack = raceTrack;
        raceEndScreen.SetActive(true);
        float roundedFinishTime = Mathf.Round(time * 100f) * 0.01f;

        string smiley = "";
        if (time <= parTime)
        {
            smiley = " :)";
        }

        finishTimeText.text = "Final Time: " + roundedFinishTime.ToString() + "s" + smiley;
        parTimeText.text = "Par Time: " + parTime.ToString() + "s";
    }

    public void RetryRace()
    {
        audioSource.PlayOneShot(clickSFX, clickVolume);
        raceEndScreenShowing = false;
        Time.timeScale = 1f;
        raceEndScreen.SetActive(false);
        currentRaceTrack.InitializeRace();
    }

    public void ContinueFromRace()
    {
        audioSource.PlayOneShot(clickSFX, clickVolume);
        raceEndScreenShowing = false;
        Time.timeScale = 1f;
        raceEndScreen.SetActive(false);
        currentRaceTrack.MovePlayerToStartPosition();
    }

    // GAME END SCREEN

    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private AudioClip endingSFX;
    [SerializeField] private float endingVolume;

    public IEnumerator ShowGameEndScreen()
    {
        if (raceEndScreenShowing)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(ShowGameEndScreen());
        }
        else
        {
            Time.timeScale = 0f;
            gameEndScreen.SetActive(true);
            audioSource.PlayOneShot(endingSFX, endingVolume);
        }
    }

    public void ContinueFromEndScreen()
    {
        audioSource.PlayOneShot(clickSFX, clickVolume);
        Time.timeScale = 1f;
        gameEndScreen.SetActive(false);
    }
}
