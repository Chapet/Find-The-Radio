using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryExample inventoryExample;
    public List<Item> inventory;
    public bool useExample = false;

    void Start()
    {     
        if (useExample)
        {
            inventory = inventoryExample.GetInventory();
        }
        else
        {
            inventory = new List<Item>();
        }
    }

    public List<Item> GetItems(ItemType itemType)
    {
        List<Item> list = new List<Item>();
        foreach (Item i in inventory)
        {
            foreach(ItemType it in i.itemType)
            {
                if (it == itemType)
                {
                    list.Add(i);
                    break;
                }
            }
        }
        return list;
    }

    public Item GetItem(Item item)
    {
        foreach (Item i in inventory)
        {
            if (i.Equals(item))
            {
                return i;
            }             
        }
        return null;
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        Debug.Log(item + " has been removed from the inventory");
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log(item + " has been added to the inventory");
    }
}
