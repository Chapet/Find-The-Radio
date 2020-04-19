using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
    public static PopupSystem Popup_System { get; private set; }
    public TMP_Text textPopupMsg;
    public Button acceptButton;
    public Button refuseButton;
    public Button okButton;
    public GameObject popPanel;
    public GameObject backPanel;
    public PlayerController player;
    public enum Popup
    {
        Bite, Death, DeathEvent, ZombieOne0, ZombieOne1, ZombieFew0, ZombieFew1, ZombieLot0,
        ZombieLot1, GroceryStore, ClothingStore, HuntingStore, Parc, OutdoorStore, PoliceStation, Pharmacy, Radio
    };

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
            case Popup.ZombieOne0: //event lvl 1
                textPopupMsg.text = "   You were biten by a zombie!";
                player.UpdateHealth(-15);
                okButton.gameObject.SetActive(true);
                break;
            case Popup.ZombieOne1: //event lvl 1
                textPopupMsg.text = "   A zombie attacked you but you were able to kill it with your gun!";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.ZombieFew0: //event lvl 2
                textPopupMsg.text = "   You were attacked by a bunch of zombies!";
                player.UpdateHealth(-45);
                okButton.gameObject.SetActive(true);
                break;
            case Popup.ZombieFew1: //event lvl 2
                textPopupMsg.text = "   A bunch of zombies attacked you but you were able to kill them with your gun!";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.ZombieLot0: //event lvl 3
                textPopupMsg.text = "   You were attacked by a large group of zombies!";
                player.UpdateHealth(-75);
                okButton.gameObject.SetActive(true);
                break;
            case Popup.ZombieLot1: //event lvl 3
                textPopupMsg.text = "   A large group of zombies attacked you but you were able to kill them with your gun!";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.GroceryStore:  //event lvl 1
                textPopupMsg.text = "   You searched a grocery store and found some supplies.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.ClothingStore: //event lvl 1
                textPopupMsg.text = "   You searched a clothing store and found some supplies.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.HuntingStore: //event lvl 2
                textPopupMsg.text = "   You searched a hunting store and found some supplies.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.OutdoorStore: //event lvl 2
                textPopupMsg.text = "   You searched an outdooor store and found some supplies.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.PoliceStation: //event lvl 3
                textPopupMsg.text = "   You searched a police station and fond some supplies.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.Parc: //event lvl 1
                textPopupMsg.text = "   You found a parc with some usable wood.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.Pharmacy: //event lvl 2
                textPopupMsg.text = "   You searched a pharmacy and found some medical supplies.";
                okButton.gameObject.SetActive(true);
                break;
            case Popup.Radio: //event lvl 3
                textPopupMsg.text = "   You found some radio parts!";
                okButton.gameObject.SetActive(true);
                break;
        }
    }
}
