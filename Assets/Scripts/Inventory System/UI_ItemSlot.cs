using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    Item item;
    public bool isEnable = false;
    public Image image;
    
    /**
     * Enable the itemSlot with item
     */
    public void setItem(Item item)
    {
        this.item = item;
        image.sprite = item.getSprite();
        gameObject.SetActive(true);
        isEnable = true;

    }

    /**
     * disable the itemSlot from the inventory and clear variables
     */
    private void clearSlot()
    {
        isEnable = false;
        item = null;
        image.sprite = null;
        gameObject.SetActive(false);
    }

    /**
     * this function is called when we press an item
     */
    public void onClickItem()
    {
        Debug.Log("Press "+this.item.name);

        switch (this.item.itemType)
        {
            case ItemType.Drink:
                //TODO: action
                break;
            case ItemType.Food:
                //TODO: action
                break;
            case ItemType.Health:
                //TODO: action
                break;
            default:
                Debug.Log("Item type unknow");
                break;
        }
        
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