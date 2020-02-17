using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthItem : Item
{
    [SerializeField] private int healthValue;

    public void Awake()
    {
        itemType.Add(ItemType.HealthValue);
    }

    public int getHealthValue()
    {
        return healthValue;
    }
}
