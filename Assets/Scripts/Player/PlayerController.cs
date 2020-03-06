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
    public StatusBar thirstBar;
    public StatusBar energyBar;

    [SerializeField] private Gear headGear = null;
    [SerializeField] private Gear chestGear = null;
    [SerializeField] private Gear legsGear = null;
    [SerializeField] private Gear weapon = null;

    public void PutHeadGearOn(Gear g)
    {
        if (g.IsOfType(Gear.ItemType.Head))
        {
            headGear = g;
        }
        else
        {
            Debug.Log("Not for the head!");
        }
    }

    public void RemoveHeadGear()
    {
        headGear = null;
    }

    public void PutChestGearOn(Gear g)
    {
        if (g.IsOfType(Gear.ItemType.Chest))
        {
            chestGear = g;
        }
        else
        {
            Debug.Log("Not for the chest!");
        }
    }

    public void RemoveChestGear()
    {
        chestGear = null;
    }

    public void PutLegsGearOn(Gear g)
    {
        if (g.IsOfType(Gear.ItemType.Legs))
        {
            legsGear = g;
        }
        else
        {
            Debug.Log("Not for the legs!");
        }
    }

    public void RemoveLegsGear()
    {
        legsGear = null;
    }

    public void EquipWeapon(Gear g)
    {
        if (g.IsOfType(Gear.ItemType.Weapon))
        {
            weapon = g;
        }
        else
        {
            Debug.Log("This is not a weapon!");
        }
    }

    public void UnequipWeapon()
    {
        weapon = null;
    }

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
        thirstBar.SetMaxValue(maxThirst);
        thirstBar.SetValue(maxThirst);

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
        thirstBar.SetValue(value);

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
