using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

//[CreateAssetMenu(fileName = "Item", menuName = "FindTheRadio/Items/Item")]

abstract public class Item : ScriptableObject
{
    public enum ItemClass
    {
        [EnumMember(Value = "Consumable")]
        Consumable,
        [EnumMember(Value = "Gear")]
        Gear,
        [EnumMember(Value = "Junk")]
        Junk,
        [EnumMember(Value = "Resource")]
        Resource
    }

    [SerializeField] private List<Item> recipe = new List<Item>();
    public List<Item> Recipe
    {
        get { return this.recipe; }
    }

    [SerializeField] public new string name;
    [SerializeField] public string filename;
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

    public static string ConvertItemClassToString(ItemClass itemClass)
    {
        if (itemClass == ItemClass.Consumable)
            return "Consumable";
        else if (itemClass == ItemClass.Gear)
            return "Gear";
        else if (itemClass == ItemClass.Junk)
            return "Junk";
        else if (itemClass == ItemClass.Resource)
            return "Resource";
        else
            return null;
    }

    public static ItemClass ConvertStringToItemCLass(string itemclass)
    {
        if (itemclass.Equals("Consumable"))
            return ItemClass.Consumable;
        else if (itemclass.Equals("Gear"))
            return ItemClass.Gear;
        else if (itemclass.Equals("Junk"))
            return ItemClass.Junk;
        else if (itemclass.Equals("Resource"))
            return ItemClass.Resource;
        else
            Debug.Log("Error ConvertStringToItemClasse: "+itemclass);
        return ItemClass.Consumable;
    }
    
    public static Item LoadItem(string filename, Item.ItemClass itemClass)
    {
        if (itemClass == Item.ItemClass.Consumable)
        {
            return Resources.Load("Items/Consumables/" + filename) as Consumable;
        }
        else if(itemClass==Item.ItemClass.Gear)
        {
            return Resources.Load("Items/Gear/" + filename) as Gear;
        }
        else if(itemClass==Item.ItemClass.Junk)
        {
            return Resources.Load("Items/Junks/" + filename) as Junk;
        }
        else if(itemClass==Item.ItemClass.Resource)
        {
            return Resources.Load("Items/Resources/" + filename) as Resource;
        }

        return null;
    }

}

