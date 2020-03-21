using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    
    private Slider slider;
    [SerializeField] private bool managedByController = true;
    [SerializeField] private bool animated = true;
    private bool initialized = false;
    private bool valueHasChanged;
    private int newValue;

    public StatType Type
    {
        get; private set;
    }
    //[SerializeField] private bool liveUpdate = true;
    //private float timer = 0f;

    private void Start()
    {
        Init();
        SetMaxValue();
        initialized = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if (initialized && managedByController)
        {
            slider.value = PlayerController.Player.currentStats[(int) Type];
            newValue = PlayerController.Player.currentStats[(int)Type];
        }
    }

    private void Init()
    {
        slider = gameObject.GetComponent<Slider>();

        switch (gameObject.name)
        {
            case ("Health Bar"):
                Type = StatType.Health;
                break;
            case ("Hunger Bar"):
                Type = StatType.Hunger;
                break;
            case ("Thirst Bar"):
                Type = StatType.Thirst;
                break;
            case ("Energy Bar"):
                Type = StatType.Energy;
                break;
            default:
                Type = StatType.None;
                break;
        }

        if (managedByController)
        {
            StatusBarController.SBController.AddStatusBar(this);
        }
    }

    private void SetMaxValue()
    {
        slider.maxValue = PlayerController.maxStats[(int) Type];
    }

    public void FixedUpdate()
    {
        if(animated && valueHasChanged)
        {
            incrementSlider(1);
        }
    }

    private void incrementSlider(int step)
    {
        if (GetValue() < newValue)
        {
            slider.value = Mathf.Min(100, slider.value + step);
        }
        else if (GetValue() > newValue)
        {
            slider.value = Mathf.Max(0, slider.value - step);
        }
        else
        {
            valueHasChanged = false;
        }
    }

    public bool HasBeenRendered()
    {
        return slider != null;
    }


    public void SetValue(int value) {
        newValue = value;
        valueHasChanged = true;

        if (!animated)
        {
            slider.value = newValue;
        }
    }

    public int GetValue()
    {
        return (int) (slider.value);
    }

    //public void SetMaxValue(int maxValue)
    //{
    //    slider.maxValue = maxValue;
    //}

    public void addValue(int value)
    {
        if (value > 0)
        {
            newValue = (int) Mathf.Min(100, slider.value + value);
        }
        else
        {
            newValue = (int) Mathf.Max(0, slider.value + value);
        }
        valueHasChanged = true;

        if (!animated)
        {
            slider.value = newValue;
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
