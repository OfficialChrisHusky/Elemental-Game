using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCraftingAbilities : MonoBehaviour
{
    [Header("Crafting Variables")]
    public Material holdingMaterial; //transparent material to indicate that the player is placing an object
    public Material wrongPlacementMaterial; //transparent material to indicate that the player can't place in the current spot
    public bool canCraft = true; //is the player able to craft something?

    private PlayerGrab grabScript;

    // Start is called before the first frame update
    void Start()
    {
        grabScript = this.GetComponent<PlayerGrab>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startPlacementOf(Item item)
    {
        if (!canCraft) return;

        var g = Instantiate(item.Object);
        var material = g.GetComponent<MeshRenderer>().material;
        g.GetComponent<MeshRenderer>().material = holdingMaterial;
        grabScript.grabPlacement(g,material);
    }

}
