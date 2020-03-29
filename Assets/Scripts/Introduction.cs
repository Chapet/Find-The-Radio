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

    private string[] introTxt=
    {
        "cela prend place durant l'an 20erzrz ou les petut essaye d e prende de plus en plius de place les prochains jours pcq j et on saot trop bien que ce texte est trop logn pour le cadre et ouis voilà ce qui se passe quant ut revail",
        "viucezrzr"
    };
    
    [SerializeField] private TutorialController _tutorialController;

    // Start is called before the first frame update
    void Start()
    {
        this.SetText(introTxt[0]);
        _scrollRect.enabled = false;
        //background.transform.position = new Vector3(0, -700);
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
            this.SetText(introTxt[currentTxt]);
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
