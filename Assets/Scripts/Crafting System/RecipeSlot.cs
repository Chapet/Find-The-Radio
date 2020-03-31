using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    private Item item;

    public Image icon;
    public Image slot;
    public Color standardColor;
    public Color missingColor;

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = newItem.GetSprite();
        icon.enabled = true;
    }

    public void Mask() {
        slot.color = missingColor;
    }

    public void UnMask() {
        slot.color = standardColor;
    }

    public Item GetItem() {
        return item;
    }
}
