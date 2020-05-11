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
    
    public override void StartTutorial()
    {
        base.StartTutorial();
        
        itemNeeded_ok.SetActive(false);
        itemNeeded_notOk.SetActive(false);
        finalItem.SetActive(false);
        
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().CraftingBtnClicked();
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
                this.SetText("The crafting table allows you to build new items with what you have found.");
                break;
            case 1:
                finalItem.SetActive(true);
                this.SetText("The item that will be crafted is displayed here");
                break;
            case 2:
                finalItem.SetActive(false);
                itemNeeded_ok.SetActive(true);
                this.SetText("The elements you need to craft your item are indicated here. ");
                break;
            case 3:
                itemNeeded_ok.SetActive(false);
                itemNeeded_notOk.SetActive(true);
                this.SetText("The items in red are the ones you don't have.");
                break;
            default:
                StopTutorial();
                break;
        }
    }

}
