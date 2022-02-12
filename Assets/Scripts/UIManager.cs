using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (!paused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
            }

        }
    }
}
