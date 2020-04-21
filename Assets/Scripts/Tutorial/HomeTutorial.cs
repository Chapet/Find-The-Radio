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

    public override void StartTutorial()
    {
        base.StartTutorial();
        bareDeVie.SetActive(false);
        inventory.SetActive(false);
        bed.SetActive(false);
        crafting.SetActive(false);
        scavenging.SetActive(false);

        currentStep = 0;
        NextStep();
        

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
                this.SetText("You can recuperate energy in your sleep. But don't forget that time passes during this time...");
                break;
            case 2 :
                //inventory
                bed.SetActive(false);
                inventory.SetActive(true);
                this.SetText("Your items are visible in the inventory");
                break;

            case 3 :
                //crafting
                inventory.SetActive(false);
                crafting.SetActive(true);
                this.SetText(("The crafting table allows you to build new items from those found outside."));
                break;
                
            case 4:
                //scavenging
                crafting.SetActive(false);
                scavenging.SetActive(true);
                this.SetText("By clicking on the ladder and getting out of the bunker, you can try to find new items outside, but this can be dangerous.");
                break;
            default:
                StopTutorial();
                break;
        }
    }


}
