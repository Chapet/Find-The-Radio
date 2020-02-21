using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    
    [Header("Game Proprity")]
    public GameController gameController;
    

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
            Eatable eat = (Eatable) selectedItem;
            if (eat.HaveHungerValue())
            {
                gameController.addHungerValue(eat.GetHunger());
            }

            if (eat.HaveThirstValue())
            {
                int v = eat.GetThirst();
                //gameController.thirstyBar.addValue(item.GetThirst());
            }

            if (eat.HaveHealthValue())
            {
                //gameController.healthBar.addValue(item.GetHealth());
            }

            if (eat.HaveFatigueValue())
            {
                //gameController.energyBar.addValue(item.GetFatigue());
            }
        }
    }
    
}
