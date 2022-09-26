using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour {

    //A static public instance since there will be only one Inventory in the entire game
    public static Inventory instance;

    private void Awake() {
        
        if(instance == null) { instance = this;  }

    }

    //---------------------
    //------Variables------
    //---------------------
    [SerializeField] private Dictionary<Item, int> items = new Dictionary<Item, int>();
    [SerializeField] private List<ItemSlot> slots = new List<ItemSlot>();

    [Header("Inventory UI")]
    [SerializeField] private bool isOpen = false;
    [SerializeField] private GameObject InventoryUI;
    public ItemSlot tempSlot;

    [Header("Recipe UI")]
    [SerializeField] private GameObject recipeSlotPrefab;
    [SerializeField] private Transform recipeSlotParent;
    private Dictionary<Recipe, RecipeSlot> recipeSlots = new Dictionary<Recipe, RecipeSlot>();

    [Header("Inventory Description")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private TMP_InputField amountInput;
    [SerializeField] private Button placeButton;

    [Header("Recipe Description")]
    [SerializeField] private Image recipeIcon;
    [SerializeField] private TMP_Text recipeNameText;
    [SerializeField] private TMP_Text recipeDescriptionText;

    [Header("Hotbar")]
    [SerializeField] private ItemSlot selectedSlot;

    private int selectedSlotIndex;
    private int recipeCraftAmount = 1;
    private Recipe currentRecipe;
    private PlayerCraftingAbilities placeSystem;

    private void Start() {

        selectedSlot = slots[0];
        selectedSlot.Select();
        placeSystem = this.GetComponent<PlayerCraftingAbilities>();

        foreach(Recipe recipe in GameManager.instance.recipes) {

            RecipeSlot slot = Instantiate(recipeSlotPrefab, recipeSlotParent).GetComponent<RecipeSlot>();

            slot.Initialize(recipe);
            recipeSlots.Add(recipe, slot);

        }

        UpdateRecipeSlots();

        amountInput.characterValidation = TMP_InputField.CharacterValidation.Integer;

    }

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Tab)) {

            isOpen = !isOpen;
            Open();

        }

        if(tempSlot.isDragged) tempSlot.transform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0) && selectedSlot.item && !isOpen)
            selectedSlot.item.PrimaryUse();
        if (Input.GetKeyDown(KeyCode.Mouse1) && selectedSlot.item && !isOpen)
            selectedSlot.item.SecondaryUse();

        if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectHotbarSlot(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectHotbarSlot(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectHotbarSlot(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectHotbarSlot(3); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SelectHotbarSlot(4); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { SelectHotbarSlot(5); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { SelectHotbarSlot(6); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { SelectHotbarSlot(7); }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { SelectHotbarSlot(8); }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { SelectHotbarSlot(9); }

        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f) {

            SelectHotbarSlot(selectedSlotIndex == 0 ? 9 : selectedSlotIndex - 1);

        } else if(Input.GetAxis("Mouse ScrollWheel") < 0.0f) {

            SelectHotbarSlot(selectedSlotIndex == 9 ? 0 : selectedSlotIndex + 1);

        }

    }

    //------------------------
    //------Inventory UI------
    //------------------------
    private void Open() {

        itemIcon.transform.parent.gameObject.SetActive(false);
        recipeIcon.transform.parent.gameObject.SetActive(false);
        InventoryUI.SetActive(isOpen);

        Player.instance.canLook = !isOpen;
        Player.instance.canMove = !isOpen;

        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;

    }

    public void ShowItemDescription(Item item) {

        itemIcon.sprite = item.sprite;
        itemNameText.text = item.name;
        itemDescriptionText.text = item.description;

        itemIcon.transform.parent.gameObject.SetActive(true);

        placeButton.onClick.RemoveAllListeners();
        placeButton.onClick.AddListener(() => { 
            placeSystem.startPlacementOf(item);
            isOpen = !isOpen;
            Open();
        });

    }

    //-----------------------
    //------Crafting UI------
    //-----------------------
    public void UpdateRecipeSlots() {

        foreach(Recipe recipe in recipeSlots.Keys) {

            int howMany = HowManyCanCraft(recipe);

            recipeSlots[recipe].UpdateSlot(howMany);
            if (currentRecipe == recipe) {

                if (howMany <= 0) EnableCraftButton(false);
                else EnableCraftButton(true);

            }

        }

    }

    public void ShowRecipeDescription(Recipe recipe, bool canCraft) {

        currentRecipe = recipe;

        recipeIcon.sprite = recipe.mainOutput.sprite;
        recipeNameText.text = recipe.mainOutput.name;
        recipeDescriptionText.text = recipe.mainOutput.description;
        
        recipeIcon.transform.parent.gameObject.SetActive(true);
        EnableCraftButton(canCraft);

    }

    public void EnableCraftButton(bool enabled) {

        recipeIcon.transform.parent.GetChild(3).gameObject.GetComponent<Button>().interactable = enabled;

    }

    //------------------------------
    //------Crafting Functions------
    //------------------------------
    public int HowManyCanCraft(Recipe recipe) {

        int ret = 0;
        bool first = true;

        foreach(ItemPair pair in recipe.requiredItems) {

            if (!items.ContainsKey(pair.item)) return 0;
            if (items[pair.item] < pair.amount) return 0;

            int temp = items[pair.item] / pair.amount;

            if (first) ret = temp;
            if (ret > temp) ret = temp;

            first = false;

        }

        return ret;

    }

    public void Craft() {

        foreach(ItemPair pair in currentRecipe.requiredItems) {

            RemoveItem(pair.item, pair.amount * recipeCraftAmount);

        }

        foreach(ItemPair pair in currentRecipe.outputItems) {

            AddItem(pair.item, pair.amount * recipeCraftAmount);

        }

    }

    public void SubmitRecipeAmount(string text) {

        if (text == "") return;

        recipeCraftAmount = int.Parse(text);

        if (HowManyCanCraft(currentRecipe) < recipeCraftAmount) {

            recipeIcon.transform.parent.GetChild(3).gameObject.GetComponent<Button>().interactable = false;

        } else {

            recipeIcon.transform.parent.GetChild(3).gameObject.GetComponent<Button>().interactable = true;

        }

    }

    //------------------------------------
    //------Adding or removing Items------
    //------------------------------------
    public void AddItem(Item item, int amount, bool onlyDictionary = false) {

        if(onlyDictionary) {

            if(items.ContainsKey(item)) { items[item] += amount; UpdateRecipeSlots(); return; }
            else { items.Add(item, amount); UpdateRecipeSlots(); return; }

        }

        List<ItemSlot> emptySlots = new List<ItemSlot>();
        List<ItemSlot> slotsWithItem = new List<ItemSlot>();

        int temp = amount;

        foreach (ItemSlot slot in slots) {

            if (!slot.item) { emptySlots.Add(slot); continue; }
            if (slot.item.id == item.id && slot.count < item.maxStack)
                { slotsWithItem.Add(slot); continue; }

        }

        if (items.ContainsKey(item)) {

            foreach (ItemSlot slot in slotsWithItem) {

                if (slot.count + temp < item.maxStack) {

                    items[item] += temp;
                    slot.count += temp;
                    slot.UpdateCount();

                    UpdateRecipeSlots();

                    return;

                }

                temp -= item.maxStack - slot.count;
                items[item] += item.maxStack - slot.count;
                slot.count = item.maxStack;
                slot.UpdateCount();

            }

        } else {

            foreach (ItemSlot slot in emptySlots) {

                if (temp < item.maxStack) {

                    slot.AddItem(item, temp);
                    if (items.ContainsKey(item))
                        { items[item] += temp; } else
                        { items.Add(item, temp); }

                    UpdateRecipeSlots();

                    return;

                }

                temp -= item.maxStack;
                slot.AddItem(item, item.maxStack);
                if (items.ContainsKey(item))
                    { items[item] += item.maxStack; } else
                    { items.Add(item, item.maxStack); }

            }

            return;

        }

        if (temp <= 0) return;

        foreach (ItemSlot slot in emptySlots) {

            if (temp < item.maxStack) {

                slot.AddItem(item, temp);
                items[item] += temp;

                UpdateRecipeSlots();

                return;

            }

            temp -= item.maxStack;
            slot.AddItem(item, item.maxStack);
            items[item] += item.maxStack;

        }

    }

    public void RemoveItem(Item item, int amount, bool onlyDictionary = false) {

        if (!items.ContainsKey(item)) { Debug.LogError("Player doesn't have this Item!"); return; }
        if(items[item] < amount) { Debug.LogError("Player Doesn't have enough!"); return; }

        if(onlyDictionary) { items[item] -= amount; return; }

        List<ItemSlot> slotsWithItem = new List<ItemSlot>();

        foreach(ItemSlot slot in slots) {

            if (slot.item && slot.item.id == item.id) slotsWithItem.Add(slot);

        }

        int temp = amount;

        foreach(ItemSlot slot in slotsWithItem) {

            if(slot.count - temp > 0) {

                items[item] -= temp;
                slot.count -= temp;
                slot.UpdateCount();

                return;

            }

            temp -= slot.count;
            items[item] -= slot.count;
            slot.Clear();

        }

        UpdateRecipeSlots();

    }

    public bool ContainsItem(Item item, int amount = 1) {

        if(!items.ContainsKey(item)) return false;

        return items[item] >= amount;

    }

    private void SelectHotbarSlot(int index) {

        selectedSlotIndex = index;

        selectedSlot.Deselect();
        slots[index].Select();
        selectedSlot = slots[index];

    }

    public void DebugInventory() {

        foreach(Item item in items.Keys) {

            Debug.Log(item.name + ": " + items[item]);

        }

    }

}
