﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.GetSprite();
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public Item GetItem()
    {
        return item;
    } 
}
