using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SBType
{
    none = 0, health, hunger, thirst, energy
}

public class StatusBarController : MonoBehaviour
{
    //private static StatusBarController statusBarController;
    public static StatusBarController SBController
    {
        get; private set;
    }
    [SerializeField] private List<StatusBar> healthBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> hungerBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> thirstBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> energyBars = new List<StatusBar>();
    private PlayerController player = PlayerController.Player;

    private void Awake()
    {
        //player = gameObject.GetComponent<PlayerController>();
        player = PlayerController.Player;
        SBController = this;
    }

    public void UpdateStatusBars()
    {
        List<StatusBar> bars = healthBars.Concat(hungerBars).Concat(thirstBars).Concat(energyBars).ToList();
        foreach (StatusBar s in bars)
        {
            if (s.HasBeenRendered())
            {
                UpdateSB(s);
            }
        }
        
        Debug.Log("Updating status bars ...");
    }

    public void AddStatusBar(StatusBar s)
    {
        switch (s.Type) {
            case SBType.health:
                healthBars.Add(s);               
                break;
            case SBType.hunger:
                hungerBars.Add(s);
                break;
            case SBType.thirst:
                thirstBars.Add(s);
                break;
            case SBType.energy:
                energyBars.Add(s);
                break;
            default:
                Debug.Log("Status bar not recognized!");
                break;
        }
        UpdateSB(s);
    }

    private void UpdateSB(StatusBar s)
    {
        switch (s.Type)
        {
            case SBType.health:
                s.SetMaxValue(PlayerController.maxHealth);
                s.SetValue(player.currentHealth);
                break;
            case SBType.hunger:
                s.SetMaxValue(PlayerController.maxHunger);
                s.SetValue(player.currentHunger);
                break;
            case SBType.thirst:
                s.SetMaxValue(PlayerController.maxThirst);
                s.SetValue(player.currentThirst);
                break;
            case SBType.energy:
                s.SetMaxValue(PlayerController.maxEnergy);
                s.SetValue(player.currentEnergy);
                break;
            default:
                Debug.Log("Status bar not recognized!");
                break;
        }
    }
}
