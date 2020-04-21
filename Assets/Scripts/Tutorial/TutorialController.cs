using System;
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

    public static int TEXT_MAX_CARACTERE;

    public static ScrollRect DIALOG_SCROLL_RECT
    {
        get; private set;
    }

    public static int TUTORIAL_CURRENT_STEP = 0;

    private int last_tutorial_current_step = -1;

    [SerializeField] private BunkerController bunkerController;

    [Tooltip("Tutorial Bow Without skip button")] 
    [SerializeField] private GameObject tutorialBoxWithoutSkip;
    [SerializeField]private Button nextDialog;
    [SerializeField] private Button skipTutorial;
    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField] private List<TutorialStep> tutorialSteps;
    [SerializeField] public GameObject backPanel;
    [SerializeField] public ScrollRect scrollRect;
    [SerializeField] public int text_max_character;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        TEXT_MAX_CARACTERE = text_max_character;
        TUTORIAL_CONTROLER = this;
        DIALOG_SCROLL_RECT = scrollRect;
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

    public Button GetSkipTutorialButton()
    {
        return skipTutorial;
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
                tutorialSteps[TUTORIAL_CURRENT_STEP].StartTutorial();
            } 
            else if (tutorialSteps.Count <= TUTORIAL_CURRENT_STEP)
            {
                //=============    END OF TUTORIAL    ==============
                EndOfTutorial();
            }
            
        }
        last_tutorial_current_step = TUTORIAL_CURRENT_STEP;
    }

    public BunkerController GetBunkerCotnroller()
    {
        return bunkerController;
    }

    public void SkipTutorial()
    {
        EndOfTutorial();
    }

    private void EndOfTutorial()
    {
        backPanel.SetActive(false);
        PlayerController.IS_FIRST_GAME = false;
        this.gameObject.SetActive(false);
    }
}
