using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameController.NewGame = true;
        Debug.Log("New game selected");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Continue() 
    {
        GameController.NewGame = false;
        Debug.Log("Continue selected");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
