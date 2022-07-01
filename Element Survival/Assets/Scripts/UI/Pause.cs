using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool Paused = false;

    public GameObject PauseMenu;
    public GameObject Settings;
    public GameObject Quit;
    public GameObject DSettings;
    public GameObject ASettings;
    public GameObject CSettings;
    public GameObject PauseButtons;

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
            //upon clicking escape, one of the two will occur, based on if the game is paused//
        }
    }

    void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Settings.SetActive(false);
        Quit.SetActive(false);
        DSettings.SetActive(false);
        ASettings.SetActive(false);
        CSettings.SetActive(false);
        PauseButtons.SetActive(true);
        //Ensures that the Pause menu closes correctly, and that the correct Menu appears upon re use//

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Makes the cursor invisible again, and locks it//

        Player.instance.canLook = true;
        //Resumes the camera rotation when the Pause menu fades//

        Paused = false;
        //Game is set as unpaused//
    }

    void PauseGame()
    {
        PauseMenu.SetActive(true);
        //Brings up the Pause menu//

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Unlocks the cursor, and makes it visible to click through the menus//

        Player.instance.canLook = false;
        //Locks camera rotaion for the duration of the pause//

        Paused = true;
        //Game is set as paused//
    }

    public void ResumeButtonPressed()
    {
        ResumeGame();
    }
    //this part is for the in-game button press to access ResumeGame//
}
