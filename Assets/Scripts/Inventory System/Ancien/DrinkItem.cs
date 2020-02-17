using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkItem : Item
{
    
    [SerializeField]private int drinkValue; //value between -100 and 100

    public DrinkItem() 
    {
        itemType.Add(ItemType.HungerValue);
    }

}