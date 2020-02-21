using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New inventory", menuName = "MyAsset/inventory")]

public class Inventory : ScriptableObject
{
    public List<Item> itemsList = new List<Item>();

    public void addItem(Item item, int amound)
    {
        for (int i = 0; i < amound; i++)
        {
            itemsList.Add(item);
        }
        
    }

    /**
     * Remove item from the inventory one time
     */
    public void RemoveItem(Item item) 
    {
        itemsList.Remove(item);
    }

    public void RemoveAllItem(Item item)
    {
        itemsList.RemoveAll(match: element => element == item); //TODO:A VERIFIER
    }

    public bool Contain(Item item)
    {
        return itemsList.Contains(item);
    }
    



}