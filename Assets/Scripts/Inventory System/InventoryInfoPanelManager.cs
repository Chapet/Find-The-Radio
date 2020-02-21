using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoPanelManager : MonoBehaviour
{
    
    [SerializeField] [Tooltip("gameObject that contain the name of the item selected")]private TextMeshProUGUI itemName;
    [SerializeField][Tooltip("gameObject that contain the item's image")]private GameObject imageItem;
    [SerializeField] [Tooltip("gameObject that contain the item's description")]private TextMeshProUGUI description;

    [SerializeField] [Tooltip("the use item button")]
    private GameObject useButton;

    public Item itemSelected = null;
    
    public void ShowItem(Item item)
    {
        this.itemSelected = item;
        useButton.SetActive(true);

        Debug.Log("Debut Show of"+item.name);
        itemName.SetText(item.name);
        imageItem.SetActive(true);
        imageItem.GetComponent<Image>().sprite = item.GetSprite();
        description.SetText(item.GetDescription());
        
    }
    
    

    public void cleanPanel()
    {
        if (!haveItem())
        {
            Debug.Log("No item is selected");
            return;
        }
        
        itemName.SetText("Select item");
        imageItem.SetActive(false);
        description.SetText("");
        useButton.SetActive(false);
        
    }

    public bool haveItem()
    {
        return itemSelected != null;
    }
    



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetSelectedItem()
    {
        return itemSelected;
    }
}
