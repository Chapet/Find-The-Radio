using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

[System.Serializable]
public class GameData
{
    public int health;
    public int hunger;
    public int thirst;
    public int energy;

    public float gameClock;

    [SerializeField] public List<string> equippedGear = new List<string>();
    [SerializeField] public List<int> equippedgearLife = new List<int>();

    [SerializeField] public List<string> consumables = new List<string>();
    
    
    [SerializeField] public List<string> equipment = new List<string>();
    
    [SerializeField] public List<string> resources = new List<string>();
    [SerializeField] public List<string> junks = new List<string>();
    
    /*=====    INTRODUCTION    =======*/
    [SerializeField] public bool is_firstGame;

    /*=========    SCAVENGING    =========*/
    public bool isScavenging;
    public List<string> scavengingItemsFound_itemName;
    public List<string> scavengingItemsFound_itemClass;
    public int[] scavengingOldStatusBar;
    public List<string> ScavengeLog;
    public List<string> scavengingPalier;
    public string scavengingStartTime;
    public string scavengingEndTime;
    
    public int scavengingActualStep;
    public int scavengingTotalSteps;

    public GameData(PlayerController p, InventoryManager i, float t)
    {  
        health = p.currentStats[(int) StatType.Health];
        hunger = p.currentStats[(int)StatType.Hunger];
        thirst = p.currentStats[(int)StatType.Thirst];
        energy = p.currentStats[(int)StatType.Energy];
        is_firstGame = PlayerController.IS_FIRST_GAME;

        gameClock = t;

        foreach (Gear g in p.Equipment)
        {
            if (g != null)
            {
                equippedGear.Add(g.filename);
                equippedgearLife.Add(g.liveGear);
            }
        }

        foreach (Consumable c in i.Consumables)
        {
            consumables.Add(c.filename);
        }
        foreach (Gear g in i.Equipment)
        {
            equipment.Add(g.filename);
        }
        foreach (Junk j in i.Junks)
        {
            junks.Add(j.filename);
        }
        foreach (Resource r in i.Resources)
        {
            resources.Add(r.filename);
        }
        
        
        /*=======    SCAVENGING    ========*/
        this.isScavenging = BackgroundTasks.Tasks.IsScavenging;
        
        this.scavengingItemsFound_itemName=new List<string>();
        this.scavengingItemsFound_itemClass=new List<string>();
        this.scavengingPalier = new List<string>();
        if (isScavenging||BackgroundTasks.Tasks.lastScavenging!=null)
        {
            foreach ((Item item, Item.ItemClass itemClass) in BackgroundTasks.Tasks.lastScavenging.itemsFound)
            {
                this.scavengingItemsFound_itemName.Add(item.filename);
                this.scavengingItemsFound_itemClass.Add(Item.ConvertItemClassToString(itemClass));
            }

            this.ScavengeLog = BackgroundTasks.Tasks.lastScavenging.scavengeLog;
            this.scavengingOldStatusBar = new[]
            {
                BackgroundTasks.Tasks.lastScavenging.oldStatusBar.health,
                BackgroundTasks.Tasks.lastScavenging.oldStatusBar.hunger,
                BackgroundTasks.Tasks.lastScavenging.oldStatusBar.thirst,
                BackgroundTasks.Tasks.lastScavenging.oldStatusBar.energy
            };
            foreach (var date in BackgroundTasks.Tasks.scavengingPalier)
            {
                this.scavengingPalier.Add(ConvertDateTimeToString(date));
            }

            if (BackgroundTasks.Tasks.StartScavenging != null && BackgroundTasks.Tasks.EndScavenging != null)
            {
                this.scavengingStartTime = ConvertDateTimeToString(BackgroundTasks.Tasks.StartScavenging);
                this.scavengingEndTime = ConvertDateTimeToString(BackgroundTasks.Tasks.EndScavenging);
            }
        }
        else
        {
            this.ScavengeLog=new List<string>();
        }
        
        this.scavengingActualStep = BackgroundTasks.Tasks.actualScavengingStep;
        this.scavengingTotalSteps = BackgroundTasks.Tasks.totalScavengingSteps;

    }

    public static string ConvertDateTimeToString(DateTime dateTime)
    {
        return dateTime.ToString("yyyy/MM/dd hh:mm:ss");
    }

    /**
     * input must have yyyy/MM/dd hh:mm:ss format
     */
    public static DateTime ConvertStringToDateTime(string input)
    {
        var a=input.Split(' ');
        var ymd = a[0].Split('/');
        var hms = a[1].Split(':');
        
        int year=Int32.Parse(ymd[0]);
        int month=Int32.Parse(ymd[1]);
        int day=Int32.Parse(ymd[2]);
        int h=Int32.Parse(hms[0]);
        int m=Int32.Parse(hms[1]);
        int s=Int32.Parse(hms[2]);

        // plus simple : this.scavengeEnding = XmlConvert.ToString(scavengeEnding, XmlDateTimeSerializationMode.Local);
        // reconvertir : XmlConvert.ToDateTime(data.scavengeEnding, XmlDateTimeSerializationMode.Local);

        return new DateTime(year,month,day,h,m,s);
    }
}
