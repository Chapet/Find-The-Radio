using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //**********    BUTTON TO DISABLE INVENTORY     ****************
    
    public void DisableInventory()
    {
        Debug.Log("Disable button");
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    //************    BUTTON USE ITEM    ********************
    public ShowItemProperty itemPropertyPanel;
    public GameController gameController;

    public void OnClickUseItem()
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
    
}
