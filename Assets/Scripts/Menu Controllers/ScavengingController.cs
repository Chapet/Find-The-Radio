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
    public TMP_Text ETAText;
    public TMP_Text AddDayText;
    public TMP_Text SliderText;
    public GameObject Slider;
    private float scavengeTime;
    private PlayerController player;
    [SerializeField]private MenuController menuController;
    [SerializeField]public PopupSystem pop;
    [SerializeField]public ScavengeResultsSystem popUpResult;

    [SerializeField]private Button scavengeButton;
    [SerializeField]private Slider scavengeSlider;

    void Start()
    {
        player = PlayerController.Player;
    }

    void Update()
    {
        scavengeTime = (Slider.GetComponent<Slider>().value) / 2;
        String hours = Math.Truncate(scavengeTime).ToString("00");
        String minutes = Math.Truncate((scavengeTime - Math.Truncate(scavengeTime)) * 60).ToString("00");
        SliderText.SetText(hours + "h" + minutes);
        UpdateETA();
    }

    private void UpdateETA()
    {
        double eta = GameController.Controller.gameClock + scavengeTime;
        string etaHoursStr;
        string etaMinutesStr;
        if (eta >= 24)
        {
            eta = eta % 24;
            AddDayText.SetText("(+1 day)");
        }
        else
        {
            AddDayText.SetText("");
        }
        etaHoursStr = Math.Truncate(eta).ToString("00");
        etaMinutesStr = Math.Truncate((eta - Math.Truncate(eta)) * 60).ToString("00");
        ETAText.SetText(etaHoursStr + "h" + etaMinutesStr);
    }


    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }

    public void ScavengingBtnClicked()
    {
        menuController.ExitMenu(gameObject);
        BackgroundTasks.Tasks.StartNewScavenging(scavengeTime);
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
    }
   
}
