using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame(int sceneID) {

        SceneManager.LoadScene(sceneID);
        //Allows the scene to change to the entered ID upon clicking associated button//

    }

    public void QuitGame() {

        Application.Quit();
        Debug.Log("Quit");
        //Quits the Game (Can't do this in Unity debugging so Debug.Log is used to show it works)//

    }

    public void OpenDiscord(string link) {

        Application.OpenURL(link);
        //Opens the link attatched to an object//
    }

}