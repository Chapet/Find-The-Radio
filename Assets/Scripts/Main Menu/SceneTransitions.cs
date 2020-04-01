 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim;
    public GameObject transitionPanel;

    private void Start()
    {
        MenuController.Transition(transitionPanel, transitionAnim);
    }

    public void NewGame()
    {
        GameController.NewGame = true;
        MenuController.Transition(transitionPanel, transitionAnim, "BunkerScene");
    }

    public void ContinueGame()
    {
        GameController.NewGame = false;
        MenuController.Transition(transitionPanel, transitionAnim, "BunkerScene");
    }
}
