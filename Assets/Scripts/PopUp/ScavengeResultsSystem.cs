using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScavengeResultsSystem : MonoBehaviour
{
    
    public  MenuController menuController;
    public GameObject gameObject;
    public GameObject backPanel;
    public GameObject slotPrefab;
    public GameObject itemContent;
    private List<GameObject> inventorySlots= new List<GameObject>();
    public InventoryExample inventoryExample;
    public TextMeshProUGUI message;

    public StatusBar healthBar;
    public StatusBar hungerBar;
    public StatusBar thirstBar;
    public StatusBar energyBar;

    private float healthGoal;
    private float hungerGoal;
    private float thirstGoal;
    private float energyGoal;
    

    public void Awake()
    {
        healthGoal = healthBar.slider.value;
        hungerGoal = hungerBar.slider.value;
        thirstGoal = thirstBar.slider.value;
        energyGoal = energyBar.slider.value;
    }

    public void PopResult(List<Item> items,((int old, int now) health,(int old, int now) hunger,(int old, int now) thirst,(int old, int now) energy) statusBarUpdate)
    {
        
        clearInventorySlot();
        if (items.Count == 0)
        {
            message.SetText("You didn't find anything");
        }

        //add show all Item
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(slotPrefab);
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.AddItem(item);
            obj.transform.SetParent(itemContent.transform, false);
            
            inventorySlots.Add(obj);
        }
        
        menuController.OpenMenu(gameObject);
        
        //=============    ANIMATION    ================ 
        healthBar.SetValue(statusBarUpdate.health.old);
        healthGoal = statusBarUpdate.health.now;
        
        hungerBar.SetValue(statusBarUpdate.hunger.old);
        hungerGoal = statusBarUpdate.hunger.now;
        
        thirstBar.SetValue(statusBarUpdate.thirst.old);
        thirstGoal = statusBarUpdate.thirst.now;
        
        energyBar.SetValue(statusBarUpdate.energy.old);
        energyGoal = statusBarUpdate.energy.now;

    }

    /**
     * increment est le nombre que l'on va rajouter au slider pour atteindre la valeur finale
     * inc est toujours possitif
     */
    private void incrementSlider(Slider slider, float finalValue, int inc)
    {
        if (slider.value < finalValue)
        {
            healthBar.addValue(inc);
        }else if (slider.value > finalValue)
        {
            healthBar.addValue(-1*inc);
        }
    }

    public void Update()
    {
        //===========    HEALTH    =============
        incrementSlider(healthBar.slider,healthGoal,1);
        
        //===========    HUNGER    =============
        incrementSlider(hungerBar.slider,hungerGoal,1);
        
        //===========    THIRST    =============
        incrementSlider(thirstBar.slider,thirstGoal,1);
        
        //===========    ENERGY    =============
        incrementSlider(energyBar.slider,energyGoal,1);
    }
    

    private void clearInventorySlot()
    {
        /*foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }*/
        for (int i = 0; i < itemContent.transform.childCount; i++)
        {
            Destroy(itemContent.transform.GetChild(i).gameObject);
        }
        inventorySlots.Clear();
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(this.gameObject);
    }
    
    
}
