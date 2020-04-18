using System;
using System.Collections;
using System.Threading;
using System.Security.Cryptography;
using Boo.Lang;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScavengeResultsSystem : MonoBehaviour
{
    
    public static ScavengeResultsSystem PopUpResultScavenging { get; private set; }
    public  MenuController menuController;
    public GameObject backPanel;
    public GameObject slotPrefab;
    public GameObject scavengingLogprefab;
    public GameObject itemContent;
    public GameObject scavengingLogContent;
    
    private List<GameObject> inventorySlots= new List<GameObject>();
    public TextMeshProUGUI message;
    private PlayerController player;

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar thirstBar;
    public StatusBar energyBar;

    private float[] goals;
    private int healthBarGoal;
    private int HungerBarGoal;
    private int thirstBarGoal;
    private int energyBarGoal;
    

    public void Awake()
    {
        player = PlayerController.Player;
        goals = new float[player.currentStats.Length];
        Array.Copy(player.currentStats, 0, goals, 0, player.currentStats.Length);
    }

    public void FixedUpdate()
    {
        InscSlider(healthBar,healthBarGoal);
        InscSlider(hungerBar,HungerBarGoal);
        InscSlider(thirstBar,thirstBarGoal);
        InscSlider(energyBar,energyBarGoal);
    }

    private void InscSlider(StatusBar statusBar, int goal)
    {
        if (goal < statusBar.GetValue())
        {
            statusBar.addValue(-1);
        }
        else if (goal>statusBar.GetValue())
        {
            statusBar.addValue(1);
        }
    }

    public void OnEnable()
    {
        PopResult(BackgroundTasks.Tasks.lastScavenging);
    }

    public void PopResult(Scavenging scavenging)
    {

        clearInventorySlot();
        
        //add show all Item
        foreach (Item item in scavenging.GetItemsFound())
        {
            GameObject obj = Instantiate(slotPrefab);
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.AddItem(item);
            obj.transform.SetParent(itemContent.transform, false);
            
            inventorySlots.Add(obj);
        }
        
        //menuController.OpenMenu(gameObject);
        
        //=============    REMOVE EXEMPLE TEXTE    ===================
        for (int i=0 ;i< scavengingLogContent.transform.childCount;i++)
        {
            Destroy(scavengingLogContent.transform.GetChild(i).gameObject); 
        }

        
        //=============    ADD SCAVENGING LOG    ===================


        foreach (string log in scavenging.scavengeLog)
        {
            GameObject obj = Instantiate(scavengingLogprefab);
            ScavengeLog scavengeLog = obj.GetComponent<ScavengeLog>();
            scavengeLog.SetText(log);

            obj.transform.SetParent(scavengingLogContent.transform, false);
        }

        //=============    ANIMATION    ================
        healthBar.SetValue(scavenging.oldStatusBar.health);
        hungerBar.SetValue(scavenging.oldStatusBar.hunger);
        thirstBar.SetValue(scavenging.oldStatusBar.thirst);
        energyBar.SetValue(scavenging.oldStatusBar.energy);
        
        //set objectif
        healthBarGoal = player.GetHealth();
        HungerBarGoal = player.GetHunger();
        thirstBarGoal = player.GetThirst();
        energyBarGoal = player.GetEnergy();

        if (!BackgroundTasks.Tasks.IsScavenging && BackgroundTasks.Tasks.lastScavenging != null)
        {
            BackgroundTasks.Tasks.lastScavenging = null;
        }

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