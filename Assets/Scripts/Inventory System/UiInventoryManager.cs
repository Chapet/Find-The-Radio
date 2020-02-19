using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryButton
{
    All,Food,Drink,Medication,Ressource
}

public class UiInventoryManager : MonoBehaviour
{

    [SerializeField]
    private Inventory inventory;

    [SerializeField] 
    private GameObject itemPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        //gridLayout=GetComponent<GridLayout>();
        //inventory = Resources.Load("Player/Player Inventory") as Inventory;

        //itemSlots = GetComponentsInChildren<UI_ItemSlot>();
        
            
        LoadFoodUiItems();
        
        
    }
    

    /**
     * load one item in the GridLayer
     */
    private void LoadUiItem(Item item)
    {
        GameObject obj = Instantiate(itemPrefab);
        obj.transform.parent = gameObject.transform;
        UiItemSlot slot = obj.GetComponent<UiItemSlot>();
        slot.SetItem(item);
    }

    public void LoadAllUiItems()
    {
        ClearUiInventory();
        foreach (Item i in inventory.itemsList)
        {
            LoadUiItem(i);
            Debug.Log("Item Name: "+i.name);
        }
    }

    public void LoadFoodUiItems()
    {
        ClearUiInventory();
        foreach (Item i in inventory.itemsList)
        {
            if (i.itemType.Contains(ItemType.HungerValue))
            {
                LoadUiItem(i);
            }
        }
    }

    public void LoadDrinkItems()
    {
        ClearUiInventory();
        foreach (Item i in inventory.itemsList)
        {
            if (i.itemType.Contains(ItemType.ThistValue))
            {
                LoadUiItem(i);
            }
        }
    }
    
    public void LoadMedicationItems()
    {
        ClearUiInventory();
        foreach (Item i in inventory.itemsList)
        {
            if (i.itemType.Contains(ItemType.HealthValue))
            {
                LoadUiItem(i);
            }
        }
    }
    
    public void LoadRessourcesItems()
    {
        ClearUiInventory();
        foreach (Item i in inventory.itemsList)
        {
            if (i.itemType.Contains(ItemType.Craftable)||i.itemType.Contains(ItemType.Gear)||i.itemType.Contains(ItemType.Scavengeable))
            {
                LoadUiItem(i);
            }
        }
    }

    private void ClearUiInventory()
    {
        int n = gameObject.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            GameObject item = gameObject.transform.GetChild(i).gameObject;
            Destroy(item);
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}