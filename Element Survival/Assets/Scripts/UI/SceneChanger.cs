using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void MoveToScene(string SceneID)
    {
        SceneManager.LoadScene(SceneID);
        //Allows the scene to change to the entered ID upon clicking associated button//
    }
}