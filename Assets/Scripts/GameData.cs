﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int health;
    public int hunger;
    public int thirst;
    public int energy;

    public List<string> test = new List<string>(new string[] { "test1", "test2" });

    [SerializeField] public List<string> equippedGear = new List<string>();

    [SerializeField] public List<string> consumables = new List<string>();
    [SerializeField] public List<string> equipment = new List<string>();
    [SerializeField] public List<string> resources = new List<string>();
    [SerializeField] public List<string> junks = new List<string>();

    public GameData(PlayerController p, InventoryManager i, float t)
    {
        health = p.currentStats[(int) StatType.Health];
        hunger = p.currentStats[(int)StatType.Hunger];
        thirst = p.currentStats[(int)StatType.Thirst];
        energy = p.currentStats[(int)StatType.Energy];

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