using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool Paused = false;
    public GameObject PauseMenu;
    float tempSensitivity;


    private PlayerLook PlayerLook;

    void Start()
    {
        PlayerLook = GetComponent<PlayerLook>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerLook.sensitivity = tempSensitivity;

        Paused = false;
    }

    void PauseGame()
    {
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        tempSensitivity = PlayerLook.sensitivity;
        PlayerLook.sensitivity = 0.0f;

        Paused = true;
    }

    public void ResumeButtonPressed()
    {
        ResumeGame();
    }
}
