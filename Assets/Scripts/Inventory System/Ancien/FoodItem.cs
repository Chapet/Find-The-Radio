using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodItem : Item
{
    public int healthValue; //value between -100 and 100
    public void Awake()
    {
        itemType.Add(ItemType.HungerValue);
    }



    public int getFoodValue()
    {
        return healthValue;
    }


}