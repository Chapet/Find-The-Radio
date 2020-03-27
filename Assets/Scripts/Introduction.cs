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

    private string[] introTxt=
    {
        "coucou",
        "viucezrzr"
    };
    
    [SerializeField] private TutorialController _tutorialController;

    // Start is called before the first frame update
    void Start()
    {
        text.SetText(introTxt[0]);
        //background.transform.position = new Vector3(0, -700);
    }
    //.transform.position = new Vector3(0, 20, 0)
    
    // Update is called once per frame
    public void StartIntroduction()
    {
        this.gameObject.SetActive(true);   
        text.SetText(introTxt[0]);
        
        
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextStep);
        
    }

    private int currentTxt = 0;
    public void NextStep()
    {
        currentTxt++;
        if (currentTxt >= introTxt.Length)
        {
            //FIN DE L'INTRO
            this.gameObject.SetActive(false);
            //this.gameObject.transform.position=new Vector3(0,421);//on resize pour le tutorial
            
            
            //DEBUT TUTORIAL
            _tutorialController.StartTutorial();
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
        else
        {
            // on continue l'intro
            text.SetText(introTxt[currentTxt]);
        }
    }
    
    
}
