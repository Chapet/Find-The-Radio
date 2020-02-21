using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is no longer in use 
 */
public class UseItemButton : MonoBehaviour
{

    [SerializeField] private InventoryInfoPanelManager itemPropertyPanel;
    [SerializeField] private GameController gameController;

    public void OnClick()
    {
        Item selectedItem = itemPropertyPanel.GetSelectedItem();
        if (selectedItem.IsEatable())
        {
            Eatable item = (Eatable) selectedItem;
            if (item.HaveHungerValue())
            {
                //gameController.hungerBar.addValue(item.GetHunger());
            }

            if (item.HaveThirstValue())
            {
                int v = item.GetThirst();
                //gameController.thirstyBar.addValue(item.GetThirst());
            }

            if (item.HaveHealthValue())
            {
                //gameController.healthBar.addValue(item.GetHealth());
            }

            if (item.HaveFatigueValue())
            {
                //gameController.energyBar.addValue(item.GetFatigue());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
