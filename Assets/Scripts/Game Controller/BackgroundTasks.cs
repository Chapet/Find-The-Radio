using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTasks : MonoBehaviour
{
    public static BackgroundTasks Tasks { get; private set; }

    public bool IsSleeping { get; private set; }
    private DateTime startSleeping;
    private DateTime endSleeping;
    public float updateStep;
    public int sleepInc;
    public float hungerMultiplier;
    public float thirstMultiplier;

    public bool IsScavenging { get; set; }
    public DateTime StartScavenging { get; private set; }
    public DateTime EndScavenging { get; set; }

    [SerializeField] private GameObject scavengePopUpResultPanel;
    [SerializeField] private MenuController menuController;
    [SerializeField] private BunkerController bunkerController;
    [SerializeField] private SnackbarController snackbarController;

    private float hStep = 0f;
    private float tStep = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Tasks = this;
        IsSleeping = false;
        if(!IsScavenging||lastScavenging==null)
            IsScavenging = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSleeping)
        {
            var diffInSeconds = (DateTime.Now - startSleeping).TotalSeconds;
            if (diffInSeconds > updateStep)
            {
                hStep += sleepInc * hungerMultiplier;
                tStep += sleepInc * thirstMultiplier;
                BackgroundSleep();
            }          
        }
        if (IsScavenging)
        {
            DateTime now = DateTime.Now;
            if(now>EndScavenging && actualScavengingStep>=totalScavengingSteps)
            {
                Debug.Log("*********    FIN SCAVENGING    *************");
                IsScavenging = false;

                Debug.Log("Returning from lastScavenging :-)");
                ReturnFromScavenging();
                return;
            }
            Scavenge(now);
        }
    }
    
    
   
    /*==============================================================================================*/
    /*=========    SCAVENGING     ==================================================================*/
    /*==============================================================================================*/
    
    public int ActualScavengingStep => actualScavengingStep;
    public int TotalScavengingSteps => totalScavengingSteps;

    public int actualScavengingStep;
    public int totalScavengingSteps;
    public List<DateTime> scavengingPalier;
    
    
    
    public void StartNewScavenging(double scavengeTime)
    {
        this.lastScavenging=new Scavenging();
        
        actualScavengingStep = 0;
        totalScavengingSteps = (int) (scavengeTime * 2);
        
        if (totalScavengingSteps == 0)
        {
            return;
        }

        Debug.Log("*****    START SCAVENGING    ************");

        this.scavengeTime = scavengeTime;
        
        
        StartScavenging = DateTime.Now;
        EndScavenging = StartScavenging.AddSeconds(5*scavengeTime);
        
        // Real implem : endTime = startTime.AddMinutes(scavengeTime);
        // Debug.Log("scavengeTime : " + scavengeTime);
        
        TimeSpan deltaT = (EndScavenging - StartScavenging);
        TimeSpan dt=new TimeSpan(deltaT.Ticks/totalScavengingSteps);
        
        List<DateTime> stepsTime=new List<DateTime>();
        stepsTime.Add(StartScavenging);
        Debug.Log("Palier:");
        for (int i = 1; i <= totalScavengingSteps; i++)
        {
            DateTime palier = stepsTime[i - 1] + dt;
            Debug.Log(palier);
            stepsTime.Add(palier);
        }

        scavengingPalier = stepsTime;
        IsScavenging = true;



    }
    private bool hadSenario = false;
    private bool ev = false;
    public Scavenging lastScavenging;
    private double scavengeTime;
    
    private void Scavenge(DateTime now)
    {
        for(;actualScavengingStep<=totalScavengingSteps && now>=scavengingPalier[actualScavengingStep];actualScavengingStep++){
            Debug.Log("*******    SCAVENGING STEP "+actualScavengingStep+"/"+totalScavengingSteps+"**************");
            
            
            var rand = new System.Random();
            double chance  = rand.NextDouble(); // 0.0 <= chance < 1.0
            if (chance > 0.66)/*========== FIND A ITEM  ===========*/
            {

                //chances for the found item, has to be balance when all items in the game
                chance = rand.NextDouble();
                
                int myLevel = 100;//TODO: link with player
                
                (string link,int minLevel,Item.ItemClass itemType,double minTimeOut)[] possibleItem;

                if (chance > 0.9)/*========== GEAR ===========*/
                {
                    //retourn tt les items que je pourrais possiblement trouver sur mon chemin en fonction de mon level, du type d'objet et du temps que j'ai décidé de sortir
                    possibleItem= lastScavenging.getMyLevelItems( myLevel,Item.ItemClass.Gear,scavengeTime);
                    
                    if (possibleItem != null && possibleItem.Length>0)
                    {
                        int index = (int) (rand.NextDouble() * (possibleItem.Length) - 1); //index random parmis les objets possible
                        Gear element = Resources.Load<Gear>(possibleItem[index].link); //load l'item
                        //Debug.Log("Find" + element.name);//print
                        addItemFound(element,Item.ItemClass.Gear);//add to inventory
                    }
                }
                else if (chance > 0.45)/*========== Consumable ===========*/
                {
                    possibleItem= lastScavenging.getMyLevelItems(myLevel,Item.ItemClass.Consumable,scavengeTime);
                    if (possibleItem != null&& possibleItem.Length>0)
                    {
                        int index = (int) (rand.NextDouble() * (possibleItem.Length - 1));
                        Consumable element = Resources.Load<Consumable>(possibleItem[index].link);
                        //Debug.Log("Find" + element.name);
                        addItemFound(element,Item.ItemClass.Consumable);
                    }
                }
                else if (chance > 0.15)/*========== RESSOURCES ===========*/
                {
                    possibleItem= lastScavenging.getMyLevelItems(myLevel,Item.ItemClass.Resource,scavengeTime);
                    if (possibleItem != null&& possibleItem.Length>0)

                    {
                        int index = (int) (rand.NextDouble() * (possibleItem.Length - 1));
                        Resource element = Resources.Load<Resource>(possibleItem[index].link);
                        //Debug.Log("Find" + element.name);
                        addItemFound(element,Item.ItemClass.Resource);
                    }
                }else if (chance > 0.0) /*========== JUNK ===========*/
                {
                    possibleItem= lastScavenging.getMyLevelItems( myLevel,Item.ItemClass.Junk,scavengeTime);
                    if (possibleItem != null&& possibleItem.Length>0)
                    {
                        int index = (int) (rand.NextDouble() * (possibleItem.Length - 1));
                        Junk element = Resources.Load<Junk>(possibleItem[index].link);
                        //Debug.Log("Find" + element.name);
                        addItemFound(element,Item.ItemClass.Junk);
                    }
                }
                
       
            }
            else if (!hadSenario)/*========== START SENARIO ===========*/
            {
                Debug.Log("Start senario");
                hadSenario = true;
            }
            else if (!ev) /*======    MEET MONSTRER, BE BITTEN, DEAD, LOST ITEMS,...    ====== */
            {
                chance = rand.NextDouble();
                if (chance > 0.999) PopupSystem.Popup_System.PopMessage(PopupSystem.Popup.Death);
                else if (chance > 0.80)
                {
                    //=========    BITTEN    =========
                    if (lastScavenging.MayUseGun(0.8))
                    {
                        Debug.Log("Weapon has been used");
                    }
                    else if (lastScavenging.MyUseArmor(0.8))
                    {
                        Debug.Log("Armor has been used");
                    }
                    else
                    {
                        lastScavenging.scavengeLog.Add("You were bitten by a zombie (-10)"); //ADD log/message
                        PlayerController.Player.UpdateHealth(-10); //update heamth    
                    }
                    
                }
            }
            
            PlayerController.Player.UpdateEnergy(-2);
            PlayerController.Player.UpdateHunger(-2);
            PlayerController.Player.UpdateThirst(-2);
            
        }
    }

    private void ReturnFromScavenging()
    {
        addItemFoundToInventory(lastScavenging);
        snackbarController.StartSnackBare("You are back from scavenging");
        return;

        /*
        if (scavengePopUpResultPanel.activeSelf)
        {
            scavengePopUpResultPanel.GetComponent<ScavengeResultsSystem>().PopResult(lastScavenging);   
        }
        else
        {
            bunkerController.GoToBunker();
            menuController.OpenMenu(scavengePopUpResultPanel);
        }
        */
        
       //scavengePopUpResultPanel.SetActive(true);
       //var vv = scavengePopUpResultPanel.GetComponent<ScavengeResultsSystem>();
       //vv.PopResult(lastScavenging);
    }

    /**
     * Rajoute les items trouvé dans l'inventaire et dans la liste des items trouvés qui sera ensuite transmis
     * à la popUp pour visualisé les résultats
     */
    private void addItemFound(Item item, Item.ItemClass itemClass)
    {
        lastScavenging.itemsFound.Add((item,itemClass));
    }


    private void addItemFoundToInventory(Scavenging scavenging)
    {
        foreach ((Item item,Item.ItemClass itemClass) in scavenging.itemsFound)
        {
            if (itemClass == Item.ItemClass.Consumable)
            {
                InventoryManager.Inventory.AddItem(Instantiate(item as Consumable));
            }
            else if (itemClass == Item.ItemClass.Gear)
            {
                InventoryManager.Inventory.AddItem(Instantiate(item as Gear));            
            }
            else if (itemClass == Item.ItemClass.Junk)
            {
                InventoryManager.Inventory.AddItem(Instantiate(item as Junk));
            }
            else if (itemClass == Item.ItemClass.Resource)
            {
                InventoryManager.Inventory.AddItem(Instantiate(item as Resource));
            }
        }
        
    }
    
    
    
    /*==========================    END SCAVINGING    ==========================================*/


    public void Sleep(float sleepTime)
    {
        IsSleeping = true;
        startSleeping = DateTime.Now;
        endSleeping = startSleeping.AddSeconds(sleepTime * updateStep);
    }

    private void BackgroundSleep()
    {
        if (startSleeping < endSleeping)
        {
            PlayerController.Player.UpdateEnergy(sleepInc);
            if (hStep >= 1f)
            {
                PlayerController.Player.UpdateHunger(-1 * (int)hStep);
                hStep = 0f;
            }
            if (tStep >= 1f)
            {
                PlayerController.Player.UpdateThirst(-1 * (int)tStep);
                tStep = 0f;
            }
            startSleeping = startSleeping.AddSeconds(updateStep);
            GameController.Controller.UpdateGameClock(1);
        }
        else
        {
            IsSleeping = false;
            hStep = 0f;
            tStep = 0f;
        }
    }

    public void Use(Consumable toUse, float delay = 0f)
    {
        StartCoroutine(DelayedUse(delay, toUse));
    }




    private IEnumerator DelayedUse(float delay, Consumable cons)
    {

        Debug.Log("Delayed use ...");
        yield return new WaitForSeconds(delay);

        PlayerController.Player.UpdateEnergy(cons.GetEnergy());
        PlayerController.Player.UpdateHealth(cons.GetHealth());
        PlayerController.Player.UpdateHunger(cons.GetHunger());
        PlayerController.Player.UpdateThirst(cons.GetThirst());

        yield return null;
    }
}
