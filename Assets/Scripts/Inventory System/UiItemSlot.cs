using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItemSlot : MonoBehaviour
{
    Item item;
    [SerializeField] private GameObject background; 
    public bool isEnable = false;
    public Image image;
    
    /**
     * Enable the itemSlot with item
     */
    public void SetItem(Item item)
    {
        this.item = item;
        image.sprite = item.GetSprite();
        gameObject.SetActive(true);
        isEnable = true;

    }

    /**
     * disable the itemSlot from the inventory and clear variables
     */
    private void ClearSlot()
    {
        isEnable = false;
        item = null;
        image.sprite = null;
        gameObject.SetActive(false);
    }

    /**
     * this function is called when we press an item
     */
    public void OnClickItem()
    {
        Debug.Log("Click");
        
        GameObject itemInfoPanel = GameObject.Find("InfoItemPanel");
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