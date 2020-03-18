﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Object = System.Object;
using Slider = UnityEngine.UI.Slider;

public class ScavengingController : MonoBehaviour
{
    public GameController gameController;
    public GameObject BunkerPanel;
    private InventoryManager inventory;
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text scavengeText;
    public GameObject Slider;
    private float scavengeTime;
    [Tooltip("Contain the object player")]
    private PlayerController player;
    public MenuController menuController;
    public PopupSystem pop;
    public ScavengeResultsSystem popUpResult;

    /*[SerializeField] [Tooltip("Contain all the items that you can find outside the bunker")]
    private ScavengingListItem itemList;
    */

    void Start()
    {
        itemsFound = new List<Item>();
        player = PlayerController.Player;
        inventory = InventoryManager.Inventory;
    }


    void Update()
    {
        scavengeTime = (Slider.GetComponent<Slider>().value) / 2;
        String hours = Math.Truncate(scavengeTime).ToString("0");
        String minutes = Math.Truncate((scavengeTime - Math.Truncate(scavengeTime)) * 60).ToString("0");
        scavengeText.SetText("Scavenge for : " + hours + "h" + minutes);
    }

    public void OnValueChanged(float newValue)
    {
        Debug.Log(gameObject.GetComponent<Slider>().value);
        Debug.Log(newValue);
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }

    public void ScavengingBtnClicked()
    {
        Scavenge();
        menuController.ExitMenu(gameObject);
    }


    /*========================= ALL ITEM SCAVENGE =================================*/
    /**
     * link: lien vers l'item
     * MinLevel: le libeau minimum de l'utilisateur pour avoir cet iem
     * itypeType: le type de ressources
     * scavengeTime: le temps minimum qu'il faut sortir pour trouuver cet item (en nbr d'heure)
     */
    private (string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] scavengeItems = new[]
    {
        /*======CONSUMABLE=====*/
        (link:"Items/Consumables/WaterBottle",minLevel:0,itemType:Item.ItemClass.Consumable,minTimeOut:0.0),
        (link:"Items/Consumables/FoodCan",minLevel:0,itemType:Item.ItemClass.Consumable,minTimeOut:0.0),
        (link:"Items/Consumables/Soda",minLevel:0,itemType:Item.ItemClass.Consumable,minTimeOut:0.0),
       
        
        /*======GEAR=====*/
        (link:"Items/Gear/Gun",minLevel:0,itemType:Item.ItemClass.Gear,minTimeOut:0.0),
        
        /*======RESOURCES=====*/
        (link:"Items/Resources/WoodLog",minLevel:0,itemType:Item.ItemClass.Ressouce,minTimeOut:0.0),        
        
        /*======Junk=====*/
        
        (link:"Items/Junks/Grass",minLevel:0,itemType:Item.ItemClass.Junk,minTimeOut:0.0),

    };




    /**
     * get all item that have a minLevl less of equal to mylevel, a type of type (consummable, ressources, junk and gear)
     * and that we can find with a our scavengeTime
     *
     * Return array of [<link> as string, <luck> as double, <minlevel> as int, <itemtype> as ItemType, <minTimeOut> as double]
     */
    private static (string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] getMyLevelItems((string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] tuple, int myLevel, Item.ItemClass type, double scavengeTime)
    {
        List<(string link, int minLevel, Item.ItemClass itemType, double minTimeOut)> result = new List<(string link, int minLevel, Item.ItemClass itemType, double minTimeOut)>();

        for (int i = 0; i < tuple.Length; i++)
        {
            if (tuple[i].minLevel <= myLevel && tuple[i].itemType == type && tuple[i].minTimeOut <= (double)scavengeTime)
            {
                result.Add(tuple[i]);
            }
        }
        return result.ToArray();
    }

    /*=============================================================================*/

    private List<Item> itemsFound;

    /**
     * Mets les items trouvés dans qq chose en attendant que la sortie soit finie et qu'on montre le résultat de la sortie.
     */
    private void addItemFound(Item item)
    {
        itemsFound.Add(item);
        inventory.AddItem(item);
    }

    void Scavenge()
    {
        //Debug.Log(scavengeTime);

        //nombre de tranche de 30 min
        int nbTimeSlice = (int)scavengeTime * 2;

        // ajout aléatoire d'items, le compte total ne dépassant pas le maximum transportable
        var rand = new System.Random();
        int nbFound = 0;
        double chance = 0;
        bool ev = false;

        for (int i = 0; i < nbTimeSlice && nbFound < player.maxCarryingSize; i++)
        {
            chance = rand.NextDouble(); // 0.0 <= chance < 1.0
            if (chance > 0.66)/*========== FIND A ITEM  ===========*/
            {
                nbFound++;

                //chances for the found item, has to be balance when all items in the game
                chance = rand.NextDouble();

                int myLevel = 100;//TODO: link with player

                (string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] possibleItem;

                if (chance > 0.9)/*========== GEAR ===========*/
                {
                    //retourn tt les items que je pourrais possiblement trouver sur mon chemin en fonction de mon level, du type d'objet et du temps que j'ai décidé de sortir
                    possibleItem = getMyLevelItems(scavengeItems, myLevel, Item.ItemClass.Gear, scavengeTime);

                    if (possibleItem != null && possibleItem.Length > 0)
                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length) - 1); //index random parmis les objets possible
                        Gear element = Resources.Load<Gear>(possibleItem[index].link); //load l'item
                        Debug.Log("Find" + element.name);//print
                        addItemFound(element);//add to inventory
                    }
                }
                else if (chance > 0.45)/*========== Consumable ===========*/
                {
                    possibleItem = getMyLevelItems(scavengeItems, myLevel, Item.ItemClass.Consumable, scavengeTime);
                    if (possibleItem != null && possibleItem.Length > 0)

                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length - 1));
                        Consumable element = Resources.Load<Consumable>(possibleItem[index].link);
                        Debug.Log("Find" + element.name);
                        addItemFound(element);
                    }
                }
                else if (chance > 0.20)/*========== RESSOURCES ===========*/
                {
                    possibleItem = getMyLevelItems(scavengeItems, myLevel, Item.ItemClass.Ressouce, scavengeTime);
                    if (possibleItem != null && possibleItem.Length > 0)

                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length - 1));
                        Resource element = Resources.Load<Resource>(possibleItem[index].link);
                        Debug.Log("Find" + element.name);
                        addItemFound(element);
                    }
                }
                else if (chance > 0) /*========== JUNK ===========*/
                {
                    possibleItem = getMyLevelItems(scavengeItems, myLevel, Item.ItemClass.Junk, scavengeTime);
                    if (possibleItem != null && possibleItem.Length > 0)

                    {
                        int index = (int)(rand.NextDouble() * (possibleItem.Length - 1));
                        Junk element = Resources.Load<Junk>(possibleItem[index].link);
                        Debug.Log("Find" + element.name);
                        addItemFound(element);
                    }
                }
            }
            else if (true)/*========== START SENARIO ===========*/
            {

            }

            if (!ev && false)
            {
                chance = rand.NextDouble();
                if (chance > 0.999) pop.PopMessage(PopupSystem.Popup.Death);
                else if (chance > 0.80) pop.PopMessage(PopupSystem.Popup.Bite);
            }

        } //le principe est la, il faut balance et ajouter les items à l'inventaire




        // updating game values
        gameController.UpdateGameClock(scavengeTime);

        //========== KEEP STATUS BAR BEFORE MODIFICATION ===========

        int oldHealth = player.currentHealth;
        int oldHunger = player.currentHunger;
        int oldThirst = player.currentThirst;
        int oldEnergy = player.currentEnergy;


        //========== MODIFY STATUS BAR =================

        player.UpdateEnergy(-2 * nbTimeSlice);
        player.UpdateHunger(-2 * nbTimeSlice);
        player.UpdateThirst(-3 * nbTimeSlice);

        //===============================================


        menuController.ExitMenu(this.gameObject);

        //=======    POP UP RESULT    =============
        popUpResult.PopResult(itemsFound, (health: (old: oldHealth, now: player.currentHealth), hunger: (old: oldHunger, now: player.currentHunger), thirst: (old: oldThirst, now: player.currentThirst), energy: (old: oldEnergy, now: player.currentEnergy)));


        itemsFound.Clear();

    }


}
