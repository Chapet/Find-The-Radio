using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    
    [Header("Game Proprity")]
    public PlayerController playerController;
    

    [Header("inventory Global gameobjects")]
    [SerializeField] 
    [Tooltip("Contain the root gameObject of the inventory")]private GameObject inventoryRoot; 
    
    [Space]
    public InventoryInfoPanelManager inventoryInfoPanelManager;

    public UiInventoryManager uiInventoryManager;
   
    
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
    //[UnityEditor.MenuItem("Tools/inventory/Enable")]
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
        Item selectedItem = inventoryInfoPanelManager.GetSelectedItem();
        //TODO: can't be user => popo up else:
        Debug.Log("Use Item");
        
        if (selectedItem.IsUsable())
        {
            Debug.Log("The item is eatable");
            Eatable eat = (Eatable) selectedItem;
            if (eat.HaveHungerValue())
            {
                playerController.IncrementHunger(eat.GetHunger());
            }

            if (eat.HaveThirstValue())
            {
                playerController.InscrementThirst(eat.GetThirst());
            }

            if (eat.HaveHealthValue())
            {
                playerController.InscrementHealth(eat.GetHealth());
            }

            if (eat.HaveFatigueValue())
            {
                playerController.InscrementEnergy(eat.GetEnergy());
            }
            
            
            
        }

        playerController.inventory.RemoveItem(selectedItem);
        uiInventoryManager.updatePanel();
        inventoryInfoPanelManager.cleanPanel();
        Debug.Log("Item has been removed");
    }
    
}
