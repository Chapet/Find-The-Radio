using System.Collections.Generic;
using System;
using System.Linq;
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

    //  Get all items in inventory corresponding to the class type @type
    public List<Item> GetItems(Type type)
    {
        List<Item> list = new List<Item>();
        foreach (Item i in inventory)
        {
            if (type == i.GetType())
            {
                list.Add(i);
            }
        }
        return list;
    }

    //  Get all items in inventory corresponding to the types enumarated in @itemTypes
    public List<Item> GetItems(params Consumable.ItemType[] itemTypes)
    {
        List<Item> list = new List<Item>();
        foreach (Consumable c in inventory.OfType<Consumable>())
        {
            foreach (Consumable.ItemType it in c.GetItemTypes())
            {
                if (itemTypes.Contains(it))
                {
                    list.Add(c);
                    break;
                }
            }
        }
        return list;
    }

    //  Get all items in inventory corresponding to the types enumarated in @itemTypes
    public List<Item> GetItems(params Gear.ItemType[] itemTypes)
    {
        List<Item> list = new List<Item>();
        foreach (Gear g in inventory.OfType<Gear>())
        {
            foreach (Gear.ItemType it in g.GetItemTypes())
            {
                if (itemTypes.Contains(it))
                {
                    list.Add(g);
                    break;
                }
            }
        }
        return list;
    }

    //  Get all items in inventory corresponding to the types enumarated in @itemTypes
    public List<Item> GetItems(params Resource.ItemType[] itemTypes)
    {
        List<Item> list = new List<Item>();
        foreach (Resource r in inventory.OfType<Resource>())
        {
            foreach (Resource.ItemType it in r.GetItemTypes())
            {
                if (itemTypes.Contains(it))
                {
                    list.Add(r);
                    break;
                }
            }
        }
        return list;
    }

    //  Get @n items in inventory corresponding to the @item:
    //      if there are @n items,
    //      otherwise null.
    public List<Item> GetItems(Item item, int n)
    {
        List<Item> list = new List<Item>();
        foreach (Item i in inventory)
        {
            if (i.Equals(item) && n>0)
            {
                list.Add(i);
                n--;
            }
        }
        if (n != 0)
        {
            return null;
        }
        else
        {
            return list;
        }
    }

    //  Get @item:
    //      if it's in the inventory,
    //      otherwise null.
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
