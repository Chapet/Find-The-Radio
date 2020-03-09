using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScavengingController : MonoBehaviour
{
    public GameController gameController;
    public GameObject BunkerPanel;
    public InventoryManager inventory;
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text scavengeText;
    public GameObject Slider;
    private float scavengeTime;
    [Tooltip("Contain the object player")]
    public PlayerController player;
    public MenuController menuController;
    public PopupSystem pop;

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
        double chance = 0;
        bool ev = false;

        for (int i = 0; i < nbTimeSlice && nbFound < player.maxCarryingSize; i++)
        {
            chance  = rand.NextDouble(); // 0.0 <= chance < 1.0
            if (chance > 0.66)
            {
                nbFound++;

                //chances for the found item, has to be balance when all items in the game
                chance = rand.NextDouble();

                if (chance > 0.9) //gun
                {
                    Debug.Log("pistol added to inventory");
                }
                else if (chance > 0.45) // water
                {
                    Debug.Log("water added to inventory");
                }
                else //food
                {
                    Debug.Log("food can added to inventory");
                }

                Debug.Log("adding Item: " + i);
            }

            if (!ev)
            {
                chance = rand.NextDouble();
                if (chance > 0.95) pop.PopMessage(PopupSystem.Popup.Death);
                else if (chance > 0.80) pop.PopMessage(PopupSystem.Popup.Bite);
            }
           
        } //le principe est la, il faut balance et ajouter les items à l'inventaire

        // updating game values
        gameController.UpdateGameClock(scavengeTime);
        player.UpdateEnergy(-2 * nbTimeSlice);

        player.UpdateHunger(-2 * nbTimeSlice);
        player.UpdateThirst(-3 * nbTimeSlice);
    }
}
