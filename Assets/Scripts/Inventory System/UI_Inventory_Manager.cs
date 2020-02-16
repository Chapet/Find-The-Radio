using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory_Manager : MonoBehaviour
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
        
        loadUIInventory();
        
        
    }


    private void loadUIInventory()
    {
        clearUIInventory();
        foreach (Item i in inventory.itemsList)
        {

            GameObject obj = Instantiate(itemPrefab);
            obj.transform.parent = gameObject.transform;
            UI_ItemSlot slot = obj.GetComponent<UI_ItemSlot>();
            slot.setItem(i);
            
            Debug.Log("Item Name: "+i.name);
        }
    }

    private void clearUIInventory()
    {
        int n = gameObject.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            GameObject Item = gameObject.transform.GetChild(i).gameObject;
            Destroy(Item);
        }

        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}