using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryButton
{
    All,Food,Drink,Medication,Ressource
}

/**
 * This class is use by gameObject that contain the UiItem and that have a GridLayer Component
 */

public class UiInventoryManager : MonoBehaviour
{

    
    private Inventory inventory;

    [SerializeField] 
    private GameObject itemPrefab;
    
    private List<GameObject> itemShowed=new List<GameObject>();
    [SerializeField]public PlayerController player;
    [SerializeField] private InventoryMenuButtonListener menuButtonManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //gridLayout=GetComponent<GridLayout>();
        //inventory = Resources.Load("Player/Player inventory") as inventory;

        //itemSlots = GetComponentsInChildren<UI_ItemSlot>();

        inventory = player.inventory;
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
        obj.GetComponent<RectTransform>().sizeDelta=new Vector2(100,100);
        itemShowed.Add(obj);
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
        foreach (GameObject game in itemShowed)
        {
            Destroy(game);
        }
        itemShowed.Clear();
        return;
        
        
        int n = itemShowed.Count;
        for (int i = 0; i < n; i++)
        {
            GameObject item = itemShowed[i];
            Destroy(item);
        }
        itemShowed.Clear();
    }

    /**
     * update the panel that show all items
     * if an item is showed and that isn't not in the inventory then it'll be destroy
     */
    public void updatePanel()
    {
        switch (menuButtonManager.selectedCategory)
        {
            case InventoryCategory.Food:
                LoadFoodUiItems();
                break;
            case InventoryCategory.Drink:
                LoadDrinkItems();
                break;
            case InventoryCategory.Medicalion:
                LoadMedicationItems();
                break;
            case InventoryCategory.Ressource:
                LoadRessourcesItems();
                break;
            default:
                Debug.Log("Inventory button selected does not match with food,drink, mediccation,ressourceà");
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}