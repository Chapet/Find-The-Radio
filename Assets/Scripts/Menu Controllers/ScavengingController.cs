using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Object = System.Object;
using Slider = UnityEngine.UI.Slider;

public class ScavengingController : MonoBehaviour
{
    [SerializeField]private GameController gameController;
    [SerializeField]private GameObject BunkerPanel;
    [SerializeField]private InventoryManager inventory;
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text scavengeText;
    public GameObject Slider;
    private float scavengeTime;
    [Tooltip("Contain the object player")]
    [SerializeField]private PlayerController player;
    [SerializeField]private MenuController menuController;
    [SerializeField]private PopupSystem pop;
    [SerializeField]private ScavengeResultsSystem popUpResult;

    /*[SerializeField] [Tooltip("Contain all the items that you can find outside the bunker")]
    private ScavengingListItem itemList;
    */
    void Start()
    {
        itemsFound = new List<Item>();
        player = PlayerController.Player;
        inventory = InventoryManager.Inventory;
    }

    private void Awake()
    {
        BuildScavengeItems(scavengeItemsData);
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
        //menuController.ExitMenu(gameObject);
    }

    
    /**
     * Check if we are going to use the gun (depend of probability) and modify inventory of gun if it's use
     * @Pre: probabilité have to be between 0 and 1
     *
     * if we don't have a gun, return false
     */
    private bool MayUseGun(double probability)
    {
        if (probability > 1 || probability < 0)
        {
            Debug.Log("probability of gun usage have to be between 0 and 1");
            return false;
        }

        Gear gun;
        if ((gun=player.GetGear(Gear.ItemType.Weapon))==null)
        {
            //I'm not equip with a weapon
            return false;
        }

        double chance=new System.Random().NextDouble();
        
        if (chance <= probability)
        {
            //SHOT with the gun
            /*====    DOMAGE    ====*/
            gun.liveGear -= 10;
            
            if (gun.liveGear <= 0)
            {
                inventory.RemoveItem(gun);
                Debug.Log(gun.name+" is broken");
                return false;
            }

            return true;
        }

        return false;
    }

    private bool MyUseArmor(double probability)
    {
        Gear armorPiece;
        double proba=new System.Random().NextDouble();
        
        if (inventory.Equipment.Count == 0||proba>probability)
        {
            return false;
        }
        
        if ((armorPiece = player.GetGear(Gear.ItemType.Chestplate)) != null) ;
        else if ((armorPiece = player.GetGear(Gear.ItemType.Greaves)) != null) ;
        else if ((armorPiece = player.GetGear(Gear.ItemType.Helmet)) != null) ;
        
        
        if (armorPiece != null)
        {
            /*==  DOMAGE  ==*/
            armorPiece.liveGear -= 10;
            
            if (armorPiece.liveGear <= 0)
            {
                inventory.RemoveItem(armorPiece);
                Debug.Log(armorPiece.name+" is broken");
                return false;
            }
            
            return true;
        }
        return false;
    }


    /*========================= ALL ITEM SCAVENGE =================================*/
    private (string link, int minLevel, Item.ItemClass itemType, double minTimeOut)[] scavengeItems;
    /**
     * link: lien vers l'item
     * MinLevel: le libeau minimum de l'utilisateur pour avoir cet iem
     * itypeType: le type de ressources
     * scavengeTime: le temps minimum qu'il faut sortir pour trouuver cet item (en nbr d'heure)
     */
    private (string link, int minLevel, Item.ItemClass itemType, double minTimeOut,int weight)[] scavengeItemsData = new[]
    {
        /*======CONSUMABLE=====*/
        (link: "Items/Consumables/WaterBottle", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:1),

        (link: "Items/Consumables/FoodCan", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:2),

        (link: "Items/Consumables/Soda", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:2),
        
        (link: "Items/Consumables/Medkit", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 0.0,weight:2),

        (link: "Items/Consumables/MilitaryRation", minLevel: 0, itemType: Item.ItemClass.Consumable, minTimeOut: 3.0,weight:1),


        /*======GEAR=====*/
        (link: "Items/Gear/Gun", minLevel: 0, itemType: Item.ItemClass.Gear, minTimeOut: 2.0,weight:1),

        /*======RESOURCES=====*/
        (link: "Items/Resources/Wood", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),
        (link: "Items/Resources/Metalscrap", minLevel: 0, itemType: Item.ItemClass.Resource, minTimeOut: 0.0,weight:1),

        /*======Junk=====*/
        (link: "Items/Junks/Grass", minLevel: 0, itemType: Item.ItemClass.Junk, minTimeOut: 0.0,weight:1),

    };

    /**
     * build array that contain all items that can be scavenging with the weight attach to each item
     */
    private void BuildScavengeItems(
        (string link, int minLevel, Item.ItemClass itemType, double minTimeOut, int weight)[] input)
    {
        List<(string link, int minLevel, Item.ItemClass itemType, double minTimeOut)> itemList=new List<(string link, int minLevel, Item.ItemClass itemType, double minTimeOut)>();
        foreach (var item in input)
        {
            for (int i = 0; i < item.weight; i++)
            {
                itemList.Add((link: item.link, minLevel: item.minLevel, itemType: item.itemType, minTimeOut: item.minTimeOut));
            }
        }

        scavengeItems = itemList.ToArray();
    }

    /*============================================================================================== */
    

    /**
     * get all item that have a minLevl less of equal to mylevel, a type of type (consummable, ressources, junk and gear)
     * and that we can find with a our scavengeTime
     *
     * Return array of [<link> as string, <luck> as double, <minlevel> as int, <itemtype> as ItemType, <minTimeOut> as double]
     */
    private static (string link,int minLevel,Item.ItemClass itemType,double minTimeOut)[] getMyLevelItems((string link,int minLevel,Item.ItemClass itemType,double minTimeOut)[] tuple,int myLevel,Item.ItemClass type, double scavengeTime)
    {
        List <(string link,int minLevel,Item.ItemClass itemType,double minTimeOut)> result= new List<(string link,int minLevel,Item.ItemClass itemType,double minTimeOut)>();

        for (int i = 0; i < tuple.Length; i++)
        {
            if (tuple[i].minLevel <= myLevel && tuple[i].itemType==type &&tuple[i].minTimeOut<=(double)scavengeTime)
            {
                result.Add(tuple[i]);
            }
        }
        return result.ToArray();
    }

    /*=============================================================================*/

    private List<Item> itemsFound;
    
    /**
     * Rajoute les items trouvé dans l'inventaire et dans la liste des items trouvés qui sera ensuite transmis
     * à la popUp pour visualisé les résultats
     */
    private void addItemFound(Item item,Item.ItemClass itemClass)
    {
        itemsFound.Add(item);
        if (itemClass == Item.ItemClass.Consumable)
        {
            inventory.AddItem(Instantiate(item as Consumable));
        }
        else if (itemClass == Item.ItemClass.Gear)
        {
            inventory.AddItem(Instantiate(item as Gear));            
        }
        else if (itemClass == Item.ItemClass.Junk)
        {
            inventory.AddItem(Instantiate(item as Junk));
        }
        else if (itemClass == Item.ItemClass.Resource)
        {
            inventory.AddItem(Instantiate(item as Resource));
        }
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
        bool hadSenario = false;

        //=====    /!\    MAX 29 CARACTERES    /!\    ================
        List<string> scavengeLog = new List<string>();//C'est là dedans que l'on doit mettre les log (ex: tu as été mordu par un zombie) 


        //========== KEEP STATUS BAR BEFORE MODIFICATION ===========

        int oldHealth = player.currentStats[(int)StatType.Health];
        int oldHunger = player.currentStats[(int)StatType.Hunger];
        int oldThirst = player.currentStats[(int)StatType.Thirst];
        int oldEnergy = player.currentStats[(int)StatType.Energy];


        for (int i = 0; i < nbTimeSlice && nbFound < player.maxCarryingSize; i++)
        {
            chance  = rand.NextDouble(); // 0.0 <= chance < 1.0
            if (chance > 0.66)/*========== FIND A ITEM  ===========*/
            {
                nbFound++;

                //chances for the found item, has to be balance when all items in the game
                chance = rand.NextDouble();
                
                int myLevel = 100;//TODO: link with player
                
                (string link,int minLevel,Item.ItemClass itemType,double minTimeOut)[] possibleItem;

                if (chance > 0.9)/*========== GEAR ===========*/
                {
                    //retourn tt les items que je pourrais possiblement trouver sur mon chemin en fonction de mon level, du type d'objet et du temps que j'ai décidé de sortir
                    possibleItem= getMyLevelItems(scavengeItems, myLevel,Item.ItemClass.Gear,scavengeTime);
                    
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
                    possibleItem= getMyLevelItems(scavengeItems, myLevel,Item.ItemClass.Consumable,scavengeTime);
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
                    possibleItem= getMyLevelItems(scavengeItems, myLevel,Item.ItemClass.Resource,scavengeTime);
                    if (possibleItem != null&& possibleItem.Length>0)

                    {
                        int index = (int) (rand.NextDouble() * (possibleItem.Length - 1));
                        Resource element = Resources.Load<Resource>(possibleItem[index].link);
                        //Debug.Log("Find" + element.name);
                        addItemFound(element,Item.ItemClass.Resource);
                    }
                }else if (chance > 0.0) /*========== JUNK ===========*/
                {
                    possibleItem= getMyLevelItems(scavengeItems, myLevel,Item.ItemClass.Junk,scavengeTime);
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
                if (chance > 0.999) pop.PopMessage(PopupSystem.Popup.Death);
                else if (chance > 0.80)
                {
                    //=========    BITTEN    =========
                    if (MayUseGun(0.8))
                    {
                        Debug.Log("Weapon has been used");
                    }
                    else if (MyUseArmor(0.8))
                    {
                        Debug.Log("Armor has been used");
                    }
                    else
                    {
                        scavengeLog.Add("You were bitten by a zombie (-10)"); //ADD log/message
                        player.UpdateHealth(-10); //update heamth    
                    }
                    
                }
            }
               
        } //le principe est la, il faut balance et ajouter les items à l'inventaire
        
        
        
        // updating game values
        gameController.UpdateGameClock(scavengeTime);


        //========== MODIFY STATUS BAR =================
        player.UpdateEnergy(-1 * nbTimeSlice);
        player.UpdateHunger(-2 * nbTimeSlice);
        player.UpdateThirst(-2 * nbTimeSlice);
        
        //===============================================


        menuController.ExitMenu(gameObject);

        
        
        /*  /!\ MAX 29 caractères  /!\   */

        if (player.currentStats[(int)StatType.Health] <= 0)
        {
            //======    DEAD    ==========
            pop.PopMessage(PopupSystem.Popup.Death);
        }
        else
        {
            //=======    POP UP RESULT    =============
            popUpResult.PopResult(itemsFound, (
                health: (old: oldHealth, now: player.currentStats[(int)StatType.Health]),
                hunger: (old: oldHunger, now: player.currentStats[(int)StatType.Hunger]),
                thirst: (old: oldThirst, now: player.currentStats[(int)StatType.Thirst]),
                energy: (old: oldEnergy, now: player.currentStats[(int)StatType.Energy])),
                scavengeLog.ToArray()
                );
            
        }
        
        itemsFound.Clear();

    }

   
}
