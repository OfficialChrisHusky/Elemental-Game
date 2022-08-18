using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCraftingAbilities : MonoBehaviour
{
    [Header("Crafting Variables")]
    // Start is called before the first frame update
    public Material holdingMaterial;
    public Material wrongPlacementMaterial;
    public bool canCraft = true;

    //Only for test
    public CraftedItem item;

    private PlayerGrab grabScript;
    void Start()
    {
        grabScript = this.GetComponent<PlayerGrab>();
    }

    // Update is called once per frame
    void Update()
    {
        //Only for test
        if (Input.GetKeyDown(KeyCode.B) && Inventory.instance.CanBeBuilded(item))
        {
            startPlacementOf(item);
        }
    }

    public void startPlacementOf(CraftedItem item)
    {
        if (!canCraft) return;

        var g = Instantiate(item.instantiated);
        var material = g.GetComponent<MeshRenderer>().material;
        g.GetComponent<MeshRenderer>().material = holdingMaterial;
        grabScript.grabPlacement(g,material);
    }

}
