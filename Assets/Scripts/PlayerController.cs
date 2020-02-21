using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Inventory", menuName = "MyAsset/Player")]

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxHunger = 100;
    public int maxThirst = 100;
    public int maxEnergy = 100;

    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentEnergy;

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar tirstBar;
    public StatusBar energyBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);

        currentHunger = maxHunger;
        hungerBar.SetMaxValue(maxHunger);
        hungerBar.SetValue(maxHunger);

        currentThirst = maxThirst;
        tirstBar.SetMaxValue(maxThirst);
        tirstBar.SetValue(maxThirst);

        currentEnergy = maxEnergy;
        energyBar.SetMaxValue(maxEnergy);
        energyBar.SetValue(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnergy(float inc)
    {
        // 1 energy point per 24 mintues <=> 2.5 energy points per hour <=> 100 energy point per 40 hours
        int value;
        if (inc < 0)
        {
            value = Mathf.CeilToInt((Mathf.Ceil(inc) + Mathf.Ceil((inc - Mathf.Ceil(inc)))) * 60 / 24);
        }
        else
        {
            value = Mathf.FloorToInt((Mathf.Floor(inc) + Mathf.Floor((inc - Mathf.Floor(inc)))) * 60 / 24);
        }
        Debug.Log("Adding " + value + " to the energy.");
        energyBar.addValue(value);
        currentEnergy = (int)(energyBar.slider.value);
    }
}
