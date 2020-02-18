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

    public void ShowItem(Item item)
    {
        Debug.Log("Debut Show of"+item.name);
        itemName.SetText(item.name);
        imageItem.sprite = item.GetSprite();

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
}
