using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public MenuController menuController;
    public GameObject optionsMenu;

    public void OptionsBtnClicked()
    {
        menuController.OpenMenu(optionsMenu);
    }
}
