using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingTutorial : TutorialStep
{
    
    
    public GameObject itemNeeded_ok;
    public GameObject itemNeeded_notOk;
    public GameObject finalItem;

    private string[] dialogs = {"coucou estce q", "erzkhrzkjrzerz"};
    private int currentDialog = 0;
    
    public override void startTutorial()
    {
        itemNeeded_ok.SetActive(false);
        itemNeeded_notOk.SetActive(false);
        finalItem.SetActive(false);
        
        nextDialog = TutorialController.TUTORIAL_CONTROLER.GetDialogButton();
        dialogText = TutorialController.TUTORIAL_CONTROLER.GetDialog();
        
        nextDialog.onClick.RemoveAllListeners();
        nextDialog.onClick.AddListener(NextStep);
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().CraftingBtnClicked();
        
        
        //first step
        hasBeenStarted = true;
        currentStep = -1;
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
                dialogText.SetText("The crafting table allows you to build new items with what you have found.");
                break;
            case 1:
                finalItem.SetActive(true);
                dialogText.SetText("The item that will be crafted is displayed here");
                break;
            case 2:
                finalItem.SetActive(false);
                itemNeeded_ok.SetActive(true);
                dialogText.SetText("The elements you need to crack your item are indicated here. ");
                break;
            case 3:
                itemNeeded_ok.SetActive(false);
                itemNeeded_notOk.SetActive(true);
                dialogText.SetText("The items in red are the ones you don't have.");
                break;
            default:
                StopTutorial();
                break;
        }
    }

    private void StopTutorial()
    {
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().GoToBunker();
        this.gameObject.SetActive(false);
        TutorialController.TUTORIAL_CURRENT_STEP++;
        TutorialController.TUTORIAL_CONTROLER.backPanel.SetActive(true);
    }
}
