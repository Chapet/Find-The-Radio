using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//[CreateAssetMenu(fileName = "New inventory", menuName = "MyAsset/Player")]
public enum StatType
{
    None = 0, Health, Hunger, Thirst, Energy
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player
    {
        get; private set;
    }

    public static int maxHealth = 100;
    public static int maxHunger = 100;
    public static int maxThirst = 100;
    public static int maxEnergy = 100;

    public static int[] maxStats = new int[] { -1, 100, 100, 100, 100 };
    public int[] currentStats;

    public int maxCarryingSize = 4;

    public Sprite[] clock_num = new Sprite[10];

    public StatusBarController statusBarController;

    [SerializeField] private Gear[] equipment;
    public static bool IS_FIRST_GAME = true;
    public Gear[] Equipment {get {return equipment; }}

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("1");
        Player = this;
        currentStats = new int[maxStats.Length];
        Array.Copy(maxStats, 0, currentStats, 0, maxStats.Length);
        equipment = new Gear[5];
    }

    private void Start()
    {
        statusBarController = StatusBarController.SBController;
    }

    public bool IsEquipped(Gear g)
    {
        foreach(Gear e in equipment)
        {
            if(g.Equals(e))
            {
                return true;
            }
        }
        return false;
    }

    public Gear GetGear(Gear.ItemType type)
    {
        return equipment[(int)type];
    }

    public void EquipGear(Gear g)
    {
        if (!IsEquipped(g))
        {
            equipment[(int)g.Type] = g;
        }
        else
        {
            Debug.Log("This gear was already equipped!");
        }
    }

    public void UnequipGear(Gear g)
    {
        if (IsEquipped(g))
        {
            equipment[(int)g.Type] = null;
        }
        else
        {
            Debug.Log("This gear wasn't equipped!");
        }
    }

    public void UpdateStat(int inc, StatType type)
    {
        int value;
        if (inc > 0)
        {
            value = Mathf.Min(maxHunger, currentStats[(int) type] + inc);
        }
        else
        {
            value = Mathf.Max(0, currentStats[(int)type] + inc);
        }

        currentStats[(int) type] = value;
        statusBarController.UpdateStatusBars(type);
    }

    /**
     * add inc to the health value
     */
    public void UpdateHealth(int inc)
    {
        UpdateStat(inc, StatType.Health);
    }

    /**
     * add inc to the hunger value
     */
    public void UpdateHunger(int inc)
    {
        UpdateStat(inc, StatType.Hunger);
    }
    
    /**
     * add inc to the thirst value
     */
    public void UpdateThirst(int inc)
    {
        UpdateStat(inc, StatType.Thirst);
    }
    
    
    public void UpdateEnergy(int inc)
    {
        UpdateStat(inc, StatType.Energy);
    }

    public void Death()
    {
        if (currentStats[(int) StatType.Health] < 1)
        {
            GameController.ResetGame();
            SceneManager.LoadScene(0);
        }
    }
}
