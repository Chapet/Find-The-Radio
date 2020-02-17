using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName="New ThistValut item",menuName="MyAsset/Items/Eatable")]

public class Eatable : Item
{
    
    
    [SerializeField]private int thirstValue; //value between -100 and 100
    [SerializeField]private int hungerValue;
    [SerializeField]private int healthValue;
    [SerializeField] private int fatigueValue;

    public bool haveHungerValue()
    {
        return itemType.Contains(ItemType.HungerValue);
    }
    public bool haveThirstValue()
    {
        return itemType.Contains(ItemType.ThistValut);
    }
    public bool haveHealthValue()
    {
        return itemType.Contains(ItemType.HealthValue);
    }

    public bool haveFatigueValue()
    {
        return itemType.Contains(ItemType.Fatigue);
    }




}