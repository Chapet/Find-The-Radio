using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    HungerValue, ThistValue, HealthValue,Fatigue,Scavengeable,Craftable,Gear
}

public class Item: ScriptableObject
{
    
    [SerializeField ]public List<ItemType> itemType=new List<ItemType>();
    [SerializeField] public new string name;
    [SerializeField] private Sprite image;
   
    
    [SerializeField] [Multiline]private string description;

    public void SetSprite(Sprite sprite)
    {
        this.image = sprite;
    }

    /*public int amount;

    public void    addUnit()
    {
        amount++;
    }*/


    public Sprite GetSprite()
    {
        return image;
    }

    public bool IsEatable()
    {
        return this is Eatable;
    }

    public bool IsCraftable()
    {
        return this.itemType.Contains(ItemType.Craftable);
    }
    public bool IsGear()
    {
        return this.itemType.Contains(ItemType.Gear);
    }

    public override bool Equals(object other)
    {
        if (other is Item)
        {
            return ((Item) other).name.Equals(this.name);
        }
        else
        {
            return false;
        }

    }

    public string GetDescription()
    {
        return this.description;
    }
}