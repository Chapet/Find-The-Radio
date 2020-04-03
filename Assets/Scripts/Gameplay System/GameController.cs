using UnityEngine;
using System.Collections;
using System.IO;


public class GameController : MonoBehaviour
{

    //[Tooltip("Contain the object player")] 
    //public PlayerController player;
    
    [Space]
    [Range(0f, 24f)]
    public float gameClock = 8f;

    public static int framerate = 60;

    public GameObject BunkerPanel;
    public GameObject transitionPanel;
    public Animator transitionAnim;
    public GameData loaded;
    [SerializeField] private Introduction _introduction;
    public static bool NewGame { get; set; }

    public ClockController clock; 

    // Start is called before the first frame update
    void Start() {
        UpdateGameClock(gameClock);
        BunkerPanel.SetActive(true);
        MenuController.Transition(transitionPanel, transitionAnim);
        if (NewGame)
        {
            ResetGame();
            CleanSave();;
            PlayerController.IS_FIRST_GAME=true;
        }
        else
        {
            LoadGame();
        }

        if (PlayerController.IS_FIRST_GAME)
        {
            _introduction.StartIntroduction();
        }
    }

    private void CleanSave()
    {
        Debug.Log("NewGame");
        Save();
    }

    public static void ResetGame()
    {
        //DirectoryInfo save_dir = new DirectoryInfo(SaveSystem.path);
        //save_dir.Delete(true);
        if(File.Exists(SaveSystem.path))
        {
            File.Delete(SaveSystem.path);
            Debug.Log("Previous save deleted");
        }
        else
        {
            Debug.Log("No previous to delete");
        }
    }

    private void LoadGame()
    {
        GameData data = SaveSystem.Load();
        if (data != null)
        {
            PlayerController.IS_FIRST_GAME = data.is_firstGame;
            loaded = data;
            PlayerController.Player.currentStats[(int)StatType.Health] = data.health;
            PlayerController.Player.currentStats[(int)StatType.Hunger] = data.hunger;
            PlayerController.Player.currentStats[(int)StatType.Thirst] = data.thirst;
            PlayerController.Player.currentStats[(int)StatType.Energy] = data.energy;

            UpdateGameClock(data.gameClock);

            foreach (string s in data.equippedGear)
            {
                Gear g = Resources.Load("Items/Gear/" + s) as Gear;
                if (g != null)
                {
                    PlayerController.Player.EquipGear(g);
                }
            }

            foreach (string s in data.consumables)
            {
                Consumable c = Resources.Load("Items/Consumables/" + s) as Consumable;
                if (c != null)
                {
                    InventoryManager.Inventory.AddItem(c);
                }
            }
            foreach (string s in data.equipment)
            {
                Gear g = Resources.Load("Items/Gear/" + s) as Gear;
                if (g != null)
                {
                    InventoryManager.Inventory.AddItem(g);
                }
            }
            foreach (string s in data.junks)
            {
                Junk j = Resources.Load("Items/Junks/" + s) as Junk;
                if (j != null)
                {
                    InventoryManager.Inventory.AddItem(j);
                }
            }
            foreach (string s in data.resources)
            {
                Resource r = Resources.Load("Items/Resources/" + s) as Resource;
                if (r != null)
                {
                    InventoryManager.Inventory.AddItem(r);
                }
            }
            Debug.Log("Save loaded!");
            NewGame = false;
        }
    }

    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        double hours = Mathf.Floor(gameClock);
        double minutes = Mathf.Abs(Mathf.Ceil((gameClock - Mathf.Ceil(gameClock)) * 60));
        clock.UpdateClock((int) hours, (int) minutes);
        //clock.SetText("Clock : " + hours.ToString("0") + "h" + minutes.ToString("0"));
    }

    public void GoToMainMenu()
    {
        Debug.Log("Going to the main menu ...");
        Save();
        MenuController.Transition(transitionPanel, transitionAnim, "MainMenuScene");
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        GameData data = new GameData(PlayerController.Player, InventoryManager.Inventory, gameClock);
        SaveSystem.Save(data);
    }
}
