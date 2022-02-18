using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField] GameObject dialogBox, pauseMenu;
    [SerializeField] TextMeshProUGUI dialogText, healthText, collectablesText;
    bool paused = false;

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

    public void DisplayDialog(string text)
    {
        dialogBox.SetActive(true);
        dialogText.text = text;
        StartCoroutine(RemoveAfterSeconds(5, dialogBox));
    }

    public void SetHealth(int health)
    {
        switch (health)
        {
            case 1:
                healthText.text = "<3";
                break;
            case 2:
                healthText.text = "<3 <3";
                break;
            case 3:
                healthText.text = "<3 <3 <3";
                break;
            default:
                Debug.Log("Error: SetHealth() was passed an invalid value");
                break;
        }
    }

    public void SetCollectables(int currentCollectables, int maxCollectables)
    {
        collectablesText.text = currentCollectables + "/" + maxCollectables;
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
