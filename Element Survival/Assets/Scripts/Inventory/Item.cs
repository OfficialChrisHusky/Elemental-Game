using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Inventory/Item")]
public class Item : ScriptableObject {

    public string name;
    public string description;
    public int id;
    public int maxStack = 100;
    public Sprite sprite;

    virtual public void PrimaryUse() {

        Debug.Log("Primary Use: " + name);

    }

    virtual public void SecondaryUse() {

        Debug.Log("Secondary Use: " + name);

    }

}