using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    public ClockDigits digits;

    public Image tenHours;
    public Image unitHours;

    public Image tenMinutes;
    public Image unitMinutes;

    private bool init = false;

    public void UpdateClock(int hours, int minutes)
    {
        if (!init)
        {
            digits.Digits = new Sprite[] { digits.zero, digits.one, digits.two, digits.three, digits.four, digits.five, digits.six, digits.seven, digits.eight, digits.nine };
            init = true;
        }
        int ten_hours = hours / 10;
        int unit_hours = hours % 10;

        int ten_minutes = minutes / 10;
        int unit_minutes = minutes % 10;
        //Debug.Log("ClockController : " + ten_hours + " " + unit_hours + " " + ten_minutes + " " + unit_minutes);
        UpdateUI(ten_hours, unit_hours, ten_minutes, unit_minutes);
    }

    private void UpdateUI(int h_10, int h_01, int m_10, int m_01)
    {
        tenHours.sprite = digits.Digits[h_10];
        unitHours.sprite = digits.Digits[h_01];

        tenMinutes.sprite = digits.Digits[m_10];
        unitMinutes.sprite = digits.Digits[m_01];
    }
}
