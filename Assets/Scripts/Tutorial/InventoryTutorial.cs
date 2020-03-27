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
    
    public override void startTutorial()
    {
        food.SetActive(false);
        med.SetActive(false);
        gear.SetActive(false);
        trash.SetActive(false);
        
        
        nextDialog = TutorialController.TUTORIAL_CONTROLER.GetDialogButton();
        dialogText = TutorialController.TUTORIAL_CONTROLER.GetDialog();
        
        nextDialog.onClick.RemoveAllListeners();
        nextDialog.onClick.AddListener(NextStep);
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().InventoryButtonClicked();
        
        //first step
        currentStep = -1;
        hasBeenStarted = true;
        NextStep();
        
        this.gameObject.SetActive(true);
    }

    public override bool hasBeenStart()
    {
        return hasBeenStarted;
    }
    

    public override void NextStep()
    {
        currentStep++;
        switch (currentStep)
        {
            case 0:
                dialogText.SetText("The inventory allows you to view, use or discard the items you own.");
                break;
            case 1:
                dialogText.SetText("It is divided into several parts");
                break;
            case 2:
                //tab food&gear
                InventoryController.tabController.TabBtnListener(TabController.Tab.FoodAndDrink);
                food.SetActive(true);
                dialogText.SetText("The food and drink tab lists the items that affect your hunger and thirst.");
                break;
            case 3:
                InventoryController.tabController.TabBtnListener(TabController.Tab.Meds);
                food.SetActive(false);
                med.SetActive(true);
                dialogText.SetText("The med tab takes the items that are used to recover from life");
                break;
            case 4:
                InventoryController.tabController.TabBtnListener(TabController.Tab.Gear);
                med.SetActive(false);
                gear.SetActive(true);
                dialogText.SetText("The gear tab lists the items you can wear that will help you when you get out of the bunker.");
                break;
            case 5:
                InventoryController.tabController.TabBtnListener(TabController.Tab.Misc);
                gear.SetActive(false);
                trash.SetActive(true);
                dialogText.SetText("This tab contains all the other items you might need to craft.");
                break;
            default:
                StopTutorial();
                break;
        }
    }

    private void StopTutorial()
    {
        InventoryController.tabController.TabBtnListener(TabController.Tab.FoodAndDrink);
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().GoToBunker();
        this.gameObject.SetActive(false);
        TutorialController.TUTORIAL_CURRENT_STEP++;
        TutorialController.TUTORIAL_CONTROLER.backPanel.SetActive(true);
    }
}
