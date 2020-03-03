using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScavengingController : MonoBehaviour
{
    public GameController gameController;
    public GameObject BunkerPanel;
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text scavengeText;
    public GameObject Slider;
    private float scavengeTime;
    [Tooltip("Contain the object player")]
    public PlayerController player;

    public MenuController menuController;

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

    void Scavenge()
    {
        Debug.Log(scavengeTime);

        //nombre de tranche de 30 min
        int nbTimeSlice = (int)scavengeTime * 2;

        // ajout aléatoire d'items, le compte total ne dépassant pas le maximum transportable
        var rand = new System.Random();
        int nbFound = 0;
        for (int i = 0; i < nbTimeSlice && nbFound < player.maxCarryingSize; i++)
        {
            double chance  = rand.NextDouble();
            if (chance > 0.5)
            {
                nbFound++;
                Debug.Log("adding Item: " + i);
            }
        } //le principe est la, il faut balance et ajouter les items à l'inventaire

        // updating game values
        gameController.UpdateGameClock(scavengeTime);
        player.UpdateEnergy(-2 * nbTimeSlice);

        player.UpdateHunger(-2 * nbTimeSlice);
        player.UpdateThirst(-3 * nbTimeSlice);
    }
}
