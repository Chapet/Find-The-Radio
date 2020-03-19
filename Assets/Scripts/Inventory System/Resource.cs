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
}
