using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeSlot : MonoBehaviour {

    public Recipe recipe;
    [SerializeField] private Image image;
    [SerializeField] private Image disabledImage;
    [SerializeField] private TMP_Text countText;

    private bool canCraft;

    public void Initialize(Recipe recipe) {

        this.recipe = recipe;
        image.sprite = recipe.mainOutput.sprite;

        image.gameObject.SetActive(true);

    }

    public void UpdateSlot(int howMany) {

        if (howMany <= 0) {

            countText.gameObject.SetActive(false);
            disabledImage.color = new Color(0.0f, 0.0f, 0.0f, 0.7f);
            canCraft = false;

            //Inventory.instance.EnableCraftButton(false);

        } else {

            countText.text = howMany.ToString();
            countText.gameObject.SetActive(true);
            disabledImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            canCraft = true;

            //Inventory.instance.EnableCraftButton(true);

        }

    }

    public void ShowRecipeDescription() {

        Inventory.instance.ShowRecipeDescription(recipe, canCraft);

    }

}