using UnityEngine;
using System.Collections.Generic;
using System;

//[CreateAssetMenu(fileName = "Item", menuName = "FindTheRadio/Items/Item")]

abstract public class Item : ScriptableObject
{
    public enum ItemClass
    {
        Consumable,Gear,Junk,Ressouce
    }

    [SerializeField] private List<Item> recipe = new List<Item>();
    public List<Item> Recipe
    {
        get { return this.recipe; }
    }

    [SerializeField] public new string name;
    [SerializeField] private Sprite image;
    [SerializeField] private int itemID;

    [SerializeField] [Multiline] private string description = "";

    [SerializeField] private Color32 maskColor = new Color32(255, 255, 255, 255);
    public Color32 GetMaskColor()
    {
        return maskColor;
    }

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

    public bool IsCraftable()
    {
        return recipe.Count != 0;
    }
    
    public bool IsGear()
    {
        return (this is Gear);
    }

    public string GetDescription()
    {
        return this.description;
    }

    public bool Equals(UnityEngine.Object other)
    {
        if (other is Item)
        {
            //return ((Item)other).GetItemID() == this.GetItemID();
            return other.GetInstanceID() == GetInstanceID();
        }
        return false;
    }

    public bool IsSameAs(Item item)
    {
        Type type = this.GetType();
        if (type == item.GetType())
        {
            return this.name == item.name && this.itemID == item.itemID; ;
        }
        return false;
    }

    /*
    public int GetItemID()
    {
        return itemID;
    }
    */

}

