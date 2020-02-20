using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class is use by every itemSlot GameObject prefab  to show the item into an itemslot
 */

public class UiItemSlot : MonoBehaviour
{
    Item item;
    [SerializeField] private GameObject background;
    public Image image;
    private bool isItemInfoEnable = false;
    
    /**
     * Enable the itemSlot with item
     */
    public void SetItem(Item item)
    {
        this.item = item;
        image.sprite = item.GetSprite();
        gameObject.SetActive(true);

    }

    /**
     * disable the itemSlot from the inventory and clear variables
     */
    private void ClearSlot()
    {
        item = null;
        image.sprite = null;
        gameObject.SetActive(false);
    }

    /**
     * this function is called when we press an item
     */
    public void OnClickItem()
    {
        if (!isItemInfoEnable)
        {
            GameObject.Find("InventoryPanel/InfoItemPanel");
            isItemInfoEnable = true;
        }

        Debug.Log("Click");
        
        
        GameObject itemInfoPanel = GameObject.Find("InventoryPanel/InfoItemPanel");
        ShowItemProperty showItemProperty = itemInfoPanel.GetComponent<ShowItemProperty>();
        showItemProperty.ShowItem(this.item);

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