using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{

    public Slider slider;

    public void SetValue(int value) {
        slider.value = value;
    }

    public void SetMaxValue(int maxValue) {
        slider.maxValue = maxValue;
    }
}
