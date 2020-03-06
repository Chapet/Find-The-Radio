﻿using UnityEngine;

//[CreateAssetMenu(fileName = "Item", menuName = "MyAsset/Items/Item")]

abstract public class Item : ScriptableObject
{

    //[SerializeField] private List<ItemType> itemTypes = new List<ItemType>();//contient tt les type d'un item
    [SerializeField] public new string name;
    [SerializeField] private Sprite image;
    [SerializeField] private int itemID = 0;

    [SerializeField] [Multiline] private string description = "";

    public void SetSprite(Sprite sprite)
    {
        this.image = sprite;
    }

    public Sprite GetSprite()
    {
        return image;
    }

    public bool IsConsumable()
    {
        return (this is Consumable);
    }

    abstract public bool IsCraftable();
    
    public bool IsGear()
    {
        return (this is Gear);
    }

    public string GetDescription()
    {
        return this.description;
    }

    public bool Equals(Object other)
    {
        if (other is Item)
        {
            return ((Item)other).GetItemID() == this.GetItemID();
        }
        return false;
    }

    public int GetItemID()
    {
        return itemID;
    }

}