 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    public void NewGame()
    {
        GameController.NewGame = true;
        Debug.Log("New game selected");
        StartCoroutine(LoadScene());
    }

    public void ContinueGame()
    {
        GameController.NewGame = false;
        Debug.Log("Continue selected");
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("out");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneName);
    }
}
