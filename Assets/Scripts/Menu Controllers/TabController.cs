using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabController : MonoBehaviour
{
    private InventoryManager inventory;
    public InventoryController inventoryController;

    public GameObject tabsPanel;
    public GameObject contentPanel;
    public GameObject scrollView;

    public Toggle foodDrinkTog;
    public Toggle gearTog;
    public Toggle medsTog;
    public Toggle miscTog;

    private Toggle[] togs;

    private TMP_Text foodDrinkTabText;
    private TMP_Text gearTabText;
    private TMP_Text medsTabText;
    private TMP_Text ressourcesTabText;

    public float fadeDuration = 0.3f;

    public Color brightColor;
    public Color darkColor;

    private Tab prevActiveTab = Tab.None;

    private void Awake()
    {

        togs = new Toggle[] { null, foodDrinkTog, gearTog, medsTog, miscTog };

        foodDrinkTog.GetComponent<Image>().color = darkColor;
        gearTog.GetComponent<Image>().color = darkColor;
        medsTog.GetComponent<Image>().color = darkColor;
        miscTog.GetComponent<Image>().color = darkColor;

        foodDrinkTog.onValueChanged.AddListener(delegate { TabSwitcher(Tab.FoodAndDrink); });
        gearTog.onValueChanged.AddListener(delegate { TabSwitcher(Tab.Gear); });
        medsTog.onValueChanged.AddListener(delegate { TabSwitcher(Tab.Meds); });
        miscTog.onValueChanged.AddListener(delegate { TabSwitcher(Tab.Misc); });

        foodDrinkTabText = foodDrinkTog.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        gearTabText = gearTog.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        medsTabText = medsTog.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        ressourcesTabText = miscTog.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        contentPanel.GetComponent<Image>().color = darkColor;

    }

    private void Start()
    {
        inventory = InventoryManager.Inventory;
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedStartTab(16f / 1000f));
    }

    private IEnumerator DelayedStartTab(float delay)
    {
        yield return new WaitForSeconds(delay);
        TabSwitcher(Tab.FoodAndDrink);
    }

    public void TabSwitcher(Tab which, Toggle tog = null)
    {
        if (tog == null)
        {
            tog = togs[(int)which];
        }

        if (tog.isOn && prevActiveTab != which)
        {
            SelectTab(which);
        }

        if (tog.isOn)
        {
            List<Item> list = new List<Item>();
            switch (which)
            {
                case Tab.FoodAndDrink:

                    //Getting food AND drinks
                    list = inventory.GetItems(Consumable.ItemType.Food, Consumable.ItemType.Drink);
                    break;
                case Tab.Gear:

                    //Getting all gears    
                    list = inventory.GetItems(typeof(Gear));

                    break;
                case Tab.Meds:

                    list = inventory.GetItems(Consumable.ItemType.Meds);

                    break;
                case Tab.Misc:

                    list = inventory.GetItems(typeof(Resource), typeof(Junk));

                    break;
                default:
                    Debug.Log("Default case in the switch");
                    break;
            }


            if (which == Tab.Gear)
            {
                inventoryController.GearTabSelected();
            }
            else
            {
                inventoryController.StandardTabSelected();
            }

            inventoryController.Clear();
            inventoryController.Show(list);
        }
        else
        {
            UnselectTabs();
        }

        prevActiveTab = which;
    }

    private void UnselectTabs()
    {
        if (contentPanel.GetComponent<Image>().color == brightColor)
        {
            StartCoroutine(ColorFade(brightColor, darkColor, fadeDuration / 3f, contentPanel.GetComponent<Image>()));
        }
        foreach (Transform child in tabsPanel.transform)
        {
            GameObject toggle = child.gameObject;
            Image img = toggle.GetComponent<Image>();
            if (img.color == brightColor)
            {
                StartCoroutine(ColorFade(brightColor, darkColor, 0.1f, img));
                toggle.GetComponent<RectTransform>().offsetMax += new Vector2(-20, 0);
                toggle.GetComponent<RectTransform>().offsetMin += new Vector2(5, 5);
                if (toggle.name != foodDrinkTog.name)
                {
                    toggle.GetComponent<RectTransform>().offsetMax += new Vector2(0, -5);
                }
            }
        }
        inventoryController.Clear();
    }

    private void SelectTab(Tab which)
    {
        Toggle toggle = togs[(int)which];
        StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, toggle.GetComponent<Image>()));
        StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
        if (which == Tab.FoodAndDrink)
        {
            toggle.GetComponent<RectTransform>().offsetMax += new Vector2(20, 0);
        }
        else
        {
            toggle.GetComponent<RectTransform>().offsetMax += new Vector2(20, 5);
        }
        toggle.GetComponent<RectTransform>().offsetMin += new Vector2(-5, -5);

    }

    public void CloseTabs()
    {
        foodDrinkTog.isOn = true;
        gearTog.isOn = false;
        medsTog.isOn = false;
        miscTog.isOn = false;
        foodDrinkTog.GetComponent<RectTransform>().offsetMax += new Vector2(-20, 0);
        foodDrinkTog.GetComponent<RectTransform>().offsetMin += new Vector2(5, 5);
        inventoryController.Clear();
        prevActiveTab = Tab.None;
        StartCoroutine(ColorFade(brightColor, darkColor, 0.001f, contentPanel.GetComponent<Image>()));
    }

    private IEnumerator ColorFade(Color start, Color end, float duration, Image btnImage)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            btnImage.color = Color.Lerp(start, end, counter / duration);
            yield return null;
        }
    }

    public enum Tab
    {
        None = 0,
        FoodAndDrink,
        Gear,
        Meds,
        Misc
    }
}
