using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame(int sceneID) {

        SceneManager.LoadScene(sceneID);

    }

    public void QuitGame() {

        Application.Quit();
        Debug.Log("Quit");

    }

    public void OpenDiscord(string link) {

        Application.OpenURL(link);

    }

}