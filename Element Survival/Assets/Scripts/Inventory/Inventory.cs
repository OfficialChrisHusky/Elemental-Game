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

    [SerializeField] private bool isOpen = false;
    [SerializeField] private GameObject InventoryUI;
    public ItemSlot tempSlot;

    [Header("Item Description")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;

    [Header("Hotbar")]
    [SerializeField] private ItemSlot selectedSlot;
    private int selectedSlotIndex;

    private void Start() {

        selectedSlot = slots[0];
        selectedSlot.Select();

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

        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f) {

            SelectHotbarSlot(selectedSlotIndex == 0 ? 8 : selectedSlotIndex - 1);

        } else if(Input.GetAxis("Mouse ScrollWheel") < 0.0f) {

            SelectHotbarSlot(selectedSlotIndex == 8 ? 0 : selectedSlotIndex + 1);

        }

    }

    private void Open() {

        itemIcon.transform.parent.gameObject.SetActive(false);
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

    }

    public bool CanBeBuilded(CraftedItem item)
    {
        for(int i = 0;  i < item.requirements.Count; i++)
        {
            int itemCount = item.quantityRequirements[i];
            foreach (ItemSlot slot in slots) {
                if (slot.item != null && slot.item.id == item.requirements[i].id) 
                    itemCount -= slot.count;
            }
            if (itemCount > 0) return false;
        }
        return true;
    }

    //------------------------------------
    //------Adding or removing Items------
    //------------------------------------
    public void AddItem(Item item, int amount, bool onlyDictionary = false) {

        if(onlyDictionary) {

            if(items.ContainsKey(item)) { items[item] += amount; return; }
            else { items.Add(item, amount); return; }

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

    }

    private void SelectHotbarSlot(int index) {

        selectedSlotIndex = index;

        slots[index].Select();
        selectedSlot.Deselect();
        selectedSlot = slots[index];

    }

    public void DebugInventory() {

        foreach(Item item in items.Keys) {

            Debug.Log(item.name + ": " + items[item]);

        }

    }

}
