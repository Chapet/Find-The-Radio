using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TutorialStep:MonoBehaviour
{
    protected int currentStep = 0;
    protected int lastCurrentStep = 0;
    protected bool hasBeenStarted = false;
    protected TextMeshProUGUI dialogText;
    protected Button nextDialog;
    protected ScrollRect _scrollRect;



    public virtual void StartTutorial()
    {
        nextDialog = TutorialController.TUTORIAL_CONTROLER.GetDialogButton();
        dialogText = TutorialController.TUTORIAL_CONTROLER.GetDialog();
        _scrollRect = TutorialController.DIALOG_SCROLL_RECT;
        nextDialog.onClick.RemoveAllListeners();
        nextDialog.onClick.AddListener(NextStep);
        hasBeenStarted = true;
        this.gameObject.SetActive(true);
    }

    /**
     * Retourn true if this step of the tutorial has already been launched, false ortherwise
     */
    public bool hasBeenStart()
    {
        return hasBeenStarted;
    }

    public abstract void NextStep();

    protected void SetText(string s)
    {
        if (s.Length >= TutorialController.TEXT_MAX_CARACTERE)
        {
            _scrollRect.enabled = true;
        }
        else
        {
            _scrollRect.enabled = false;
        }

        dialogText.SetText(s);
    }

    public virtual void StopTutorial()
    {
        TutorialController.TUTORIAL_CONTROLER.GetBunkerCotnroller().GoToBunker();
        this.gameObject.SetActive(false);
        TutorialController.TUTORIAL_CURRENT_STEP++;
        TutorialController.TUTORIAL_CONTROLER.backPanel.SetActive(true);
    }
}
