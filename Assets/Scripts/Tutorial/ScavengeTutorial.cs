using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScavengeTutorial : TutorialStep
{
    
    
    public GameObject slider;
    
    
    public override void StartTutorial()
    {
        base.StartTutorial();
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().ScavengeButtonClicked();
        
        currentStep = -1;
        NextStep();
    }
    


    public override void NextStep()
    {
        currentStep++;
        switch (currentStep)
        {
            case 0:
                slider.SetActive(true);
                this.SetText("The slider let you choose how long you will be out of the bunker.");
                break;
            case 1:
                slider.SetActive(false);
                this.SetText("The longer you're out, the more good items you'll find.");
                break;
            case 2:
                this.SetText("However, the longer you go out, the more dangerous it gets");
                break;
            case 3:
                this.SetText("Gears can protect you from certain dangers.");
                break;
            case 4:
                this.SetText("During the scavening, you can't sleep, use items or craft.");
                break;
            default:
                StopTutorial();
                break;
        }
    }
    
}
