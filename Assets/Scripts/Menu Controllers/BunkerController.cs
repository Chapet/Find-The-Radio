using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkerController : MonoBehaviour
{
    public static BunkerController Bunker_Controller { get; private set; }
    public GameObject bedPanel;
    public GameObject scavengingPanel;
    private GameObject bunkerPanel;
    public GameObject inventoryPanel;
    public GameObject cheatPanel;
    public GameObject craftingPanel;
    public GameObject scavengingPopUpResultPanel;

    public SnackbarController snackbar;

    public GameObject healthBar;
    public GameObject hungerBar;
    public GameObject thirstBar;
    public GameObject energyBar;

    public TabController tabController;

    public GameObject backPanel;

    public MenuController menuController;
    // Start is called before the first frame update
    void Awake()
    {
        bunkerPanel = gameObject;
        bunkerPanel.SetActive(true);
        bedPanel.SetActive(false);
        scavengingPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        cheatPanel.SetActive(false);
        craftingPanel.SetActive(false);
        backPanel.SetActive(false);
        scavengingPopUpResultPanel.SetActive(false);
    }

    public void SleepButtonClicked()
    {
        if (BackgroundTasks.Tasks.IsScavenging)
        {
            snackbar.ShowSnackBar("You can't sleep because you are scavenging !");
        }
        else
        {
            Debug.Log("Going to bed ... zZzZ ");
            //throw new System.Exception("test exception CLickOnBed ");
            menuController.OpenMenu(bedPanel);
        }
    }

    public void ScavengeButtonClicked()
    {
        if (BackgroundTasks.Tasks.IsSleeping)
        {
            snackbar.ShowSnackBar("You can't scavenge because you are sleeping !");
        }
        else
        {
            Debug.Log("Scavenge: depart into the unknown ...");
            if (BackgroundTasks.Tasks.IsScavenging || BackgroundTasks.Tasks.lastScavenging != null)
            {
                menuController.OpenMenu(scavengingPopUpResultPanel);
            }
            else
            {
                menuController.OpenMenu(scavengingPanel);
            }
        }
    }

    public void CraftingBtnClicked()
    {
        if (BackgroundTasks.Tasks.IsScavenging || BackgroundTasks.Tasks.IsSleeping)
        {
            if (BackgroundTasks.Tasks.IsScavenging) snackbar.ShowSnackBar("You can't craft because you are scavenging !");
            if (BackgroundTasks.Tasks.IsSleeping) snackbar.ShowSnackBar("You can't craft because you are sleeping !");
        }
        else
        {
            Debug.Log("Let's go crafting ...");
            craftingPanel.GetComponent<CraftingController>().UpdateCraftable();
            menuController.OpenMenu(craftingPanel);
        }
    }

    public void GoToBunker()
    {
        if(bedPanel.activeSelf)
        {
            menuController.ExitMenu(bedPanel);
        }
        if(scavengingPanel.activeSelf)
        {
            menuController.ExitMenu(scavengingPanel);
        }
        if(scavengingPopUpResultPanel.activeSelf)
        {
            menuController.ExitMenu(scavengingPopUpResultPanel);
        }
        if (inventoryPanel.activeSelf)
        {
            tabController.CloseTabs();
            menuController.ExitMenu(inventoryPanel);
        }
        if(cheatPanel.activeSelf)
        {
            menuController.ExitMenu(cheatPanel);
        }
        if (craftingPanel.activeSelf)
        {
            menuController.ExitMenu(craftingPanel);
        }
    }

    public void CheatBtnClicked()
    {
        menuController.OpenMenu(cheatPanel);
    }


    public void InventoryButtonClicked()
    {
        if (BackgroundTasks.Tasks.IsScavenging || BackgroundTasks.Tasks.IsSleeping)
        {
            if (BackgroundTasks.Tasks.IsScavenging) snackbar.ShowSnackBar("You can't open your inventory because you are scavenging !");
            if (BackgroundTasks.Tasks.IsSleeping) snackbar.ShowSnackBar("You can't open your inventory because you are sleeping !");
        }
        else
        {
            menuController.OpenMenu(inventoryPanel);
        }
    }
}
