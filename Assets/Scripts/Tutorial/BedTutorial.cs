using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BedTutorial : TutorialStep
{
    
    
    public GameObject bedSlider;
    
    
    public override void startTutorial()
    {
        bedSlider.SetActive(false);
        
        nextDialog = TutorialController.TUTORIAL_CONTROLER.GetDialogButton();
        dialogText = TutorialController.TUTORIAL_CONTROLER.GetDialog();
        
        nextDialog.onClick.RemoveAllListeners();
        nextDialog.onClick.AddListener(NextStep);
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().SleepButtonClicked();
        //TutorialController.TUTORIAL_CONTROLER.backPanel.GetComponent<Image>().enabled = false;
        
        
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
                dialogText.SetText("The bed allows you to recuperate energy");
                break;
            case 1:
                bedSlider.SetActive(true);
                dialogText.SetText("You choose the time you sleep with the slider, the more you sleep, the more energy you get back, but during this time you may be thirsty and/or hungry.");
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
