using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSplash : MonoBehaviour
{
    [SerializeField] private GameObject Main;
    [SerializeField] private GameObject Splash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Main.SetActive(true);
            Splash.SetActive(false);
        }
    }
}
