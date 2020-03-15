using System.Collections.Generic;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    [SerializeField] private List<StatusBar> healthBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> hungerBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> thirstBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> energyBars = new List<StatusBar>();
    private PlayerController player;

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    public void UpdateStatusBars()
    {
        foreach(StatusBar s in healthBars)
        {
            s.SetMaxValue(PlayerController.maxHealth);
            s.SetValue(player.currentHealth);
        }
        foreach (StatusBar s in hungerBars)
        {
            s.SetMaxValue(PlayerController.maxHunger);
            s.SetValue(player.currentHunger);
        }
        foreach (StatusBar s in thirstBars)
        {
            s.SetMaxValue(PlayerController.maxThirst);
            s.SetValue(player.currentThirst);
        }
        foreach (StatusBar s in energyBars)
        {
            s.SetMaxValue(PlayerController.maxEnergy);
            s.SetValue(player.currentEnergy);
        }
    }


}
