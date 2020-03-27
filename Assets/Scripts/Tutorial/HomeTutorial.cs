using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HomeTutorial : TutorialStep
{
  
    

    public GameObject bareDeVie;
    public GameObject inventory;
    public GameObject bed;
    public GameObject crafting;
    public GameObject scavenging;

    public override void startTutorial()
    {
        bareDeVie.SetActive(false);
        inventory.SetActive(false);
        bed.SetActive(false);
        crafting.SetActive(false);
        scavenging.SetActive(false);
        
        nextDialog = TutorialController.TUTORIAL_CONTROLER.GetDialogButton();
        dialogText = TutorialController.TUTORIAL_CONTROLER.GetDialog();
        
        nextDialog.onClick.RemoveAllListeners();
        nextDialog.onClick.AddListener(NextStep);
        
        
        //FIRST STEP
        hasBeenStarted = true;
        currentStep = 0;
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
                //bare de vie
                
                break;
            case 1:
                //bed
                bareDeVie.SetActive(false);
                bed.SetActive(true);
                dialogText.SetText("You can recuperate energy in your sleep. But don't forget that time passes during this time...");
                break;
            case 2 :
                //inventory
                bed.SetActive(false);
                inventory.SetActive(true);
                dialogText.SetText("Your items are visible in the inventory");
                break;

            case 3 :
                //crafting
                inventory.SetActive(false);
                crafting.SetActive(true);
                dialogText.SetText(("The crafting table allows you to build new items from those found outside."));
                break;
                
            case 4:
                //scavenging
                crafting.SetActive(false);
                scavenging.SetActive(true);
                dialogText.SetText("By clicking on the ladder and getting out of the bunker, you can try to find new items outside, but this can be dangerous.");
                break;
            default:
                StopTutorial();
                break;
        }
    }

    private void StopTutorial()
    {
        this.gameObject.SetActive(false);
        TutorialController.TUTORIAL_CURRENT_STEP++;
    }
}
