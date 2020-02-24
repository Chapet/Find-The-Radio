using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject backPanel;
    private Animator openCloseMenuAnimator;
    private float animDuration = 10f / 60f;

    public void ExitMenu(GameObject panel)
    {
        var canvGroup = panel.GetComponent<CanvasGroup>();
        openCloseMenuAnimator = panel.GetComponent<Animator>();
        StartCoroutine(DoFade(canvGroup, 1, 0));
        StartCoroutine(ExitWithAnim(panel, animDuration));
    }

    public void OpenMenu(GameObject panel)
    {
        var canvGroup = panel.GetComponent<CanvasGroup>();
        openCloseMenuAnimator = panel.GetComponent<Animator>();
        panel.SetActive(true);
        StartCoroutine(DoFade(canvGroup, 0, 1));
        openCloseMenuAnimator.SetBool("close", false);
        openCloseMenuAnimator.SetBool("open", true);
        backPanel.SetActive(true);
    }

    private IEnumerator ExitWithAnim(GameObject panel, float f)
    {
        openCloseMenuAnimator.SetBool("open", false);
        openCloseMenuAnimator.SetBool("close", true);
        backPanel.SetActive(false);
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
}
