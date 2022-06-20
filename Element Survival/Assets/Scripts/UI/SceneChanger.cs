using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void MoveToScene(string SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}