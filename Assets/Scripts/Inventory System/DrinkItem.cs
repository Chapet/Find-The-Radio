using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Drink item",menuName="MyAsset/Items/Drink")]

public class DrinkItem : Item
{
    
    [SerializeField]private int drinkValue; //value between -100 and 100

    public DrinkItem() 
    {
        itemType = ItemType.Food;
    }

}