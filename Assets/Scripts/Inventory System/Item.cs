using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Food, Drink, Health
}

public class Item: ScriptableObject
{
    
    [SerializeField ]public ItemType itemType;
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
}