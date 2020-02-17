using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    HungerValue, ThistValut, HealthValue,Fatigue,Scavengable,Craftable,Gear
}

public class Item: ScriptableObject
{
    
    [SerializeField ]public List<ItemType> itemType=new List<ItemType>();
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
   
    
    [SerializeField] private string description;

    public void setSprite(Sprite sprite)
    {
        this.image = sprite;
    }

    /*public int amount;

    public void addUnit()
    {
        amount++;
    }*/


    public Sprite getSprite()
    {
        return image;
    }

    public bool isEatable()
    {
        return this is Eatable;
    }

    public bool isCraftable()
    {
        return this.itemType.Contains(ItemType.Craftable);
    }
    public bool isGear()
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
}