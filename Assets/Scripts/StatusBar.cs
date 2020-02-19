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

    public void addValue(int value)
    {
        float newValue = slider.value + value;
        if (value > 0)
        {
            slider.value = Mathf.Min(100, slider.value + value);
        }
        else
        {
            slider.value = Mathf.Max(0, slider.value + value);
        }
        
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
