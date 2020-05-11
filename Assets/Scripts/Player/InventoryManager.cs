using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public InventoryExample inventoryExample;
    public static InventoryManager Inventory
    {
        get; private set;
    }
    //public List<Item> inventory;
    [SerializeField] private List<Consumable> consumables;
    public List<Consumable> Consumables { get { return consumables; } }
    [SerializeField] private List<Gear> equipment;
    public List<Gear> Equipment { get { return equipment; } }
    [SerializeField] private List<Resource> resources;
    public List<Resource> Resources { get { return resources; } }
    [SerializeField] private List<Junk> junks;
    public List<Junk> Junks { get { return junks; } }
    [SerializeField] private bool useExample = false;
    [SerializeField] private int maxConsumables = 32;
    [SerializeField] private int maxEquipment = 32;
    [SerializeField] private int maxResources = 32;
    [SerializeField] private int maxJunks = 32;

    private void Awake()
    {
        Inventory = this;
        consumables = new List<Consumable>();
        equipment = new List<Gear>();
        resources = new List<Resource>();
        junks = new List<Junk>();
        if (useExample)
        {
            //inventory = inventoryExample.GetInventory();
            StartCoroutine(StartWithExample());
        }
    }

    private IEnumerator StartWithExample()
    {
        Debug.Log("OK1");
        yield return new WaitForSeconds(2f);
        Debug.Log("OK2");
        object[] example = inventoryExample.GetInventory();
        consumables = example[0] as List<Consumable>;
        equipment = example[1] as List<Gear>;
        resources = example[2] as List<Resource>;
        junks = example[3] as List<Junk>;
    }

    void Start()
    {
        Debug.Log("Inventory Started!");
    }

    //  Get all items in inventory corresponding to the class type @type
    public List<Item> GetItems(params Type[] types)
    {
        List<Item> list = new List<Item>();
        foreach (Type type in types)
        {
            if (type == typeof(Consumable))
            {
                list.AddRange(consumables);
            }
            if (type == typeof(Gear))
            {
                list.AddRange(equipment);
            }
            if (type == typeof(Junk))
            {
                list.AddRange(junks);
            }
            if (type == typeof(Resource))
            {
                list.AddRange(resources);
            }
        }
        return list;
    }

    //  Get all items in inventory corresponding to the types enumarated in @itemTypes
    public List<Item> GetItems(params Consumable.ItemType[] itemTypes)
    {
        List<Item> list = new List<Item>();
        foreach (Consumable c in consumables)
        {
            foreach (Consumable.ItemType it in c.GetTypes())
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
        foreach (Gear g in equipment)
        {
            if (itemTypes.Contains(g.Type))
            {
                list.Add(g);
                break;
            }
        }
        return list;
    }

    //  Get all items in inventory corresponding to the types enumarated in @itemTypes
    public List<Item> GetItems(params Resource.ItemType[] itemTypes)
    {
        List<Item> list = new List<Item>();
        foreach (Resource r in resources)
        {
            foreach (Resource.ItemType it in r.GetTypes())
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
        Consumable c = item as Consumable;
        if (c != null)
        {
            foreach (Consumable tmp in consumables)
            {
                if (tmp.IsSameAs(c) && n > 0)
                {
                    list.Add(tmp);
                    n--;
                }
            }
        }
        Gear g = item as Gear;
        if (g != null)
        {
            foreach (Gear tmp in equipment)
            {
                if (tmp.IsSameAs(g) && n > 0)
                {
                    list.Add(tmp);
                    n--;
                }
            }
        }
        Resource r = item as Resource;
        if (r != null)
        {
            foreach (Resource tmp in resources)
            {
                if (tmp.IsSameAs(r) && n > 0)
                {
                    list.Add(tmp);
                    n--;
                }
            }
        }
        Junk j = item as Junk;
        if (j != null)
        {
            foreach (Junk tmp in junks)
            {
                if (tmp.IsSameAs(j) && n > 0)
                {
                    list.Add(tmp);
                    n--;
                }
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
        Type t = item.GetType();
        switch (t.ToString())
        {
            case "Consumable":
                foreach (Consumable c in consumables)
                {
                    if (c.IsSameAs(item))
                    {
                        return c;
                    }
                }
                break;
            case "Gear":
                foreach (Gear g in equipment)
                {
                    if (g.IsSameAs(item))
                    {
                        return g;
                    }
                }
                break;
            case "Resource":
                foreach (Resource r in resources)
                {
                    if (r.IsSameAs(item))
                    {
                        return r;
                    }
                }
                break;
            case "Junk":
                foreach (Consumable c in consumables)
                {
                    if (c.IsSameAs(item))
                    {
                        return c;
                    }
                }
                break;

        }
        
        return null;
    }

    public void RemoveItem(Item item)
    {
        Type t = item.GetType();
        switch (t.ToString())
        {
            case "Consumable":
                consumables.Remove(item as Consumable);
                break;
            case "Gear":
                equipment.Remove(item as Gear);
                if(PlayerController.Player.IsEquipped(item as Gear))
                {
                    PlayerController.Player.UnequipGear(item as Gear);
                }
                break;
            case "Resource":
                resources.Remove(item as Resource);
                break;
            case "Junk":
                junks.Remove(item as Junk);
                break;
        }
        
        Debug.Log(item + " has been removed from the inventory");
    }

    public void AddItem(Item item)
    {
        Type t = item.GetType();
        switch (t.ToString())
        {
            case "Consumable":
                if (consumables.Count < maxConsumables)
                    consumables.Add(item as Consumable);
                else
                    Debug.Log("Too many consumables!");
                break;
            case "Gear":
                if (equipment.Count < maxEquipment)
                    equipment.Add(item as Gear);
                else
                    Debug.Log("Too many equipement!");
                break;
            case "Resource":
                if (resources.Count < maxResources)
                    resources.Add(item as Resource);
                else
                    Debug.Log("Too many resources!");
                break;
            case "Junk":
                if (junks.Count < maxJunks)
                    junks.Add(item as Junk);
                else
                    Debug.Log("Too many junks!");
                break;

        }
        //inventory.Add(item);
        //Debug.Log(item + " has been added to the inventory");
    }

    /**
     * return the first item with the same name
     * return null if there is no such item with the same name
     * 
     */
    public Item GetItem(string itemName,Item.ItemClass itemClass)
    {
        if (itemClass == Item.ItemClass.Consumable)
        {
            foreach (Item item in consumables)
            {
                if (item.name.Equals(itemName))
                    return item;
            } 
        }
        else if (itemClass == Item.ItemClass.Gear)
        {
            foreach (Item item in equipment)
            {
                if (item.name.Equals(itemName))
                    return item;
            } 
        }
        else if (itemClass == Item.ItemClass.Junk)
        {
            foreach (Item item in junks)
            {
                if (item.name.Equals(itemName))
                    return item;
            } 
        }
        else if (itemClass == Item.ItemClass.Resource)
        {
            foreach (Item item in resources)
            {
                if (item.name.Equals(itemName))
                    return item;
            }
        }
        return null;
    }

    /**
     * return null if there is no such item with that name
     */
    public Item GetItem(string itemName)
    {
        Item item;
        item = GetItem(itemName, Item.ItemClass.Consumable);
        if (item != null)
            return item;
        item = GetItem(itemName, Item.ItemClass.Gear);
        if (item != null)
            return item;
        item = GetItem(itemName, Item.ItemClass.Junk);
        if (item != null)
            return item;
        item = GetItem(itemName, Item.ItemClass.Resource);
        return item;
    }

    public bool Contain(string itemName)
    {
        return GetItem(itemName) != null;
    }

    public bool Contain(string itemName, Item.ItemClass itemClass)
    {
        return GetItem(itemName, itemClass) != null;
    }

}
