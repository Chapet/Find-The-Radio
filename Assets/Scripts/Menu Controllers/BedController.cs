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
    public TMP_Text sleepText;
    public GameObject Slider;
    private float sleepTime;

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
        //StopCoroutine(gameController.Sleep(sleepTime));
        BackgroundTasks.BgTasks.Sleep(sleepTime);
    }

    
}
