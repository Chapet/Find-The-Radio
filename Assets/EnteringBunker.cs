using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnteringBunker : MonoBehaviour
{
    public Animator transitionAnim;
    public GameObject transitionPanel;

    void Start()
    {
        transitionPanel.SetActive(true);
    }

    void Awake()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("entering");
        yield return new WaitForSeconds(0.5f);
        transitionPanel.SetActive(false);
    }
}