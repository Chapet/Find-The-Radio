using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public MenuController menuController;
    public GameObject optionsMenu;

    private void Start()
    {
        Application.targetFrameRate = GameController.framerate;
    }

    public void OptionsBtnClicked()
    {
        menuController.OpenMenu(optionsMenu);
    }
}
