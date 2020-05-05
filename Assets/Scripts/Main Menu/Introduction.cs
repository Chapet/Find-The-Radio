using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Introduction : MonoBehaviour
{
    private GameObject camera;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject backpanel;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private int TEXT_MAX_CARACTERE;
    [SerializeField] private GameObject backgroundBunker;
    private Vector3 initalPosition;
    private Vector3 tempPosition;

    private string[] introTxt=
    {
        "The year is 2020, a virus causes a global pandemic, bringing humanity to its knees.",
        "The global healthcare system is unable to follow, the virus spreads and mutates at an abnormal speed.",
        "People infected by the virus begin to change, showing signs of dementia and aggressive behaviors.",
        "Before measures can be taken, all the infected turn into zombies.",
        "The governments fall one after the others, as the virus spreads to every corner of the world.",
        "A short time after, only a few people, immuned to the virus, were able to survive.",
        "You are one of those survivors.",
        "All by yourself, you desperatly search for other survivors and a safe shelter.",
        "Finding radio parts and assembling them to commmunicate is your last hope.",
        "Good Luck."
    };
    
    [SerializeField] private TutorialController _tutorialController;

    // Start is called before the first frame update
    void Start()
    {
        this.SetText(introTxt[0]);
        _scrollRect.enabled = false;
        
        initalPosition = backgroundBunker.transform.position;
        tempPosition = initalPosition;
        tempPosition.y -= 11f;
        backgroundBunker.transform.position = tempPosition;


    }
    //.transform.position = new Vector3(0, 20, 0)
    
    // Update is called once per frame
    public void StartIntroduction()
    {
        this.gameObject.SetActive(true);   
        this.SetText(introTxt[0]);
        
        
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextStep);
        
    }

    private int currentTxt = 0;
    private Vector3 positionGoal;
    public void NextStep()
    {
        currentTxt++;
        if (currentTxt >= introTxt.Length)
        {
            //FIN INTRODUCTION
            positionGoal = initalPosition;
            updatePosition = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            // on continue l'intro
            this.SetText(introTxt[currentTxt]);
        }
    }

    private bool updatePosition = false;
    public void FixedUpdate()
    {
        if (updatePosition)
        {
            if (backgroundBunker.transform.position.y < positionGoal.y)
            {
                backgroundBunker.transform.position=new Vector3(backgroundBunker.transform.position.x,backgroundBunker.transform.position.y+0.25f,backgroundBunker.transform.position.z);
            }
            else
            {
                //FIN DE L'INTRO
                this.gameObject.SetActive(false);
                //this.gameObject.transform.position=new Vector3(0,421);//on resize pour le tutorial
                backgroundBunker.transform.position = initalPosition;
            
                //DEBUT TUTORIAL
                _tutorialController.StartTutorial();
                this.gameObject.SetActive(false);
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                this.enabled = false;
                
                updatePosition = false;
            }
            
        }
    }

    private void SetText(string s)
    {
        if (s.Length >= TEXT_MAX_CARACTERE)
        {
            _scrollRect.enabled = true;
        }
        else
        {
            _scrollRect.enabled = false;
        }

        text.SetText(s);
    }


}
