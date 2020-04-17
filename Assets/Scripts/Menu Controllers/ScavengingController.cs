using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Object = System.Object;
using Slider = UnityEngine.UI.Slider;

public class ScavengingController : MonoBehaviour
{
    
    public static ScavengingController Scavenging_Controller { get; private set; } 
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text scavengeText;
    public GameObject Slider;
    private float scavengeTime;
    private PlayerController player;
    [SerializeField]private MenuController menuController;
    [SerializeField]public PopupSystem pop;
    [SerializeField]public ScavengeResultsSystem popUpResult;

    [SerializeField]private Button scavengeButton;
    [SerializeField]private Slider scavengeSlider;


    /*[SerializeField] [Tooltip("Contain all the items that you can find outside the bunker")]
    private ScavengingListItem itemList;
    */
    void Start()
    {
        player = PlayerController.Player;

    }

    /*void OnEnable()
    {
        
        //    scavengeButton.onClick.RemoveAllListeners();
         //   scavengeSlider.interactable = false;
          //  scavengeButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Is still scavenging");
            scavengeButton.onClick.RemoveAllListeners();
            scavengeButton.onClick.AddListener(ScavengingBtnClicked);
            scavengeSlider.interactable = true;
            scavengeButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Scavenge");
    }*/


    void Update()
    {
        scavengeTime = (Slider.GetComponent<Slider>().value) / 2;
        String hours = Math.Truncate(scavengeTime).ToString("00");
        String minutes = Math.Truncate((scavengeTime - Math.Truncate(scavengeTime)) * 60).ToString("00");
        scavengeText.SetText("Scavenge for : " + hours + "h" + minutes);
    }
    

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }

    public void ScavengingBtnClicked()
    {
        //Scavenge();
        BackgroundTasks.Tasks.StartNewScavenging(scavengeTime);
        menuController.ExitMenu(gameObject);

    }


    public void StartPupUpResult(Scavenging scavenging)
    {
        menuController.OpenMenu(popUpResult.gameObject);
        popUpResult.PopResult(scavenging);
    }


    private int nbrScavengeStep;
    private int oldHealth;
    private int oldHunger;
    private int oldThirst;
    private int oldEnergy;
    private List<string> scavengeLog;
    
    
    void StartNewScavenging()
    {
        scavengeLog = new List<string>();
        
        oldHealth = player.currentStats[(int)StatType.Health];
        oldHunger = player.currentStats[(int)StatType.Hunger];
        oldThirst = player.currentStats[(int)StatType.Thirst];
        oldEnergy = player.currentStats[(int)StatType.Energy];
        
    }
    
   

    void Scavenge()
    {
        BackgroundTasks.Tasks.StartNewScavenging(scavengeTime);
        menuController.ExitMenu(gameObject);
        //Debug.Log(scavengeTime);

        //nombre de tranche de 30 min

        return;

        //========== KEEP STATUS BAR BEFORE MODIFICATION ===========

        int oldHealth = player.currentStats[(int)StatType.Health];
        int oldHunger = player.currentStats[(int)StatType.Hunger];
        int oldThirst = player.currentStats[(int)StatType.Thirst];
        int oldEnergy = player.currentStats[(int)StatType.Energy];
        


        menuController.ExitMenu(gameObject);

        
        
        /*  /!\ MAX 29 caractères  /!\   */

        /*
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
        
        itemsFound.Clear();*/

    }

   
}
