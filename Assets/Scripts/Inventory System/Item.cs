using System.Collections;
using System.Collections.Generic;
using Inventory_System;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Food,Heal,Usable,Craftable,Gear
}

[CreateAssetMenu(fileName="Item",menuName="MyAsset/Items/Item")]

public class Item: ScriptableObject
{

    [SerializeField] public List<ItemType> itemType=new List<ItemType>();//contient tt les type d'un item
    [SerializeField] public new string name;
    [SerializeField] private Sprite image;
   
    
    [SerializeField] [Multiline]private string description;

    public void SetSprite(Sprite sprite)
    {
        this.image = sprite;
    }


    public Sprite GetSprite()
    {
        return image;
    }

    public bool IsUsable()
    {
        return (this is Usable) || itemType.Contains(ItemType.Usable);
    }

    public bool IsCraftable()
    {
        return this.itemType.Contains(ItemType.Craftable);
    }
    public bool IsGear()
    {
        return (this is Gear)||this.itemType.Contains(ItemType.Gear);
    }
    

    public string GetDescription()
    {
        return this.description;
    }

    public bool Equals(Object other)
    {
        if (other is Item)
        {
            return ((Item)other).description == this.description
                && ((Item)other).image == this.image
                && ((Item)other).name == this.name
                && ((Item)other).itemType.Equals(this.itemType);
        }
        return false;
    }
}