using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New inventory", menuName = "MyAsset/Player")]

public class PlayerController : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int maxHunger = 100;
    public static int maxThirst = 100;
    public static int maxEnergy = 100;

    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentEnergy;

    public int maxCarryingSize = 4;

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

    /**
     * add inc to the hunger value
     */
    public void UpdateHunger(int inc)
    {
        int value;
        //define new value
        if (inc < 0)
        {
            value = Mathf.Min(maxHunger, currentHunger + inc);
        }
        else
        {
            value = Mathf.Max(0, currentHunger + inc);
        }

        currentHunger = value;
        hungerBar.SetValue(value);

        Debug.Log("Adding " + inc + " to the Hunger.");
    }
    
    /**
     * add inc to the thirst value
     */
    public void UpdateThirst(int inc)
    {
        int value;
        if (inc < 0)
        {
            value = Mathf.Min(maxThirst, currentThirst + inc);
        }
        else
        {
            value = Mathf.Max(0, currentThirst + inc);
        }

        currentThirst = value;
        tirstBar.SetValue(value);

        Debug.Log("Adding " + inc + " to the Thirst.");
        
    }
    
    /**
     * add inc to the health value
     */
    public void UpdateHealth(int inc)
    {
        int value;
        if (inc < 0)
        {
            value = Mathf.Min(maxHealth, currentHealth + inc);
        }
        else
        {
            value = Mathf.Max(0, currentHealth + inc);
        }

        currentHealth = value;
        healthBar.SetValue(value);
        Debug.Log("Adding " + inc + " to the Health.");
        
    }
    public void UpdateEnergy(int inc)
    {
        int value;
        if (inc < 0)
        {
            value = Mathf.Min(maxEnergy, currentEnergy + inc);
        }
        else
        {
            value = Mathf.Max(0, currentEnergy + inc);
        }

        currentEnergy = value;
        energyBar.SetValue(value);
        Debug.Log("Adding " + inc + " to the Energy.");
    }
}
