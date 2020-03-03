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
    //public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text sleepText;
    public GameObject Slider;
    private float sleepTime;

    public MenuController menuController;

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
        gameController.UpdateGameClock(sleepTime);
        // 1 energy point per 9.6 mintues <=> 6.25 energy points per hour <=> 100 energy point per 16 hours
        player.UpdateEnergy(Mathf.FloorToInt((Mathf.Floor(sleepTime) + Mathf.Floor((sleepTime - Mathf.Floor(sleepTime)))) * 60f / 9.6f));
    }
}
