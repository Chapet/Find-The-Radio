using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //public StatusBarController barController;
    public GameObject backPanel;
    private Animator openCloseMenuAnimator;
    private float animDuration = 10f / 60f;
    private static MenuController instance;
    public static MenuController Controller;

    void Awake()
    {
        instance = this;
        Controller = this;
    }

    public void ExitMenu(GameObject panel)
    {
        var canvGroup = panel.GetComponent<CanvasGroup>();
        openCloseMenuAnimator = panel.GetComponent<Animator>();
        StartCoroutine(DoFade(canvGroup, 1, 0));
        StartCoroutine(BackPanel());
        StartCoroutine(ExitWithAnim(panel, animDuration));
    }

    public void OpenMenu(GameObject panel)
    {
        var canvGroup = panel.GetComponent<CanvasGroup>();
        openCloseMenuAnimator = panel.GetComponent<Animator>();
        panel.SetActive(true);
        StartCoroutine(DoFade(canvGroup, 0, 1));
        StartCoroutine(BackPanel());
        StartCoroutine(OpenWithAnim(animDuration));
    }

    private IEnumerator OpenWithAnim(float f)
    {
        openCloseMenuAnimator.SetBool("close", false);
        openCloseMenuAnimator.SetBool("open", true);
        
        yield return new WaitForSeconds(f);
    }

    private IEnumerator ExitWithAnim(GameObject panel, float f)
    {
        openCloseMenuAnimator.SetBool("open", false);
        openCloseMenuAnimator.SetBool("close", true);
        yield return new WaitForSeconds(f);
        openCloseMenuAnimator.SetBool("close", false);
        panel.SetActive(false);
    }

    private IEnumerator DoFade(CanvasGroup c, float start, float end)
    {
        float counter = 0f;

        while (counter < animDuration)
        {
            counter += Time.deltaTime;
            c.alpha = Mathf.Lerp(start, end, counter / animDuration);

            yield return null;
        }
    }

    private bool backPanelActive = false;
    private IEnumerator BackPanel()
    {
        backPanelActive = !backPanelActive;
        backPanel.SetActive(backPanelActive);
        yield return null;
    }

    public static void Transition(GameObject transitionPanel, Animator anim)
    {
        instance.StartCoroutine(CoroutineAnim(transitionPanel, anim, null));       
    }

    public static void Transition(GameObject transitionPanel, Animator anim, string sceneName)
    {
        instance.StartCoroutine(CoroutineAnim(transitionPanel, anim, sceneName));
    }

    private static IEnumerator CoroutineAnim(GameObject transitionPanel, Animator anim, string sceneName)
    {
        transitionPanel.SetActive(true);
        float st = 0.02f;
        float lg = 0.18f;
        if (sceneName == null)
        {
            var tmp_color = transitionPanel.GetComponent<Image>().color;
            tmp_color.a = 1f;
            transitionPanel.GetComponent<Image>().color = tmp_color;

            yield return new WaitForSeconds(st);
            anim.SetTrigger("toTransparent");
            yield return new WaitForSeconds(lg);

            tmp_color = transitionPanel.GetComponent<Image>().color;
            tmp_color.a = 1f;
            transitionPanel.GetComponent<Image>().color = tmp_color;
        }
        else
        {
            var tmp_color = transitionPanel.GetComponent<Image>().color;
            tmp_color.a = 0f;
            transitionPanel.GetComponent<Image>().color = tmp_color;

            anim.SetTrigger("toOpaque");
            yield return new WaitForSeconds(lg);
            SceneManager.LoadScene(sceneName);
            yield return new WaitForSeconds(st);

            tmp_color = transitionPanel.GetComponent<Image>().color;
            tmp_color.a = 0f;
            transitionPanel.GetComponent<Image>().color = tmp_color;
        }
        transitionPanel.SetActive(false);
    }
}
