using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Food item",menuName="MyAsset/Items/Food")]

public class FoodItem : Item
{
    public int healthValue; //value between -100 and 100
    public void Awake()
    {
        itemType = ItemType.Food;
    }



    public int getFoodValue()
    {
        return healthValue;
    }


}