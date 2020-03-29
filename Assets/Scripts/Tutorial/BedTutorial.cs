using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BedTutorial : TutorialStep
{
    
    
    public GameObject bedSlider;
    
    
    public override void StartTutorial()
    {
        base.StartTutorial();
        bedSlider.SetActive(false);
        
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().SleepButtonClicked();
        
        currentStep = -1;
        NextStep();

    }



    public override void NextStep()
    {
        currentStep++;
        switch (currentStep)
        {
            case 0:
                this.SetText("The bed allows you to recuperate energy");
                break;
            case 1:
                bedSlider.SetActive(true);
                this.SetText("You choose the time you sleep with the slider, the more you sleep, the more energy you get back, but during this time you may be thirsty and/or hungry.");
                break;
            default:
                StopTutorial();
                break;
        }
    }

    protected override void StopTutorial()
    {
        base.StopTutorial();
    }
}
