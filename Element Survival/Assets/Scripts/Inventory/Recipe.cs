using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Inventory/Recipe")]
public class Recipe : ScriptableObject {

    public Item mainOutput;
    public List<ItemPair> requiredItems = new List<ItemPair>();
    public List<ItemPair> outputItems = new List<ItemPair>();
    public float timeToCraft = 1.0f;
}

[System.Serializable]
public class ItemPair {

    public Item item;
    public int amount;

}