﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    //private static StatusBarController statusBarController;
    public static StatusBarController SBController
    {
        get; private set;
    }

    private List<StatusBar>[] bars;

    private PlayerController player = PlayerController.Player;

    private void Awake()
    {
        player = PlayerController.Player;
        SBController = this;
        bars = new List<StatusBar>[] { new List<StatusBar>(), new List<StatusBar>(), new List<StatusBar>(), new List<StatusBar>(), new List<StatusBar>() };
    }

    public void UpdateStatusBars(StatType type)
    {
        foreach (StatusBar s in bars[(int)type])
        {
            s.SetValue(player.currentStats[(int)type]);
            Debug.Log(s + " set to " + player.currentStats[(int)type]);
        }

        Debug.Log("Updating status bars of type "+type);
    }

    public void AddStatusBar(StatusBar s)
    {
        bars[(int) s.Type].Add(s);
    }
}
