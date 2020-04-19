using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberSlot : MonoBehaviour {

    private Item item;
    private int inventoryNumber;
    private int craftingNumber;

    public Image icon;
    public Image slot;
    public Color standardColor;
    public Color missingColor;

    public TMP_Text numbersText;

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = newItem.GetSprite();
        icon.enabled = true;
    }

    public void SetNumbers(int a, int b) {
        inventoryNumber = a;
        craftingNumber = b;
        numbersText.SetText(a + "/" + b);
        if (a < b) {
            Mask();
        } else {
            UnMask();
        }
    }

    public void Mask() {
        slot.color = missingColor;
        numbersText.color = missingColor;
    }

    public void UnMask() {
        slot.color = standardColor;
        numbersText.color = standardColor;
    }

    public Item GetItem() {
        return item;
    }

    public int GetCraftingNumber() {
        return craftingNumber;
    }
}