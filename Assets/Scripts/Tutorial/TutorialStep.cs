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
    public abstract void startTutorial();

    /**
     * Retourn true if this step of the tutorial has already been launched, false ortherwise
     */
    public abstract bool hasBeenStart();

    public abstract void NextStep();
}
