using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryTutorial : TutorialStep
{
    
    public GameObject food;
    public GameObject med;
    public GameObject gear;
    public GameObject trash;
    public InventoryController InventoryController;

    private void Start()
    {
        food.SetActive(false);
        med.SetActive(false);
        gear.SetActive(false);
        trash.SetActive(false);
    }

    public override void StartTutorial()
    {
        base.StartTutorial();
        
        food.SetActive(false);
        med.SetActive(false);
        gear.SetActive(false);
        trash.SetActive(false);
        
        

        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().InventoryButtonClicked();
        
        //first step
        currentStep = -1;
        NextStep();
        
    }


    public override void NextStep()
    {
        currentStep++;
        switch (currentStep)
        {
            case 0:
                this.SetText("The inventory allows you to view, use or discard the items you own.");
                break;
            case 1:
                this.SetText("It is divided into several parts");
                break;
            case 2:
                //tab food&gear
                InventoryController.tabController.TabSwitcher(TabController.Tab.FoodAndDrink);
                food.SetActive(true);
                this.SetText("The food and drink tab lists the items that affect your hunger and thirst.");
                break;
            case 3:
                InventoryController.tabController.TabSwitcher(TabController.Tab.Meds);
                food.SetActive(false);
                med.SetActive(true);
                this.SetText("The med tab takes the items that are used to heal yourself");
                break;
            case 4:
                InventoryController.tabController.TabSwitcher(TabController.Tab.Gear);
                med.SetActive(false);
                gear.SetActive(true);
                this.SetText("The gear tab lists the items you can wear that will help you when you scavenge.");
                break;
            case 5:
                InventoryController.tabController.TabSwitcher(TabController.Tab.Misc);
                gear.SetActive(false);
                trash.SetActive(true);
                this.SetText("This tab contains all the other items you might need to craft.");
                break;
            default:
                StopTutorial();
                break;
        }
    }

    public override void StopTutorial()
    {

        InventoryController.tabController.TabSwitcher(TabController.Tab.FoodAndDrink);
        base.StopTutorial();
    }
}
