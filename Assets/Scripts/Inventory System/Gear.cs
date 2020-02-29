using System.Collections.Generic;
using UnityEngine;

namespace Inventory_System
{
    public enum ScavengOption
    {
        option1,option2
    }

    [CreateAssetMenu(fileName = "Gear Item", menuName = "MyAsset/Items/Gear")]


    public class Gear:Item
    {
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


    }
}