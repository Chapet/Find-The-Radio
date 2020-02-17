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
        
        if (this.item.isEatable())
        {
            Debug.Log("This item is eatable");
            Eatable eatItem = (Eatable) this.item;
            
            if (eatItem.haveHungerValue()) ;
            {
                //TODO:action
            }
            
            if (eatItem.haveThirstValue()) ;
            {
                //TODO:action
            }
            
            if (eatItem.haveHealthValue())
            {
                //TODO:action
            }
            
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