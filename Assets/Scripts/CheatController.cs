using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatController : MonoBehaviour
{
    //public GameObject bunkerPanel;
    public GameObject backPanel;
    public GameController gameController;

    public TMP_Text gameClockText;
    public GameObject timeSlider;
    private float gameClockTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameClockTime = (timeSlider.GetComponent<Slider>().value);
        string hours = Mathf.Floor(gameClockTime).ToString("0");
        gameClockText.SetText("Add " + hours + "h" + " to the Game Clock.");
    }

    public void ExitButtonClicked()
    {
        //bunkerPanel.SetActive(true);
        gameObject.SetActive(false);
        backPanel.SetActive(false);
    }

    public void UpdateBtnClicked()
    {
        gameController.UpdateGameClock(gameClockTime);
        ExitButtonClicked();
    }
}
