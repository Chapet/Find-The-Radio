using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryInfoPanelManager : MonoBehaviour
{
    
    [SerializeField] [Tooltip("gameObject that contain the name of the item selected")]private TextMeshProUGUI itemName;
    [SerializeField][Tooltip("gameObject that contain the item's image")]private GameObject imageItem;
    [SerializeField] [Tooltip("gameObject that contain the item's description")]private TextMeshProUGUI description;

    [Header("Properties")] 
    [SerializeField] [Tooltip("gameObject that contain all properties's gameobjects")] private GameObject rootProperties;
    [SerializeField] [Tooltip("Text that shows health property")] private TextMeshProUGUI healthTxt;
    [SerializeField] [Tooltip("Text that shows hunger property")]  private TextMeshProUGUI hungerTxt;
    [SerializeField] [Tooltip("Text that shows thirst property")] private TextMeshProUGUI thirstTxt;
    [SerializeField][Tooltip("Text that shows ernergy property")] private TextMeshProUGUI energyTxt;
    
    [Space]
    [SerializeField] [Tooltip("the use item button")]
    private GameObject useButton;

    [HideInInspector]public Item itemSelected = null;
    
    public void ShowItem(Item item)
    {
        this.itemSelected = item;
        useButton.SetActive(true);

        //Debug.Log("Debut Show of"+item.name);
        itemName.SetText(item.name);
        imageItem.SetActive(true);
        imageItem.GetComponent<Image>().sprite = item.GetSprite();
        description.SetText(item.GetDescription());

        if (item.IsUsable())
        {
            Eatable eat = (Eatable) item;
            rootProperties.SetActive((true));
            healthTxt.SetText(PropertyToString(Item.MIN_HEALTH_VALUE,Item.MAX_HEALTH_VALUE,eat.GetHealth()));
            hungerTxt.SetText(PropertyToString(Item.MIN_HUNGER_VALUE,Item.MAX_HUNGER_VALUE,eat.GetHunger()));
            thirstTxt.SetText(PropertyToString(Item.MIN_THIST_VALUE,Item.MAX_THIST_VALUE,eat.GetThirst()));
            energyTxt.SetText(PropertyToString(Item.MIN_ENERGY_VALUE,Item.MAX_ENERGY_VALUE,eat.GetEnergy()));
            
        }



    }

    /**
     * convert the value to a string:
     * if value<=0.25*MaxValue => +
     * elseif value <=0.5*MaxValue=> ++
     * elseif value <=0.75*MaxValue=> +++
     * elseif value <=1*MaxValue=> ++++
     *
     * same for negative number -/--/---/----
     *
     * Maxvalue is the maximum value that can have an item of this type
     *
     * @Pre: min has to be negative
     *       max has to be positive
     */
    private string PropertyToString(int minValue,int maxValue, int value)
    {
        Debug.Assert(minValue <= 0);
        Debug.Assert(maxValue >= 0);
        Debug.Assert(value <= maxValue);
        Debug.Assert(value >= minValue);
        
        int max;
        if (value == 0)
        {
            return "";
        }
        else if (value > 0)
        {
            max = maxValue;

            if (value <= 0.25 * max)
            {
                return "+";
            }
            else if (value <= 0.5 * max)
            {
                return "++";
            }
            else if (value <= 0.75 * max)
            {
                return "+++";
            }
            else
            {
                return "++++";
            }
        }
        else
        {
            max = Mathf.Abs(minValue);
            value = Mathf.Abs(value);
            if (value <= 0.25 * max)
            {
                return "-";
            }
            else if (value <= 0.5 * max)
            {
                return "--";
            }
            else if (value <= 0.75 * max)
            {
                return "---";
            }
            else
            {
                return "----";
            }
        }
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
        
        rootProperties.SetActive(false);
        
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
