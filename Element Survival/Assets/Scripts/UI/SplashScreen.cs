using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float fullSplashTime = 1.0f;
    [SerializeField] float emptySplashTime = 1.5f;

    [SerializeField] private Image SplashImage;
    [SerializeField] private GameObject Main;

    bool isShowingSplash;

    IEnumerator Start()
    {
        isShowingSplash = true;

        SplashImage.canvasRenderer.SetAlpha(0.0f);

        SplashImage.CrossFadeAlpha(1.0f, 1.5f, false);
        yield return new WaitForSeconds(fullSplashTime + 1.5f);

        SplashImage.CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(emptySplashTime + 2.5f);

        Main.SetActive(true);
        gameObject.SetActive(false);

        isShowingSplash = false;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) && isShowingSplash) {

            Main.SetActive(true);
            gameObject.SetActive(false);

        }

    }

}
