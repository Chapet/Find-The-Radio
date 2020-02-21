﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class GameController : MonoBehaviour
{

    [Header("Scriptable objects")] 
    [Tooltip("Contain the object player")] 
    public Player Player;
    
    [Space]
    [Range(0f, 24f)]
    public float gameClock = 8f;

    public int maxHealth = 100;
    public int currentHealth;

    public int maxHunger = 100;
    public int currentHunger;

    public int maxThirst = 100;
    public int currentThirst;

    public int maxEnergy = 100;
    public int currentEnergy;

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar thirstyBar;
    public StatusBar energyBar;

    public TMP_Text clock;
    public GameObject BunkerPanel;

    // Start is called before the first frame update
    void Start() {
        //UpdateGameClock(gameClock);

        BunkerPanel.SetActive(true);
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);

        currentHunger = maxHunger;
        hungerBar.SetMaxValue(maxHunger);
        hungerBar.SetValue(maxHunger);

        currentThirst = maxThirst;
        thirstyBar.SetMaxValue(maxThirst);
        thirstyBar.SetValue(maxThirst);

        currentEnergy = maxEnergy;
        energyBar.SetMaxValue(maxEnergy);
        energyBar.SetValue(maxEnergy);
    }

    // Update is called once per frame
    void Update() {
    }

    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        String hours = Math.Truncate(gameClock).ToString("0");
        String minutes = Math.Truncate((gameClock - Math.Truncate(gameClock)) * 60).ToString("0");
        clock.SetText("Clock : " + hours + "h" + minutes);
    }
}
