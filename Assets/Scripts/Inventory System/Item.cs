using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "Item", menuName = "FindTheRadio/Items/Item")]

abstract public class Item : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] private Sprite image;

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
            //return ((Item)other).GetItemID() == this.GetItemID();
            return other.GetInstanceID() == this.GetInstanceID();
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

