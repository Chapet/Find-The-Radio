using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemButton : MonoBehaviour
{
    public InventoryManager inventoryManager;
    
    public void OnClick()
    {
        inventoryManager.OnClickUseItem();
    }
}
