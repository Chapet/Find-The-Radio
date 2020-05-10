using System;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    public static StatusBarController SBController
    {
        get; private set;
    }

    private List<StatusBar>[] bars;
    public int[] prevStats;

    public bool statsChanged;

    private void Awake()
    {
        SBController = this;
        bars = new List<StatusBar>[] { new List<StatusBar>(), new List<StatusBar>(), new List<StatusBar>(), new List<StatusBar>(), new List<StatusBar>() };
    }

    private void Start()
    {
        prevStats = new int[PlayerController.Player.currentStats.Length];
        Array.Copy(PlayerController.Player.currentStats, prevStats, prevStats.Length);
    }

    private void FixedUpdate()
    {
        statsChanged = StatsChanged();
        if (statsChanged)
        {
            for (int i = 0; i < prevStats.Length; i++)
            {
                if (prevStats[i] != PlayerController.Player.currentStats[i])
                {
                    UpdateStatusBars((StatType) i);
                }
            }
        }
    }

    public void UpdateStatusBars(StatType type)
    {
        foreach (StatusBar s in bars[(int)type])
        {
            s.SetValue(PlayerController.Player.currentStats[(int)type]);
            //Debug.Log(s + " set to " + player.currentStats[(int)type]);
        }
        Array.Copy(PlayerController.Player.currentStats, prevStats, prevStats.Length);
        //Debug.Log("Updating status bars of type "+type);
    }

    public void AddStatusBar(StatusBar s)
    {
        bars[(int) s.Type].Add(s);
    }

    private bool StatsChanged()
    {
        for (int i=0; i<prevStats.Length; i++)
        {
            if (prevStats[i] != PlayerController.Player.currentStats[i])
            {
                return true;
            }
        }
        return false;
    }
}
