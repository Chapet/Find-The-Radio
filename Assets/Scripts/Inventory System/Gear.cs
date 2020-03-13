using System.Collections.Generic;
using UnityEngine;

public enum ScavengOption
{
    option1,option2
}

[CreateAssetMenu(fileName = "Gear Item", menuName = "FindTheRadio/Items/Gear")]

public class Gear:Item
{
    [SerializeField] private List<Item> recipe = new List<Item>();
    public List<Item> Recipe {
        get { return this.recipe; }
    }

    // ===== Canvas for item types =====
    public enum ItemType
    {
        None = 0, Weapon, Head, Chest, Legs
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

    [SerializeField] private int scavengRate = 0; //change d'avoir certain item
    [SerializeField] private int scavengSize = 0; //nombre d'item qu'on peut prendre avec grâce à cet item
    [SerializeField] private List<ScavengOption> scavengOptions= new List<ScavengOption>();

    public int getScavengRate()
    {
        return scavengRate;
    }

    public int getScavengSize()
    {
        return scavengSize;
    }

    public ScavengOption[] ScavengOptions()
    {
        return scavengOptions.ToArray();
    }

    public bool haveScavengeOption(ScavengOption option)
    {
        return scavengOptions.Contains(option);
    }

    public override bool IsCraftable()
    {
        return recipe.Count != 0;
    }
}