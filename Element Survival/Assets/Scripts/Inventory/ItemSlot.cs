using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour {

    public Item item;
    public int count;
    public bool isDragged;

    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countText;

    public void AddItem(Item item, int count) {

        this.item = item;
        image.sprite = item.sprite;
        this.count = count;
        UpdateCount();

        image.gameObject.SetActive(true);

    }

    public void Clear() {

        image.gameObject.SetActive(false);

        isDragged = false;
        image.sprite = null;
        item = null;
        count = 0;
        UpdateCount();

    }

    public void UpdateCount() { countText.text = count.ToString(); }

    public void BeginDrag() {

        ItemSlot tempSlot = Inventory.instance.tempSlot;

        if (item) Inventory.instance.ShowItemDescription(item);

        if (tempSlot.isDragged) {

            if(!item) {

                Item item = Inventory.instance.tempSlot.item;
                int count = Inventory.instance.tempSlot.count;

                AddItem(item, count);
                Inventory.instance.AddItem(item, count, true);

                tempSlot.gameObject.SetActive(false);
                tempSlot.Clear();

            } else {

                Item savedItem = tempSlot.item;
                int savedCount = tempSlot.count;

                tempSlot.AddItem(item, count);
                Inventory.instance.RemoveItem(item, count, true);

                AddItem(savedItem, savedCount);
                Inventory.instance.AddItem(savedItem, savedCount, true);

            }

        } else {

            if (!item) return;

            tempSlot.transform.position = Input.mousePosition;
            tempSlot.isDragged = true;

            tempSlot.AddItem(item, count);
            Inventory.instance.RemoveItem(item, count, true);

            tempSlot.gameObject.SetActive(true);

            Clear();

        }

    }

    public void Select() {

        transform.GetChild(0).GetComponent<Image>().color = new Color(0.08f, 0.08f, 0.08f, 0.31f);

    }

    public void Deselect() {

        transform.GetChild(0).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.47f);

    }

}
