using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Inventory Example",menuName="FindTheRadio/Player/Inventory")]

public class InventoryExample : ScriptableObject
{
    public List<Item> exampleItems;
    List<Consumable> consumables;
    List<Gear> equipment;
    List<Junk> junks;
    List<Resource> resources;
    public int numberOfConsumables = 32;
    public int numberOfEquipment = 8;
    public int numberOfJunks = 16;
    public int numberOfResources = 16;

    private void InitInventory()
    {
        consumables = new List<Consumable>();
        equipment = new List<Gear>();
        resources = new List<Resource>();
        junks = new List<Junk>();
        foreach (Item i in exampleItems)
        {
            Debug.Log(i);
            if (i.GetType().ToString() == "Consumable")
            {
                int rnd = Random.Range(4, 8);
                for (int j = 0; j < rnd; j++)
                {
                    consumables.Add(Instantiate(i as Consumable));
                }
            }
            if (i.GetType().ToString() == "Gear")
            {
                int rnd = Random.Range(2, 4);
                for (int j = 0; j < rnd; j++)
                {
                    equipment.Add(Instantiate(i as Gear));
                }
            }
            if (i.GetType().ToString() == "Resource")
            {
                int rnd = Random.Range(4, 8);
                for (int j = 0; j < rnd; j++)
                {
                    resources.Add(Instantiate(i as Resource));
                }
            }
            if (i.GetType().ToString() == "Junk")
            {
                int rnd = Random.Range(4, 8);
                for (int j = 0; j < rnd; j++)
                {
                    junks.Add(Instantiate(i as Junk));
                }
            }
        }
    }

    public object[] GetInventory()
    {
        InitInventory();
        object[] list = new object[] {consumables, equipment, resources, junks};
        return list;
        /*
        List<Item> list = new List<Item>();
        for (int i=0; i<numberOfItems; i++)
        {
            int rnd = Random.Range(0, exampleItems.Count);
            Item item = Instantiate(exampleItems[rnd]);
            list.Add(item);
        }
        return list;
        */
    }
}