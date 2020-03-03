using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    FoodAndDrink = 0, Meds, Usable, Craftable, Gear, Ressources
}

public enum ItemID
{
    DefaultID = 0, WaterBottle, Gun, FoodCan, Soda, WoodLog
}

[CreateAssetMenu(fileName = "Item", menuName = "MyAsset/Items/Item")]

public class Item : ScriptableObject
{

    [SerializeField] private List<ItemType> itemTypes = new List<ItemType>();//contient tt les type d'un item
    [SerializeField] public new string name;
    [SerializeField] private Sprite image;
    [SerializeField] private ItemID id;


    [SerializeField] [Multiline] private string description;

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
        return (this is Usable) || itemTypes.Contains(ItemType.Usable);
    }

    public bool IsCraftable()
    {
        return this.itemTypes.Contains(ItemType.Craftable);
    }
    public bool IsGear()
    {
        return (this is Gear) || this.itemTypes.Contains(ItemType.Gear);
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

    public ItemID GetItemID()
    {
        return id;
    }

    public List<ItemType> GetItemTypes()
    {
        return itemTypes;
    }

    public bool IsOfType(ItemType it)
    {
        foreach(ItemType itemType in itemTypes)
        {
            if (itemType == it)
            {
                return true;
            }
        }
        return false;
    }
}