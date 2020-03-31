﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class GameController : MonoBehaviour
{

    //[Tooltip("Contain the object player")] 
    //public PlayerController player;
    
    [Space]
    [Range(0f, 24f)]
    public float gameClock = 8f;

    //public TMP_Text clock;
    public GameObject BunkerPanel;
    public GameObject transitionPanel;
    public Animator transitionAnim;
    public GameData loaded;
    public static bool NewGame { get; set; }

    public ClockController clock; 

    // Start is called before the first frame update
    void Start() {
        UpdateGameClock(gameClock);
        BunkerPanel.SetActive(true);
        transitionPanel.SetActive(true);
        StartCoroutine(TransitionAnim());
        if (NewGame)
        {
            ErasePreviousSave();
        }
        else
        {
            LoadGame();
        }
    }

    private void ErasePreviousSave()
    {
        Debug.Log("NewGame");
        Save();
    }

    private void LoadGame()
    {
        GameData data = SaveSystem.Load();
        if (data != null)
        {
            loaded = data;
            PlayerController.Player.currentStats[(int)StatType.Health] = data.health;
            PlayerController.Player.currentStats[(int)StatType.Hunger] = data.hunger;
            PlayerController.Player.currentStats[(int)StatType.Thirst] = data.thirst;
            PlayerController.Player.currentStats[(int)StatType.Energy] = data.energy;

            UpdateGameClock(data.gameClock);

            foreach (string s in data.equippedGear)
            {
                Gear g = Resources.Load("Items/Gear/" + s) as Gear;
                if (g != null)
                {
                    PlayerController.Player.EquipGear(g);
                }
            }

            foreach (string s in data.consumables)
            {
                Consumable c = Resources.Load("Items/Consumables/" + s) as Consumable;
                if (c != null)
                {
                    InventoryManager.Inventory.AddItem(c);
                }
            }
            foreach (string s in data.equipment)
            {
                Gear g = Resources.Load("Items/Gear/" + s) as Gear;
                if (g != null)
                {
                    InventoryManager.Inventory.AddItem(g);
                }
            }
            foreach (string s in data.junks)
            {
                Junk j = Resources.Load("Items/Junks/" + s) as Junk;
                if (j != null)
                {
                    InventoryManager.Inventory.AddItem(j);
                }
            }
            foreach (string s in data.resources)
            {
                Resource r = Resources.Load("Items/Resources/" + s) as Resource;
                if (r != null)
                {
                    InventoryManager.Inventory.AddItem(r);
                }
            }
            Debug.Log("Save loaded!");
            NewGame = false;
        }
    }

    private IEnumerator TransitionAnim()
    {
        transitionAnim.SetTrigger("in");
        yield return new WaitForSeconds(0.5f);
    }    

    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        double hours = Mathf.Floor(gameClock);
        double minutes = Mathf.Abs(Mathf.Ceil((gameClock - Mathf.Ceil(gameClock)) * 60));
        clock.UpdateClock((int) hours, (int) minutes);
        //clock.SetText("Clock : " + hours.ToString("0") + "h" + minutes.ToString("0"));
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        GameData data = new GameData(PlayerController.Player, InventoryManager.Inventory, gameClock);
        SaveSystem.Save(data);
    }
}
