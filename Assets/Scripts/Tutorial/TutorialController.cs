﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static TutorialController TUTORIAL_CONTROLER
    {
        get; private set;
    }

    public static int TUTORIAL_CURRENT_STEP = 0;

    private int last_tutorial_current_step = -1;

    [SerializeField] private BunkerController bunkerController;
    [SerializeField]private Button nextDialog;
    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField] private List<TutorialStep> tutorialSteps;
    [SerializeField] public GameObject backPanel;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        TUTORIAL_CONTROLER = this;
        this.gameObject.SetActive(true);
    }

    private void Start()
    {
        //backPanel.SetActive(true);
    }

    public void StartTutorial()
    {
        this.gameObject.SetActive(true);
    }

    public TextMeshProUGUI GetDialog()
    {
        return dialogText;
    }

    public Button GetDialogButton()
    {
        return nextDialog;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TUTORIAL_CURRENT_STEP != last_tutorial_current_step || last_tutorial_current_step==-1)
        {
            if (last_tutorial_current_step == -1)
            {
                last_tutorial_current_step = 0;} //1er 
            
            //==========    NEXT TUTORIAL STEP    ==============
            
            if (tutorialSteps.Count  > TUTORIAL_CURRENT_STEP && !tutorialSteps[TUTORIAL_CURRENT_STEP].hasBeenStart())
            {
                //=============    START STEP    ===================
                tutorialSteps[TUTORIAL_CURRENT_STEP].startTutorial();
            }
            else if (tutorialSteps.Count <= TUTORIAL_CURRENT_STEP)
            {
                //=============    END OF TUTORIAL    ==============
                backPanel.SetActive(false);
                this.gameObject.SetActive(false);
            }
            
        }
        last_tutorial_current_step = TUTORIAL_CURRENT_STEP;
    }

    public BunkerController GetBunkerCotnroller()
    {
        return bunkerController;
    }
}
