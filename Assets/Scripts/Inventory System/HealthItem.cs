using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Health item",menuName="MyAsset/Items/Health")]

public class HealthItem : Item
{
    [SerializeField] private int healthValue;

    public void Awake()
    {
        itemType= ItemType.Health;
    }

    public int getHealthValue()
    {
        return healthValue;
    }
}
