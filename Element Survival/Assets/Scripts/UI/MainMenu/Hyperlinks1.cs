using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlinks1 : MonoBehaviour
{
    public void OpenDiscord (string link)
    {
        Application.OpenURL(link);
    }
}
