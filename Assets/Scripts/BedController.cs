using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BedController : MonoBehaviour
{
    public GameObject GameController;
    public GameObject BunkerPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text sleepText;
    public GameObject Slider;
    private float sleepTime;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        sleepTime = Slider.GetComponent<Slider>().value;
        sleepText.SetText("Sleep for : " + sleepTime.ToString("0.0") + " h.");
    }

    public void OnValueChanged(float newValue)
    {
        Debug.Log(gameObject.GetComponent<Slider>().value);
        Debug.Log(newValue);
    }

    public void ExitButtonClicked()
    {
        BunkerPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SleepButtonClicked() {
        Sleep();
        ExitButtonClicked();
    }

    void Sleep()
    {
        GameController script = GameController.GetComponent<GameController>();
        script.UpdateGameClock(sleepTime);
    }
}
