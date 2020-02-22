using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName="New ThistValue item",menuName="MyAsset/Items/Eatable")]

public class Eatable : Item
{
    
    
    [Range(Item.MIN_THIST_VALUE,Item.MAX_THIST_VALUE)][SerializeField]private int thirstValue=0; //value between -100 and 100
    [Range(Item.MIN_HUNGER_VALUE,Item.MAX_HUNGER_VALUE)][SerializeField]private int hungerValue=0;
    [Range(Item.MIN_HEALTH_VALUE,Item.MAX_HEALTH_VALUE)][SerializeField]private int healthValue=0;
    [Range(Item.MIN_ENERGY_VALUE,Item.MAX_ENERGY_VALUE)][SerializeField]private int fatigueValue=0;

    public bool HaveHungerValue()
    {
        return itemType.Contains(ItemType.HungerValue);
    }
    public bool HaveThirstValue()
    {
        return itemType.Contains(ItemType.ThistValue);
    }
    public bool HaveHealthValue()
    {
        return itemType.Contains(ItemType.HealthValue);
    }

    public bool HaveFatigueValue()
    {
        return itemType.Contains(ItemType.Fatigue);
    }

    public int GetHunger()
    {
        return hungerValue;
    }

    public int GetThirst()
    {
        return thirstValue;
    }

    public int GetHealth()
    {
        return healthValue;
    }

    public int GetEnergy()
    {
        return fatigueValue;
    }




}