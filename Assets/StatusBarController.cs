using System.Collections.Generic;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    [SerializeField] private List<StatusBar> healthBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> hungerBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> thirstBars = new List<StatusBar>();
    [SerializeField] private List<StatusBar> energyBars = new List<StatusBar>();
    [SerializeField] private PlayerController player;

    public void UpdateStatusBars()
    {
        foreach(StatusBar s in healthBars)
        {
            s.SetValue(player.currentHealth);
        }
        foreach (StatusBar s in hungerBars)
        {
            s.SetValue(player.currentHunger);
        }
        foreach (StatusBar s in thirstBars)
        {
            s.SetValue(player.currentThirst);
        }
        foreach (StatusBar s in energyBars)
        {
            s.SetValue(player.currentEnergy);
        }
    }


}
