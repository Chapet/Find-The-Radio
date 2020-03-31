﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public MenuController menuController;

    public Toggle m_Toggle_30;
    public Toggle m_Toggle_45;
    public Toggle m_Toggle_60;
    public Toggle m_Toggle_90;

    // Start is called before the first frame update
    void Start()
    {
        m_Toggle_30.onValueChanged.AddListener(delegate {
            ToggleSelected(m_Toggle_30, 30);
        });
        m_Toggle_45.onValueChanged.AddListener(delegate {
            ToggleSelected(m_Toggle_45, 45);
        });
        m_Toggle_60.onValueChanged.AddListener(delegate {
            ToggleSelected(m_Toggle_60, 60);
        });
        m_Toggle_90.onValueChanged.AddListener(delegate {
            ToggleSelected(m_Toggle_90, 90);
        });
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }

    void ToggleSelected(Toggle changed, int fps)
    {
        if(changed.isOn)
        {
            Debug.Log("New value : " + fps + " FPS");
            GameController.framerate = fps;
        }
        else
        {
            Debug.Log("Old value : " + fps + " FPS");
        }
    }
}
