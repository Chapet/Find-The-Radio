using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    
    [Header("Game Proprity")]
    public GameController playerController;
    

    [Header("Inventory Global gameobjects")]
    [SerializeField] 
    [Tooltip("Contain the root gameObject of the Inventory")]private GameObject inventoryRoot; 
    
    [Space]
    public InventoryInfoPanelManager itemPropertyPanel;
   
    
    
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
        for (int i = 0; i < inventoryRoot.transform.childCount; i++)
        {

            inventoryRoot.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    //************    ENABLE INVENTORY    ********************
    //[UnityEditor.MenuItem("Tools/Inventory/Enable")]
    public void EnableInventory()
    {
        if (inventoryRoot == null)
        {
            Debug.LogError("InventoryRoot variable == null");
            return;
        }
        
        //eanable all gameobject
        for (int i = 0; i < inventoryRoot.transform.childCount; i++)
        {
            inventoryRoot.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    
    
    //************    BUTTON USE ITEM    ********************
    
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
