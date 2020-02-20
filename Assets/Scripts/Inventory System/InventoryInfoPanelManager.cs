using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemProperty : MonoBehaviour
{
    
    [SerializeField] [Tooltip("gameObject that contain the name of the item selected")]private TextMeshProUGUI itemName;
    [SerializeField][Tooltip("gameObject that contain the item's image")]private Image imageItem;
    [SerializeField] [Tooltip("gameObject that contain the item's description")]private TextMeshProUGUI description;

    private Item itemSelected = null;
    
    public void ShowItem(Item item)
    {
        this.itemSelected = item;

        Debug.Log("Debut Show of"+item.name);
        itemName.SetText(item.name);
        imageItem.sprite = item.GetSprite();
        description.SetText(item.GetDescription());


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
