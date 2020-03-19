using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScavengeResultsSystem : MonoBehaviour
{
    
    public  MenuController menuController;
    public GameObject backPanel;
    public GameObject slotPrefab;
    public GameObject itemContent;
    private List<GameObject> inventorySlots= new List<GameObject>();
    public TextMeshProUGUI message;
    private PlayerController player;

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar thirstBar;
    public StatusBar energyBar;

    private float[] goals;
    

    public void Start()
    {
        player = PlayerController.Player;
        goals = new float[player.currentStats.Length];
        Array.Copy(player.currentStats, 0, goals, 0, player.currentStats.Length);
    }

    public void PopResult(List<Item> items,((int old, int now) health,(int old, int now) hunger,(int old, int now) thirst,(int old, int now) energy) statusBarUpdate)
    {
        
        clearInventorySlot();
        if (items.Count == 0)
        {
            Debug.Log("You didn't find anything");
        }

        //add show all Item
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(slotPrefab);
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.AddItem(item);
            obj.transform.SetParent(itemContent.transform, false);
            
            inventorySlots.Add(obj);
        }
        
        menuController.OpenMenu(gameObject);

        //=============    ANIMATION    ================
        healthBar.SetValue(statusBarUpdate.health.now);

        hungerBar.SetValue(statusBarUpdate.hunger.now);

        thirstBar.SetValue(statusBarUpdate.thirst.now);

        energyBar.SetValue(statusBarUpdate.energy.now);
    }

    private void clearInventorySlot()
    {
        /*foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }*/
        for (int i = 0; i < itemContent.transform.childCount; i++)
        {
            Destroy(itemContent.transform.GetChild(i).gameObject);
        }
        inventorySlots.Clear();
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(this.gameObject);
    }
}
