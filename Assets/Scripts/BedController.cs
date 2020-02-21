using System;
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
        gameObject.SetActive(false);
        backPanel.SetActive(false);
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
}
