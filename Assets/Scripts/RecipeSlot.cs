using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    private Item item;

    public Image icon;

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = newItem.GetSprite();
        icon.enabled = true;
    }
}
