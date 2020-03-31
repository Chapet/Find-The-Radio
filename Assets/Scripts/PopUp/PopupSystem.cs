using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
    public TMP_Text textPopupMsg;
    public Button acceptButton;
    public Button refuseButton;
    public Button okButton;
    public GameObject popPanel;
    public GameObject backPanel;
    public PlayerController player;
    public enum Popup {Bite, Death, DeathEvent};

    public void PopMessage(Popup popup) 
    {
        popPanel.SetActive(true);
        backPanel.SetActive(true);

        switch (popup) 
        {
            case Popup.Bite:
                textPopupMsg.text = "   You were biten by a zombie!";
                player.UpdateHealth(-20);
                //update player effect: biten/bleeding
                okButton.gameObject.SetActive(true);
                break;
            case Popup.DeathEvent:
                textPopupMsg.text = "   You were assaulted by a crowd of zombies, you didn't make it";
                player.UpdateHealth(-100);
                okButton.gameObject.SetActive(true);
                break;
            case Popup.Death:
                textPopupMsg.text = "   You were unable to survive in this harsh world. Your story ends here.";
                okButton.gameObject.SetActive(true);
                break;
        }
    }
}
