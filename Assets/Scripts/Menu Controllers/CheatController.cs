using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatController : MonoBehaviour
{
    //public GameObject bunkerPanel;
    //public GameObject backPanel;
    public GameController gameController;

    public TMP_Text gameClockText;
    public GameObject timeSlider;
    private float gameClockTime;

    public  MenuController menuController;

    // Update is called once per frame
    void Update()
    {
        gameClockTime = (timeSlider.GetComponent<Slider>().value);
        string hours = Mathf.Floor(gameClockTime).ToString("0");
        gameClockText.SetText("Add " + hours + "h" + " to the Game Clock.");
    }

    public void UpdateBtnClicked()
    {
        gameController.UpdateGameClock(gameClockTime);
        menuController.ExitMenu(gameObject);
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }
}
