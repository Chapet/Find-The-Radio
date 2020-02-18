using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [Range(0f, 24f)]
    public float gameClock = 12f;

    public int maxHealth = 100;
    public int currentHealth;

    public int maxHunger = 100;
    public int currentHunger;

    public int maxThirst = 100;
    public int currentThirst;

    public int maxEnergy = 100;
    public int currentEnergy;

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar thirstyBar;
    public StatusBar energyBar;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);

        currentHunger = maxHunger;
        hungerBar.SetMaxValue(maxHunger);
        hungerBar.SetValue(maxHunger);

        currentThirst = maxThirst;
        thirstyBar.SetMaxValue(maxThirst);
        thirstyBar.SetValue(maxThirst);

        currentEnergy = maxEnergy;
        energyBar.SetMaxValue(maxEnergy);
        energyBar.SetValue(maxEnergy);
    }

    // Update is called once per frame
    void Update() {
    }

    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        // Update UI
    }
}
