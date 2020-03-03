using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Inventory Example",menuName="MyAsset/Player/Inventory")]

public class InventoryExample : ScriptableObject
{
    public List<Item> exampleItems;
    public int numberOfItems = 20;

    public List<Item> GetInventory()
    {
        List<Item> list = new List<Item>();
        for (int i=0; i<numberOfItems; i++)
        {
            int rnd = Random.Range(0, exampleItems.Count);
            list.Add(exampleItems[rnd]);
        }
        return list;
    }
}