using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class GameController : MonoBehaviour
{

    //[Tooltip("Contain the object player")] 
    //public PlayerController player;
    
    [Space]
    [Range(0f, 24f)]
    public float gameClock = 8f;

    public static int framerate = 90;
    public static GameController Controller { get; private set; }

    public float autoSaveTiming = 30f;
    private float timePrevSave = 0f;

    public GameObject BunkerPanel;
    public GameObject transitionPanel;
    public Animator transitionAnim;
    public GameData loaded;
    [SerializeField] private Introduction _introduction;
    public PopupSystem pop;

    public static bool NewGame { get; set; }

    public ClockController clock;

    private void Awake()
    {
        Controller = this;
        Application.targetFrameRate = framerate;
    }

    // Start is called before the first frame update
    void Start() {
        BunkerPanel.SetActive(true);
        MenuController.Transition(transitionPanel, transitionAnim);
        NotificationManager.Manager.CancelAllNotifications();

        if (!NewGame)
        {
            LoadGame();
        }

        if (NewGame)
        {
            ResetGame();
            UpdateGameClock(8f);
            CleanSave();
            PlayerController.IS_FIRST_GAME = true;
        }
        

        if (PlayerController.IS_FIRST_GAME)
        {
            _introduction.StartIntroduction();
        }
        timePrevSave = 0f;
    }

    private void CleanSave()
    {
        Save();
    }

    private bool dead = false;
    private void FixedUpdate()
    {
        if(timePrevSave >= autoSaveTiming - Time.fixedDeltaTime)
        {
            Save();
            Debug.Log("Autosave ...");
        }
        timePrevSave = (timePrevSave + Time.fixedDeltaTime) % autoSaveTiming;
        
        if (PlayerController.Player.GetHealth()<=0&&dead==false)
        {
            dead = true;
            pop.PopMessage(PopupSystem.Popup.Death);
        }
    }

    public static void ResetGame()
    {
        //DirectoryInfo save_dir = new DirectoryInfo(SaveSystem.path);
        //save_dir.Delete(true);
        if(File.Exists(SaveSystem.path))
        {
            File.Delete(SaveSystem.path);
            Debug.Log("Previous save deleted");
        }
        else
        {
            Debug.Log("No previous to delete");
        }
    }

    private void LoadGame()
    {
        GameData data = SaveSystem.Load();
        if (data != null)
        {
            PlayerController.IS_FIRST_GAME = data.is_firstGame;
            loaded = data;
            PlayerController.Player.currentStats[(int)StatType.Health] = data.health;
            PlayerController.Player.currentStats[(int)StatType.Hunger] = data.hunger;
            PlayerController.Player.currentStats[(int)StatType.Thirst] = data.thirst;
            PlayerController.Player.currentStats[(int)StatType.Energy] = data.energy;

            UpdateGameClock(data.gameClock);

            foreach (string s in data.consumables)
            {
                Consumable c = Instantiate(Resources.Load("Items/Consumables/" + s) as Consumable);
                if (c != null)
                {
                    InventoryManager.Inventory.AddItem(c);
                }
            }

            for(int i=0;i<data.equipment.Count;i++)
            {
                Gear g = Instantiate(Resources.Load("Items/Gear/" + data.equipment[i]) as Gear);
                g.liveGear = data.equipementLife[i];
                if (g != null)
                {
                    InventoryManager.Inventory.AddItem(g);
                }
            }
            
            foreach (string s in data.junks)
            {
                Junk j = Instantiate(Resources.Load("Items/Junks/" + s) as Junk);
                if (j != null)
                {
                    InventoryManager.Inventory.AddItem(j);
                }
            }
            foreach (string s in data.resources)
            {
                Resource r = Instantiate(Resources.Load("Items/Resources/" + s) as Resource);
                if (r != null)
                {
                    InventoryManager.Inventory.AddItem(r);
                }
            }

            //for (int i = 0; i < data.equippedGear.Count; i++)
            //{
            //    Gear g = Resources.Load("Items/Gear/" + data.equippedGear[i]) as Gear;
            //    g.liveGear = data.equippedGearLife[i];
            //    if (g != null)
            //    {
            //        PlayerController.Player.EquipGear(g);
            //    }
            //}

            for (int i = 0; i < data.equippedGear.Count; i++)
            {
                Gear equipped = Resources.Load("Items/Gear/" + data.equippedGear[i]) as Gear;
                if (equipped != null)
                {
                    Debug.Log(equipped + " is equipped");
                    foreach (Gear g in InventoryManager.Inventory.GetItems(typeof(Gear)))
                    {
                        if (g.IsSameAs(equipped) && g.liveGear == data.equippedGearLife[i])
                        {
                            Debug.Log(g + " is equivalent to "+ equipped);
                            PlayerController.Player.EquipGear(g);
                        }
                    }
                    
                }
            }

            Debug.Log("Save loaded!");
            NewGame = false;           
            
            /*=====    SCAVENGING    =======*/
            if (data.isScavenging)
            {
                BackgroundTasks.Tasks.actualScavengingStep = data.scavengingActualStep;
                BackgroundTasks.Tasks.totalScavengingSteps = data.scavengingTotalSteps;
            
                Scavenging scavenging= new Scavenging();
                //load itemsfound
                List<(Item item,Item.ItemClass itemClass)> itemsFound=new List<(Item item, Item.ItemClass itemClass)>();
                for (int i = 0; i < data.scavengingItemsFound_itemName.Count; i++)
                {
                    Item.ItemClass itemC = Item.ConvertStringToItemCLass(data.scavengingItemsFound_itemClass[i]);
                    itemsFound.Add((Item.LoadItem(data.scavengingItemsFound_itemName[i],itemC),itemC));
                }
            
                scavenging.itemsFound = itemsFound;
                scavenging.scavengeLog = data.ScavengeLog;
                scavenging.oldStatusBar = (data.scavengingOldStatusBar[0], data.scavengingOldStatusBar[1],
                    data.scavengingOldStatusBar[2], data.scavengingOldStatusBar[3]);

                BackgroundTasks.Tasks.lastScavenging = scavenging;

                List<DateTime> scavengepalier=new List<DateTime>();
                foreach (var palier in data.scavengingPalier)
                {
                    scavengepalier.Add(GameData.ConvertStringToDateTime(palier));
                }


                if (data.scavengingStartTime != null && data.scavengingEndTime != null)
                {
                    BackgroundTasks.Tasks.StartScavenging = GameData.ConvertStringToDateTime(data.scavengingStartTime);
                    BackgroundTasks.Tasks.EndScavenging = GameData.ConvertStringToDateTime(data.scavengingEndTime);
                    Debug.Log("After loading : " + BackgroundTasks.Tasks.StartScavenging + " - " + BackgroundTasks.Tasks.EndScavenging);
                }

                
                BackgroundTasks.Tasks.scavengingPalier = scavengepalier;
                BackgroundTasks.Tasks.IsScavenging = true;
            }
            else
            {
                BackgroundTasks.Tasks.IsScavenging = false;

            }
            Debug.Log("New State Of IsScavenging because loaddata"+ BackgroundTasks.Tasks.IsScavenging);

            /*========    END SCAVENGING    ========*/
        }
        else
        {
            NewGame = true;
        }
    }



    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        int hours = (int) Mathf.Floor(gameClock);
        int minutes = (int) Mathf.Round((gameClock - hours) * 60f);
        //Debug.Log("Clock : " + gameClock + " = " + hours.ToString("00") + "h" + minutes.ToString("00"));
        clock.UpdateClock(hours, minutes);
        AutoRegen.GetAutoRegen.DoRegen(inc);
        AutoDamage.GetAutoDamage.DoDamage(inc);
    }

    public void GoToMainMenu()
    {
        //Debug.Log("Going to the main menu ...");
        Save();
        MenuController.Transition(transitionPanel, transitionAnim, "MainMenuScene");
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Save();
            if (BackgroundTasks.Tasks.IsScavenging)
            {
                NotificationManager.Manager.SendDelayedNotif("Scavenging finished !", "Your vault dweller has returned from the harsh wasteland", BackgroundTasks.Tasks.EndScavenging);
            }
        }
        else
        {
            NotificationManager.Manager.CancelAllNotifications();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
        if (BackgroundTasks.Tasks.IsScavenging)
        {
            NotificationManager.Manager.SendDelayedNotif("Scavenging finished !", "Your vault dweller has returned from the harsh wasteland", BackgroundTasks.Tasks.EndScavenging);
        }
    }

    private void Save()
    {
        GameData data = new GameData(PlayerController.Player, InventoryManager.Inventory, gameClock);
        SaveSystem.Save(data);
    }
}
