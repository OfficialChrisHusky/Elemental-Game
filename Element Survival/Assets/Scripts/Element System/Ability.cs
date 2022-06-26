using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour {

    [Header("Default Ability Info")]
    public string name;
    [TextArea(15, 20)]
    public string description;
    public Sprite sprite;
    public KeyCode useKey;

    public virtual void Use() {

        Debug.Log("Used Ability: " + name);

    }

    public virtual void Activate() {

        Debug.Log("Activated Ability: " + name);

    }
    
}