﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "FindTheRadio/Items/Consumable")]

public class Consumable : Item
{
    // ===== Canvas for item types =====
    public enum ItemType
    {
        None=0, Food, Drink, Meds
    }

    [SerializeField] private List<ItemType> types = new List<ItemType>();

    public List<ItemType> GetTypes()
    {
        return types;
    }

    public bool IsOfType(ItemType it)
    {
        foreach (ItemType itemType in types)
        {
            if (itemType == it)
            {
                return true;
            }
        }
        return false;
    }
    // =================================

    public const int MAX_HUNGER_VALUE = 100;
    public const int MIN_HUNGER_VALUE = -100;

    public const int MAX_THIST_VALUE = 100;
    public const int MIN_THIST_VALUE = -100;

    public const int MAX_HEALTH_VALUE = 100;
    public const int MIN_HEALTH_VALUE = -100;

    public const int MAX_ENERGY_VALUE = 100;
    public const int MIN_ENERGY_VALUE = -100;


    //impact of the object on health, hunger,....
    [Range(MIN_THIST_VALUE, MAX_THIST_VALUE)] [SerializeField] private int thirstValue = 0;
    [Range(MIN_HUNGER_VALUE, MAX_HUNGER_VALUE)] [SerializeField] private int hungerValue = 0;
    [Range(MIN_HEALTH_VALUE, MAX_HEALTH_VALUE)] [SerializeField] private int healthValue = 0;
    [Range(MIN_ENERGY_VALUE, MAX_ENERGY_VALUE)] [SerializeField] private int energyValue = 0;

    

    public bool HaveHungerValue()
    {
        return hungerValue != 0;
    }
    public bool HaveThirstValue()
    {
        return thirstValue != 0;
    }
    public bool HaveHealthValue()
    {
        return healthValue != 0;
    }

    public bool HaveEnergyValue()
    {
        return energyValue != 0;
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
        return energyValue;
    }
}