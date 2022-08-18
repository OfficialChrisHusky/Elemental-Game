using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftedItem", menuName = "ScriptableObject/Inventory/Crafting/CraftedItem")]
public class CraftedItem : Item
{

    public List<Item> requirements;
    public List<int> quantityRequirements;
    public GameObject instantiated;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
