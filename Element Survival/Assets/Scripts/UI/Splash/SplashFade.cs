using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashFade : MonoBehaviour
{
    public Image SplashImage;
    //public string Load;//
    [SerializeField] private GameObject Main;
    [SerializeField] private GameObject Splash;

    IEnumerator Start()
    {
        SplashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(3.5f);

        FadeOut();
        yield return new WaitForSeconds(2.5f);

        //SceneManager.LoadScene(Load);//
        MenuLoad();
        yield return new WaitForSeconds(1.5f);
    }

    void FadeIn()
    {
        SplashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        SplashImage.CrossFadeAlpha(0.0f, 2.5f, false);
        
    }

    void MenuLoad()
    {
        Main.SetActive(true);
        Splash.SetActive(false);
    }
}
