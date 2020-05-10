using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public PopupSystem pop;

    public SpriteRenderer bunker;
    public Sprite bunkerWithoutLights;
    public Sprite bunkerWithLights;
    public GameObject filtreSombre;
    public GameObject lights;

    public bool IsScavenging { get; set; }
    public DateTime StartScavenging { get;  set; }
    public DateTime EndScavenging { get; set; }

    [SerializeField] private GameObject scavengePopUpResultPanel;
    [SerializeField] private MenuController menuController;
    [SerializeField] private BunkerController bunkerController;
    [SerializeField] public SnackbarController snackbarController;

    private float hStep = 0f;
    private float tStep = 0f;
    private float sStep = 0f;

    void Awake()
    {
        Tasks = this;
        IsSleeping = false;
        if (!IsScavenging || lastScavenging == null)
        {
            IsScavenging = false;
        }
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
                sStep += sleepInc;
                BackgroundSleep();
            }
        }
        if (IsScavenging)
        {
            DateTime now = DateTime.Now;
            if (EndScavenging!=null && now > EndScavenging && actualScavengingStep >= totalScavengingSteps)
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
        SwitchLightsOff();

        this.lastScavenging = new Scavenging();

        actualScavengingStep = 0;
        totalScavengingSteps = (int)(scavengeTime * 2);

        if (totalScavengingSteps == 0)
        {
            return;
        }

        Debug.Log("*****    START SCAVENGING    ************");

        this.scavengeTime = scavengeTime;


        StartScavenging = DateTime.Now;
        EndScavenging = StartScavenging.AddSeconds(5 * scavengeTime);

        // Real implem : endTime = startTime.AddMinutes(scavengeTime);
        // Debug.Log("scavengeTime : " + scavengeTime);

        TimeSpan deltaT = (EndScavenging - StartScavenging);
        TimeSpan dt = new TimeSpan(deltaT.Ticks / totalScavengingSteps);

        List<DateTime> stepsTime = new List<DateTime>();
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
        snackbarController.ShowSnackBar("You'll be back from scavening in "+deltaT.Minutes+"m:"+deltaT.Seconds+"s");
        



    }
    private bool hadSenario = false;
    private bool ev = false;
    public Scavenging lastScavenging;
    private double scavengeTime;

    private void Scavenge(DateTime now)
    {
        SwitchLightsOff();

        for (; actualScavengingStep <= totalScavengingSteps && now >= scavengingPalier[actualScavengingStep]; actualScavengingStep++)
        {
            Debug.Log("*******    SCAVENGING STEP " + actualScavengingStep + "/" + totalScavengingSteps + "**************");
            GameController.Controller.UpdateGameClock(0.5f);

            var rand = new System.Random();
            double chance = rand.NextDouble(); // 0.0 <= chance < 1.0
            if (chance > 0.75)/*========== FIND A ITEM  ===========*/
            {

                //chances for the found item, has to be balance when all items in the game
                chance = rand.NextDouble();

                int myLevel = 100;

                (string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] possibleItem;

                if (chance > 0.9)/*========== GEAR ===========*/
                {
                    //retourn tt les items que je pourrais possiblement trouver sur mon chemin en fonction de mon level, du type d'objet et du temps que j'ai décidé de sortir
                    possibleItem = lastScavenging.getMyLevelItems(myLevel, Item.ItemClass.Gear, scavengeTime);

                    if (possibleItem != null && possibleItem.Length > 0)
                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length) - 1); //index random parmis les objets possible
                        Gear element = Resources.Load<Gear>(possibleItem[index].link); //load l'item
                        //Debug.Log("Find" + element.name);//print
                        addItemFound(element, Item.ItemClass.Gear);//add to inventory
                    }
                }
                else if (chance > 0.45)/*========== Consumable ===========*/
                {
                    possibleItem = lastScavenging.getMyLevelItems(myLevel, Item.ItemClass.Consumable, scavengeTime);
                    if (possibleItem != null && possibleItem.Length > 0)
                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length - 1));
                        Consumable element = Resources.Load<Consumable>(possibleItem[index].link);
                        //Debug.Log("Find" + element.name);
                        addItemFound(element, Item.ItemClass.Consumable);
                    }
                }
                else if (chance > 0.00)/*========== RESSOURCES ===========*/
                {
                    possibleItem = lastScavenging.getMyLevelItems(myLevel, Item.ItemClass.Resource, scavengeTime);
                    if (possibleItem != null && possibleItem.Length > 0)

                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length - 1));
                        Resource element = Resources.Load<Resource>(possibleItem[index].link);
                        //Debug.Log("Find" + element.name);
                        addItemFound(element, Item.ItemClass.Resource);
                    }
                }
                

                //junk not implemented


            }
            else if (!hadSenario)/*========== START SENARIO ===========*/
            {
                chance = rand.NextDouble();
                if (chance > 0.85)
                {
                    Debug.Log("Start senario");

                    if (totalScavengingSteps - actualScavengingStep > 12) //level 3 -> ZombieLot, PoliceStation, Radio, Death
                    {
                        chance = rand.NextDouble();
                        if (chance > 0.999) pop.PopMessage(PopupSystem.Popup.DeathEvent);
                        else if (chance > 0.90)
                        {
                            if (lastScavenging.MayUseWeapon(1))
                            {
                                pop.PopMessage(PopupSystem.Popup.ZombieLot1);
                            }
                            else
                            {
                                lastScavenging.scavengeLog.Add("You were attacked by a large group of zombies!");
                                pop.PopMessage(PopupSystem.Popup.ZombieLot0);
                            }
                        }
                        else if (chance > 0.80)
                        {
                            pop.PopMessage(PopupSystem.Popup.HuntingStore);
                            Gear element = Resources.Load<Gear>("Items/Gear/Gun");
                            addItemFound(element, Item.ItemClass.Gear);
                        }
                        else
                        {
                            pop.PopMessage(PopupSystem.Popup.RadioParts); //gives a random radio part

                            //chance range is 0.80-0.00, there are 5 radio parts so 80/5 = 16.0
                            if (chance > 0.64)
                            {
                                Resource element1 = Resources.Load<Resource>("Items/Resources/Diode");
                                addItemFound(element1, Item.ItemClass.Resource);
                            }
                            else if (chance > 0.48)
                            {
                                Resource element2 = Resources.Load<Resource>("Items/Resources/TuningCoil");
                                addItemFound(element2, Item.ItemClass.Resource);
                            }
                            else if (chance > 32)
                            {
                                Resource element3 = Resources.Load<Resource>("Items/Resources/Antenna");
                                addItemFound(element3, Item.ItemClass.Resource);
                            }
                            else if (chance > 0.16)
                            {
                                Resource element4 = Resources.Load<Resource>("Items/Resources/Capacitor");
                                addItemFound(element4, Item.ItemClass.Resource);
                            }

                            else
                            {
                                Resource element5 = Resources.Load<Resource>("Items/Resources/Speaker");
                                addItemFound(element5, Item.ItemClass.Resource);
                            }
                        }

                    }
                    else if (totalScavengingSteps - actualScavengingStep > 6) //level 2 -> ZombieFew, HuntingStore, OutdoorStore, Pharmacy, Death
                    {
                        chance = rand.NextDouble();
                        if (chance > 0.999) pop.PopMessage(PopupSystem.Popup.DeathEvent);
                        else if (chance > 0.75)
                        {
                            pop.PopMessage(PopupSystem.Popup.SportStore);
                            Gear element = Resources.Load<Gear>("Items/Gear/BaseballBat");
                            addItemFound(element, Item.ItemClass.Gear);
                        }
                        else if (chance > 0.50)
                        {
                            pop.PopMessage(PopupSystem.Popup.PoliceStation);

                            //chance range is 0.75 - 0.50
                            if (chance > 0.60)
                            {
                                Gear element = Resources.Load<Gear>("Items/Gear/Greaves");
                                addItemFound(element, Item.ItemClass.Gear);
                            }
                            else if (chance > 0.70)
                            {
                                Gear element = Resources.Load<Gear>("Items/Gear/Helmet");
                                addItemFound(element, Item.ItemClass.Gear);
                            }
                            else
                            {
                                Gear element = Resources.Load<Gear>("Items/Gear/Chestplate");
                                addItemFound(element, Item.ItemClass.Gear);
                            }
                        }
                        else if (chance > 0.25)
                        {
                            pop.PopMessage(PopupSystem.Popup.Pharmacy);
                            Consumable element = Resources.Load<Consumable>("Items/Consumables/Medkit");
                            addItemFound(element, Item.ItemClass.Consumable);
                        }
                        else
                        {
                            if (lastScavenging.MayUseWeapon(1))
                            {
                                pop.PopMessage(PopupSystem.Popup.ZombieFew1);
                            }
                            else
                            {   
                                lastScavenging.scavengeLog.Add("A bunch of zombies attacked you");
                                pop.PopMessage(PopupSystem.Popup.ZombieFew0);
                            }
                        }
                    }
                    else //level 1 -> ZombieOne, GroceryStore, ClothingStore, Parc, Death
                    {
                        chance = rand.NextDouble();
                        if (chance > 0.999) pop.PopMessage(PopupSystem.Popup.DeathEvent);
                        else if (chance > 0.75)
                        {
                            pop.PopMessage(PopupSystem.Popup.Parc);
                            Resource element = Resources.Load<Resource>("Items/Resources/Wood");
                            addItemFound(element, Item.ItemClass.Resource);
                            addItemFound(element, Item.ItemClass.Resource);
                            addItemFound(element, Item.ItemClass.Resource);
                        }
                        else if (chance > 0.50)
                        {
                            pop.PopMessage(PopupSystem.Popup.GroceryStore);
                            Resource element1 = Resources.Load<Resource>("Items/Resources/CoffeeBeans");
                            addItemFound(element1, Item.ItemClass.Resource);
                            Resource element2 = Resources.Load<Resource>("Items/Resources/TeaLeaf");
                            addItemFound(element2, Item.ItemClass.Resource);
                            Consumable element3 = Resources.Load<Consumable>("Items/Consumables/Crackers");
                            addItemFound(element3, Item.ItemClass.Consumable);
                        }
                        else if (chance > 0.25)
                        {
                            pop.PopMessage(PopupSystem.Popup.ClothingStore);
                            Resource element = Resources.Load<Resource>("Items/Resources/Cloth");
                            addItemFound(element, Item.ItemClass.Resource);
                            addItemFound(element, Item.ItemClass.Resource);
                            addItemFound(element, Item.ItemClass.Resource);
                        }
                        else
                        {
                            if (lastScavenging.MayUseWeapon(1))
                            {
                                pop.PopMessage(PopupSystem.Popup.ZombieOne1);
                            }
                            else
                            {
                                lastScavenging.scavengeLog.Add("A zombie attacked you");
                                pop.PopMessage(PopupSystem.Popup.ZombieOne0);
                            }
                        }
                    }

                    hadSenario = true;
                }

            }
            else if (!ev) /*======    MEET MONSTRER, BE BITTEN, DEAD, LOST ITEMS,...    ====== */
            {
                chance = rand.NextDouble();
                if (chance > 0.999) pop.PopMessage(PopupSystem.Popup.DeathEvent);
                else if (chance > 0.80)
                {
                    //=========    BITTEN    =========
                    double probaWeapon;
                    Gear getGear = PlayerController.Player.GetGear(Gear.ItemType.Weapon);
                    if (getGear != null)
                    {
                        if (getGear.filename== "Gun")
                        {
                            probaWeapon = 0.9;
                        }else if (getGear.filename == "Knife")
                        {
                            probaWeapon = 0.7;
                        }else if (getGear.filename== "IronPipe")
                        {
                            probaWeapon = 0.5;
                        }else if (getGear.filename== "BaseballBat")
                        {
                            probaWeapon = 0.4;
                        }
                        else
                        {
                            probaWeapon = 0.2;
                        }
                    }
                    else
                    {
                        probaWeapon = 0;
                    }



                    if (lastScavenging.MayUseWeapon(probaWeapon))
                    {
                        Debug.Log("Weapon has been used");
                        pop.PopMessage(PopupSystem.Popup.ZombieUseWeapon);
                    }
                    else if (lastScavenging.MyUseArmor(0.8))
                    {
                        Debug.Log("Armor has been used");
                        pop.PopMessage(PopupSystem.Popup.ZombieUseArmor);
                        PlayerController.Player.UpdateHealth(-5); //update heamth   
                        lastScavenging.scavengeLog.Add("You were bitten by a zombie (-5)");
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

        SwitchLightsOn();

        addItemFoundToInventory(lastScavenging);
        hadSenario = false;
        snackbarController.ShowSnackBar("You are back from scavenging");
        if (pop.gameObject.activeSelf)
        {
            pop.OkBtnClicked();
        }

        if (!scavengePopUpResultPanel.activeSelf)
        {
            bunkerController.ScavengeButtonClicked();
            
        }


    }

    /**
     * Rajoute les items trouvé dans l'inventaire et dans la liste des items trouvés qui sera ensuite transmis
     * à la popUp pour visualisé les résultats
     */
    private void addItemFound(Item item, Item.ItemClass itemClass)
    {
        lastScavenging.itemsFound.Add((item, itemClass));
    }


    private void addItemFoundToInventory(Scavenging scavenging)
    {
        foreach ((Item item, Item.ItemClass itemClass) in scavenging.itemsFound)
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

        SwitchLightsOff();

        startSleeping = DateTime.Now;
        endSleeping = startSleeping.AddSeconds(2 * sleepTime * updateStep);
    }

    private void BackgroundSleep()
    {
        if (startSleeping < endSleeping)
        {
            if (sStep >= 1f)
            {
                PlayerController.Player.UpdateEnergy((int)sStep);
                sStep = 0f;
            }
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
            GameController.Controller.UpdateGameClock(0.5f);
        }
        else
        {
            IsSleeping = false;

            SwitchLightsOn();

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

    /*==========================    Switch lights    ==========================================*/
    private void SwitchLightsOn()
    {
        bunker.sprite = bunkerWithLights;
        filtreSombre.SetActive(false);
        lights.SetActive(true);
    }

    private void SwitchLightsOff()
    {
        bunker.sprite = bunkerWithoutLights;
        filtreSombre.SetActive(true);
        lights.SetActive(false);
    }

}
