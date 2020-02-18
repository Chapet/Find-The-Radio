using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName="New ThistValut item",menuName="MyAsset/Items/Eatable")]

public class Eatable : Item
{
    
    
    [Range(-100,100)][SerializeField]private int thirstValue; //value between -100 and 100
    [Range(-100,100)][SerializeField]private int hungerValue;
    [Range(-100,100)][SerializeField]private int healthValue;
    [Range(-100,100)][SerializeField]private int fatigueValue;

    public bool HaveHungerValue()
    {
        return itemType.Contains(ItemType.HungerValue);
    }
    public bool HaveThirstValue()
    {
        return itemType.Contains(ItemType.ThistValut);
    }
    public bool HaveHealthValue()
    {
        return itemType.Contains(ItemType.HealthValue);
    }

    public bool HaveFatigueValue()
    {
        return itemType.Contains(ItemType.Fatigue);
    }




}