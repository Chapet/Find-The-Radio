using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScavengingController : MonoBehaviour
{
    public GameObject GameController;
    public GameObject BunkerPanel;
    public GameObject backPanel;
    public Color BrightYellow;
    public Color DarkYellow;
    public TMP_Text scavengeText;
    public GameObject Slider;
    private float scavengeTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        scavengeTime = (Slider.GetComponent<Slider>().value) / 2;
        String hours = Math.Truncate(scavengeTime).ToString("0");
        String minutes = Math.Truncate((scavengeTime - Math.Truncate(scavengeTime)) * 60).ToString("0");
        scavengeText.SetText("Scavenge for : " + hours + "h" + minutes);
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
        backPanel.SetActive(false);
    }

    public void ScavengingButtonClicked()
    {
        Scavenge();
        ExitButtonClicked();
    }

    void Scavenge()
    {
        GameController script = GameController.GetComponent<GameController>();
        script.UpdateGameClock(scavengeTime);
    }
}
