using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class GameController : MonoBehaviour
{

    [Header("Scriptable objects")] 
    [Tooltip("Contain the object player")] 
    public Player player;
    
    [Space]
    [Range(0f, 24f)]
    public float gameClock = 12f;

    public static int maxHealth = 100;
    

    public static int maxHunger = 100;
    

    public static int maxThirst = 100;
   

    public static int maxEnergy = 100;
   

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar thirstyBar;
    public StatusBar energyBar;

    public TMP_Text clock;
    public GameObject BunkerPanel;

    // Start is called before the first frame update
    void Start() {


        BunkerPanel.SetActive(true);
        player.currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);

        player.currentHunger = maxHunger;
        hungerBar.SetMaxValue(maxHunger);
        hungerBar.SetValue(maxHunger);

        player.currentThirst = maxThirst;
        thirstyBar.SetMaxValue(maxThirst);
        thirstyBar.SetValue(maxThirst);

        player.currentEnergy = maxEnergy;
        energyBar.SetMaxValue(maxEnergy);
        energyBar.SetValue(maxEnergy);
    }

    // Update is called once per frame
    void Update() {
    }

    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        String hours = Math.Truncate(gameClock).ToString("0");
        String minutes = Math.Truncate((gameClock - Math.Truncate(gameClock)) * 60).ToString("0");
        clock.SetText("Clock : " + hours + "h" + minutes);
    }

    public void addHungerValue(int value)
    {
        //getnewValue
        
        
        //MAJ Player
        
        //MAJ hungerBar
        hungerBar.addValue(value);
    }
    
   
}
