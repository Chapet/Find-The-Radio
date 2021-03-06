﻿
using System;
using System.Collections.Generic;
public class Scavenging
{
    private PlayerController player;
    private InventoryManager inventory;

    public List<(Item item,Item.ItemClass itemClass)> itemsFound;
    public List<string> scavengeLog; 

    public List<Item> GetItemsFound()
    {
        List<Item> items=new List<Item>();
        foreach (var e in itemsFound)
        {
            items.Add(e.item);
        }

        return items;
    }

    public (int health, int hunger, int thirst, int energy) oldStatusBar;
    public Scavenging()
    {
        player=PlayerController.Player;
        inventory=InventoryManager.Inventory;
        BuildScavengeItems(scavengeItemsData);
        itemsFound = new List<(Item item, Item.ItemClass itemClass)>();
        scavengeLog = new List<string>();

    }
    
    
    
   
    
    /**
     * build array that contain all items that can be scavenging with the weight attach to each item
     */
    private void BuildScavengeItems(
        (string link, int minLevel, Item.ItemClass itemType, double minTimeOut, int weight)[] input)
    {
        List<(string link, int minLevel, Item.ItemClass itemType, double minTimeOut)> itemList=new List<(string link, int minLevel, Item.ItemClass itemType, double minTimeOut)>();
        foreach (var item in input)
        {
            for (int i = 0; i < item.weight; i++)
            {
                itemList.Add((link: item.link, minLevel: item.minLevel, itemType: item.itemType, minTimeOut: item.minTimeOut));
            }
        }

        scavengeItems = itemList.ToArray();
    }
    
    /**
     * get all item that have a minLevl less of equal to mylevel, a type of type (consummable, ressources, junk and gear)
     * and that we can find with a our scavengeTime
     *
     * Return array of [<link> as string, <luck> as double, <minlevel> as int, <itemtype> as ItemType, <minTimeOut> as double]
     */
    public (string link,int minLevel,Item.ItemClass itemType,double minTimeOut)[] getMyLevelItems(int myLevel,Item.ItemClass type, double scavengeTime)
    {
        
        List <(string link,int minLevel,Item.ItemClass itemType,double minTimeOut)> result= new List<(string link,int minLevel,Item.ItemClass itemType,double minTimeOut)>();

        for (int i = 0; i < scavengeItems.Length; i++)
        {
            if (scavengeItems[i].minLevel <= myLevel && scavengeItems[i].itemType==type &&scavengeItems[i].minTimeOut<=(double)scavengeTime)
            {
                result.Add(scavengeItems[i]);
            }
        }
        return result.ToArray();
    }

    
    /*========================= ALL ITEM SCAVENGE =================================*/
    private (string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] scavengeItems;
        /**
         * link: lien vers l'item
         * MinLevel: le libeau minimum de l'utilisateur pour avoir cet iem
         * itypeType: le type de ressources
         * scavengeTime: le temps minimum qu'il faut sortir pour trouuver cet item (en nbr d'heure)
         */
    private (string link, int minLevel, Item.ItemClass itemType, double minTimeOut,int weight)[] scavengeItemsData = new[]
    {
        /*======CONSUMABLE=====*/
        (link: "Items/Consumables/WaterBottle", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:4),

        (link: "Items/Consumables/FoodCan", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:2),

        (link: "Items/Consumables/Soda", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:2),
        
        (link: "Items/Consumables/Medkit", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 6.0,weight:1),

        (link: "Items/Consumables/MilitaryRation", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 4.0,weight:1),

        (link: "Items/Consumables/Crackers", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:2),

        (link: "Items/Consumables/Bandage", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 2.0,weight:1),


        /*======GEAR=====*/
        (link: "Items/Gear/Gun", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 8.0,weight:1),

        (link: "Items/Gear/BaseballBat", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 0.0,weight:1),

        (link: "Items/Gear/Helmet", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 3.0,weight:1),

        (link: "Items/Gear/Chestplate", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 5.0,weight:1),

        (link: "Items/Gear/Greaves", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 4.0,weight:1),

        (link: "Items/Gear/IronPipe", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 4.0,weight:1),

        (link: "Items/Gear/Knife", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 6.0,weight:1),


        /*======RESOURCES=====*/
        (link: "Items/Resources/Wood", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),

        (link: "Items/Resources/Metalscrap", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),

        (link: "Items/Resources/Leather", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),

        (link: "Items/Resources/Cloth", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),

        (link: "Items/Resources/TeaLeaf", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),

        (link: "Items/Resources/CoffeeBeans", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),


        /*======Junk=====*/
        //(link: "Items/Junks/Grass", minLevel: 0, itemType: Item.ItemClass.Junk, minTimeOut: 0.0,weight:1),

    };
    
    
    /**
     * Check if we are going to use the gun (depend of probability) and modify inventory of gun if it's use
     * @Pre: probabilité have to be between 0 and 1
     *
     * if we don't have a gun, return false
     */
    public bool MayUseWeapon()
    {
        double probaWeapon;
        Gear getGear = PlayerController.Player.GetGear(Gear.ItemType.Weapon);
        if (getGear != null)
        {
            if (getGear.filename == "Gun")
            {
                probaWeapon = 0.9;
            }
            else if (getGear.filename == "Knife")
            {
                probaWeapon = 0.6;
            }
            else if (getGear.filename == "IronPipe")
            {
                probaWeapon = 0.4;
            }
            else if (getGear.filename == "BaseballBat")
            {
                probaWeapon = 0.2;
            }
            else
            {
                probaWeapon = 0;
            }
        }
        else
        {
            probaWeapon = 0;
        }
        if (probaWeapon > 1 || probaWeapon < 0)
        {
            return false;
        }

        Gear gun;
        if ((gun=player.GetGear(Gear.ItemType.Weapon))==null)
        {
            //I'm not equiped with a weapon
            return false;
        }

        double chance=new System.Random().NextDouble();
        
        if (chance <= probaWeapon)
        {
            /*====    DOMAGE    ====*/
            gun.liveGear -= 20;
            
            if (gun.liveGear <= 0)
            {
                inventory.RemoveItem(gun);
                player.Equipment[(int) Gear.ItemType.Weapon] = null;
                return false;
            }

            return true;
        }

        return false;
    }
    
    public int MayUseArmor()
    {
        Gear armorPiece;
        int count = 0;
        double proba=new System.Random().NextDouble();
        
        if (inventory.Equipment.Count == 0)
        {
            return 0;
        }

        if ((armorPiece = player.GetGear(Gear.ItemType.Chestplate)) != null)
        {
            count+=3;

            armorPiece.liveGear -= 20;
            if (armorPiece.liveGear <= 0)
            {
                inventory.RemoveItem(armorPiece);
            }

        }
        if ((armorPiece = player.GetGear(Gear.ItemType.Greaves)) != null)
        {
            count+=2;

            armorPiece.liveGear -= 20;
            if (armorPiece.liveGear <= 0)
            {
                inventory.RemoveItem(armorPiece);
            }

        }
        if ((armorPiece = player.GetGear(Gear.ItemType.Helmet)) != null)
        {
            count+=2;

            armorPiece.liveGear -= 20;
            if (armorPiece.liveGear <= 0)
            {
                inventory.RemoveItem(armorPiece);
            }

        }

        return count;
    }
    
    
    
}