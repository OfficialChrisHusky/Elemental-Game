using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashFade : MonoBehaviour
{
    public Image SplashImage;
    public string Load;

    IEnumerator Start()
    {
        SplashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(3.5f);

        FadeOut();
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(Load);
    }

    void FadeIn()
    {
        SplashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        SplashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
