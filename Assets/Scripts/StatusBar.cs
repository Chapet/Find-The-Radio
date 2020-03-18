using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private bool managedByController = true;
    //private PlayerController player;
    public SBType Type
    {
        get; private set;
    }
    //[SerializeField] private bool liveUpdate = true;
    //private float timer = 0f;
    
    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        if (slider == null)
        {
            Debug.Log("== Begining of Kaput ==");
            Debug.Log(gameObject.name + " with parent : " + gameObject.transform.parent.gameObject.name);
        }
        else
        {
            Debug.Log("Slider catched!");
        }
        switch(gameObject.name)
        {
            case ("Health Bar"):
                Type = SBType.health;
                break;
            case ("Hunger Bar"):
                Type = SBType.hunger;
                break;
            case ("Thirst Bar"):
                Type = SBType.thirst;
                break;
            case ("Energy Bar"):
                Type = SBType.energy;
                break;
            default:
                Type = SBType.none;
                break;
        }
        if (managedByController)
        {
            StatusBarController.SBController.AddStatusBar(this);
        }

        //player = PlayerController.GetPlayer;
    }

    /*
    void FixedUpdate()
    {
        if (slider == null)
        {
            Start();
        }
        if (liveUpdate && timer < Time.fixedDeltaTime)
        {
            switch (type)
            {
                case (SBType.health):
                    SetValue(player.currentHealth);
                    break;
                case (SBType.hunger):
                    SetValue(player.currentHunger);
                    break;
                case (SBType.thirst):
                    SetValue(player.currentThirst);
                    break;
                case (SBType.energy):
                    SetValue(player.currentEnergy);
                    break;
                default:
                    break;
            }
        }
        timer = (timer + Time.fixedDeltaTime) % (5f * Time.fixedDeltaTime);
    }
    */
    public bool HasBeenRendered()
    {
        return slider != null;
    }


    public void SetValue(int value) {
        if (slider == null)
        {
            Debug.Log("SB slider null pointer => SB type : " + Type + " with parent : " + transform.parent.gameObject.name);
            Start();
        }
        slider.value = value;
    }

    public int GetValue()
    {
        if (slider == null)
        {
            Debug.Log("SB slider null pointer => SB type : " + Type + " with parent : " + transform.parent.gameObject.name);
            Start();
        }
        return (int) (slider.value);
    }

    public void SetMaxValue(int maxValue)
    {
        if (slider == null)
        {
            Debug.Log("SB slider null pointer => SB type : " + Type + " with parent : " + transform.parent.gameObject.name);
            Start();
        }
        if (slider==null)
        {
            
        }
        slider.maxValue = maxValue;
    }

    public void addValue(int value)
    {
        if (slider == null)
        {
            Debug.Log("SB slider null pointer => SB type : " + Type + " with parent : " + transform.parent.gameObject.name);
            Start();
        }
        float newValue = slider.value + value;
        if (value > 0)
        {
            slider.value = Mathf.Min(100, newValue);
        }
        else
        {
            slider.value = Mathf.Max(0, newValue);
        }
        
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
