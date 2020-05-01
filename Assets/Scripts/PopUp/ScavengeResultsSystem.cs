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
    public TextMeshProUGUI titlePanel;
    private List<GameObject> inventorySlots= new List<GameObject>();
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

    private int nbrItemFound = 0;
    private int nbrLog = 0;
    public void FixedUpdate()
    {
        InscSlider(healthBar,healthBarGoal);
        InscSlider(hungerBar,HungerBarGoal);
        InscSlider(thirstBar,thirstBarGoal);
        InscSlider(energyBar,energyBarGoal);
        UpdateTitle();
        
        if (BackgroundTasks.Tasks.lastScavenging!=null && BackgroundTasks.Tasks.lastScavenging.GetItemsFound().Count != nbrItemFound)
        {
            clearInventorySlot();

            //add show all Item
            foreach (Item item in BackgroundTasks.Tasks.lastScavenging.GetItemsFound())
            {
                GameObject obj = Instantiate(slotPrefab);
                InventorySlot slot = obj.GetComponent<InventorySlot>();
                slot.AddItem(item);
                obj.transform.SetParent(itemContent.transform, false);
            
                inventorySlots.Add(obj);
            }

            nbrItemFound = BackgroundTasks.Tasks.lastScavenging.GetItemsFound().Count;
        }

        
        
        //=============    ADD SCAVENGING LOG    ===================
        if (BackgroundTasks.Tasks.lastScavenging!=null && BackgroundTasks.Tasks.lastScavenging.scavengeLog.Count != nbrLog)
        {
            foreach (string log in BackgroundTasks.Tasks.lastScavenging.scavengeLog)
            {
                GameObject obj = Instantiate(scavengingLogprefab);
                ScavengeLog scavengeLog = obj.GetComponent<ScavengeLog>();
                scavengeLog.SetText(log);

                obj.transform.SetParent(scavengingLogContent.transform, false);
            }

            nbrLog = BackgroundTasks.Tasks.lastScavenging.scavengeLog.Count;
        }
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
        backPanel.SetActive(true);
        UpdateTitle();
        
        PopResult(BackgroundTasks.Tasks.lastScavenging);
        
    }
    
    

    private void UpdateTitle()
    {
        
            TimeSpan totalDt = BackgroundTasks.Tasks.EndScavenging - BackgroundTasks.Tasks.StartScavenging;
            TimeSpan myDelta = DateTime.Now - BackgroundTasks.Tasks.StartScavenging;
            int pourcentage = (int) (((double) ((double) myDelta.Ticks / (double) totalDt.Ticks)) * 100);

            int show = Math.Min(pourcentage, (int) (pourcentage / 10)) * 10;

            titlePanel.SetText("Summary of your Journey (" + Math.Min(show, 100) + "%)");
    }

    public void PopResult(Scavenging scavenging)
    {

        clearInventorySlot();
        
        //Force to reload itemfound and log
        nbrItemFound = 0;
        nbrLog = 0;
        
        //=============    REMOVE EXEMPLE TEXTE    ===================
        for (int i=0 ;i< scavengingLogContent.transform.childCount;i++)
        {
            Destroy(scavengingLogContent.transform.GetChild(i).gameObject); 
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

    }

    private void clearInventorySlot()
    {
        for (int i = 0; i < itemContent.transform.childCount; i++)
        {
            Destroy(itemContent.transform.GetChild(i).gameObject);
        }
        inventorySlots.Clear();
    }

    public void ExitBtnClicked()
    {
        if (!BackgroundTasks.Tasks.IsScavenging && BackgroundTasks.Tasks.lastScavenging != null)
        {
            BackgroundTasks.Tasks.lastScavenging = null;
        }

        nbrItemFound = 0;
        nbrLog = 0;
        menuController.ExitMenu(this.gameObject);
        
    } 
    
}