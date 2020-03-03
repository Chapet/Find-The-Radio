using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkerController : MonoBehaviour
{
    public GameObject bedPanel;
    public GameObject scavengingPanel;
    private GameObject bunkerPanel;
    public GameObject inventoryPanel;
    public GameObject cheatPanel;
    public GameObject craftingPanel;

    public TabManager tabController;

    public GameObject backPanel;

    public MenuController menuController;
    // Start is called before the first frame update
    void Start()
    {
        bunkerPanel = gameObject;
        bunkerPanel.SetActive(true);
        bedPanel.SetActive(false);
        scavengingPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        cheatPanel.SetActive(false);
        craftingPanel.SetActive(false);
        backPanel.SetActive(false);
    }

    public void SleepButtonClicked()
    {
        Debug.Log("Going to bed ... zZzZ ");
        throw new System.Exception("test exception CLickOnBed ");
        menuController.OpenMenu(bedPanel);
    }

    public void ScavengeButtonClicked()
    {
        Debug.Log("Scavenge: depart into the unknown ...");
        menuController.OpenMenu(scavengingPanel);
    }

    public void CraftingBtnClicked()
    {
        Debug.Log("Let's go crafting ...");
        menuController.OpenMenu(craftingPanel);
    }

    public void GoToBunker()
    {
        //bunkerPanel.SetActive(true);
        if(bedPanel.activeSelf)
        {
            menuController.ExitMenu(bedPanel);
        }
        if(scavengingPanel.activeSelf)
        {
            menuController.ExitMenu(scavengingPanel);
        }
        if(inventoryPanel.activeSelf)
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
        menuController.OpenMenu(inventoryPanel);
    }
}
