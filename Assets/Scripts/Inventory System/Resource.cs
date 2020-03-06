using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Item", menuName = "FindTheRadio/Items/Resource")]

public class Resource : Item
{
    // ===== Canvas for item types =====
    public enum ItemType
    {
        None = 0, Resource
    }

    [SerializeField] private List<ItemType> itemTypes = new List<ItemType>();

    public List<ItemType> GetItemTypes()
    {
        return itemTypes;
    }

    public bool IsOfType(ItemType it)
    {
        foreach (ItemType itemType in itemTypes)
        {
            if (itemType == it)
            {
                return true;
            }
        }
        return false;
    }
    // =================================

    public override bool IsCraftable()
    {
        return false;
    }
}
