using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BedController : MonoBehaviour
{
    public GameController gameController;
    public PlayerController player;
    //public GameObject BunkerPanel;
    public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text sleepText;
    public GameObject Slider;
    private float sleepTime;

    public Animator anim;

    public float animDuration = 15f / 60f;

    public CanvasGroup canvGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        sleepTime = (Slider.GetComponent<Slider>().value) / 2;
        String hours = Math.Truncate(sleepTime).ToString("0");
        String minutes = Math.Truncate((sleepTime - Math.Truncate(sleepTime)) * 60).ToString("0");
        sleepText.SetText("Sleep for : " + hours + "h" + minutes);
    }

    public void OnValueChanged(float newValue)
    {
        Debug.Log(gameObject.GetComponent<Slider>().value);
        Debug.Log(newValue);
    }

    public void ExitButtonClicked()
    {
        //BunkerPanel.SetActive(true);
        StartCoroutine(DoFade(canvGroup, 1, 0));
        StartCoroutine(ExitWithAnim(animDuration));
        
    }

    public void SleepButtonClicked() {
        Sleep();
        ExitButtonClicked();
    }

    void Sleep()
    {
        gameController.UpdateGameClock(sleepTime);
        double hours = Math.Truncate(sleepTime);
        double minutes = Math.Truncate((sleepTime - Math.Truncate(sleepTime)) * 60);
        player.UpdateEnergy(2 * sleepTime);
    }

    public IEnumerator ExitWithAnim(float f)
    {
        Debug.Log("Waiting for " + f + " seconds ...");
        anim.SetBool("open", false);
        anim.SetBool("close", true);
        backPanel.SetActive(false);
        yield return new WaitForSeconds(f);
        anim.SetBool("close", false);
        gameObject.SetActive(false);
    }

    public void PlayOpenAnim()
    {
        StartCoroutine(DoFade(canvGroup, 0, 1));
        backPanel.SetActive(true);
        anim.SetBool("close", false);
        anim.SetBool("open", true);
    }

    private IEnumerator DoFade(CanvasGroup c, float start, float end)
    {
        float counter = 0f;

        while(counter < animDuration)
        {
            counter += Time.deltaTime;
            c.alpha = Mathf.Lerp(start, end, counter / animDuration);

            yield return null;
        }
    }
}
