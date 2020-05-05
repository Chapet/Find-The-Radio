using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BedController : MonoBehaviour
{
    private GameController gameController;
    public PlayerController player;
    //public GameObject BunkerPanel;
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text SliderText;
    public TMP_Text ETAText;
    public TMP_Text AddDayText;
    public GameObject Slider;
    public float sleepTime;

    public MenuController menuController;

    private void Start()
    {
        gameController = GameController.Controller;
    }

    void Update()
    {
        sleepTime = (Slider.GetComponent<Slider>().value) / 2;
        String hours = Math.Truncate(sleepTime).ToString("00");
        String minutes = Math.Truncate((sleepTime - Math.Truncate(sleepTime)) * 60).ToString("00");
        SliderText.SetText(hours + "h" + minutes);
        UpdateETA();
    }

    private void UpdateETA()
    {
        double eta = gameController.gameClock + sleepTime;
        string etaHoursStr;
        string etaMinutesStr;
        if (eta >= 24)
        {
            eta = eta % 24;
            AddDayText.SetText("(+1 day)");
        }
        else
        {
            AddDayText.SetText("");
        }
        etaHoursStr = Math.Truncate(eta).ToString("00");
        etaMinutesStr = Math.Truncate((eta - Math.Truncate(eta)) * 60).ToString("00");
        ETAText.SetText(etaHoursStr + "h" + etaMinutesStr);
    }

    public void OnValueChanged(float newValue)
    {
        Debug.Log(gameObject.GetComponent<Slider>().value);
        Debug.Log(newValue);
    }

    public void SleepButtonClicked() {
        Sleep();
        menuController.ExitMenu(gameObject);
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }

    void Sleep()
    {
        //StopCoroutine(gameController.Sleep(sleepTime));
        float eta = gameController.gameClock + sleepTime;
        Debug.Log("ETA : " + eta);
        BackgroundTasks.Tasks.Sleep(sleepTime);
    }

    
}
