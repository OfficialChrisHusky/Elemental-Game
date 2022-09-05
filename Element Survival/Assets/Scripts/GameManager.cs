using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake() {

        if (instance == null) { instance = this; }

    }

    public int max = 10;

    [SerializeField] private List<Item> allItems = new List<Item>();
    public List<Recipe> recipes = new List<Recipe>();

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Y)) {

            Item item = allItems[Random.Range(0, allItems.Count)];
            int amount = Random.Range(1, max);

            //Debug.Log(item.name + ": " + amount);
            Inventory.instance.AddItem(item, amount);

        }

        if (Input.GetKeyDown(KeyCode.X)) {

            Item item = allItems[Random.Range(0, allItems.Count)];
            int amount = Random.Range(1, max);

            //Debug.Log(item.name + ": " + amount);
            Inventory.instance.RemoveItem(item, amount);
            
        }

        if(Input.GetKeyDown(KeyCode.P)) { Inventory.instance.DebugInventory(); }

    }

}