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

    [SerializeField] public List<string> consumables = new List<string>();
    [SerializeField] public List<string> equipment = new List<string>();
    [SerializeField] public List<string> resources = new List<string>();
    [SerializeField] public List<string> junks = new List<string>();
    [SerializeField] public bool is_firstGame;

    // Background tasks :
    public bool isScavenging;
    public string scavengeEnding;

    public GameData(PlayerController p, InventoryManager i, float t, bool isScavenging, DateTime scavengeEnding)
    {
        if (isScavenging)
        {
            this.isScavenging = isScavenging;
            this.scavengeEnding = XmlConvert.ToString(scavengeEnding, XmlDateTimeSerializationMode.Local);
        }
    
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
    }
}
