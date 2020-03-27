using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScavengeTutorial : TutorialStep
{
    
    
    public GameObject slider;
    
    
    public override void startTutorial()
    {
        nextDialog = TutorialController.TUTORIAL_CONTROLER.GetDialogButton();
        dialogText = TutorialController.TUTORIAL_CONTROLER.GetDialog();
        
        nextDialog.onClick.RemoveAllListeners();
        nextDialog.onClick.AddListener(NextStep);
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().ScavengeButtonClicked();
        
        //first Step
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
                slider.SetActive(true);
                dialogText.SetText("The slider let you choose how long you will be out of the bunker.");
                break;
            case 1:
                slider.SetActive(false);
                dialogText.SetText("The longer you're out, the more good items you'll find.");
                break;
            case 2:
                dialogText.SetText("However, the longer you go out, the more dangerous it gets");
                break;
            case 3:
                dialogText.SetText("Gears can protect you from certain dangers.");
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
