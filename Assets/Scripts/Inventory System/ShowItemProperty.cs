using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemProperty : MonoBehaviour
{
    
    [SerializeField]private TextMeshProUGUI itemName;
    [SerializeField]private Image imageItem;
    [SerializeField] private TextMeshProUGUI description;

    private Item itemSelected = null;
    
    public void ShowItem(Item item)
    {
        this.itemSelected = item;

        Debug.Log("Debut Show of"+item.name);
        itemName.SetText(item.name);
        imageItem.sprite = item.GetSprite();
        description.SetText(item.GetDescription());


    }

    public ShowItemProperty()
    {
       
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
