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

    public GameObject headerPanel;
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
        StartCoroutine(DelayedStartTab(5f));
    }

    private IEnumerator DelayedStartTab(float delay)
    {
        yield return new WaitForSeconds(delay / 1000f);
        TabSwitcher(Tab.FoodAndDrink, foodDrinkTog);
    }

    public void TabSwitcher(Tab which, Toggle tog = null)
    {
        if (tog == null)
        {
            tog = togs[(int)which];
        }
        if (tog.isOn)// && tabActive != which)
        {
            List<Item> list = new List<Item>();
            switch (which)
            {
                case Tab.FoodAndDrink:
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, tog.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));

                    tog.GetComponent<RectTransform>().offsetMax += new Vector2(20, 0);
                    tog.GetComponent<RectTransform>().offsetMin += new Vector2(-5, -5);

                    //Getting food AND drinks
                    list = inventory.GetItems(Consumable.ItemType.Food, Consumable.ItemType.Drink);
                    break;
                case Tab.Gear:
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, tog.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));

                    tog.GetComponent<RectTransform>().offsetMax += new Vector2(20, 5);
                    tog.GetComponent<RectTransform>().offsetMin += new Vector2(-5, -5);

                    //Getting all gears    
                    list = inventory.GetItems(typeof(Gear));

                    break;
                case Tab.Meds:
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, tog.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));

                    tog.GetComponent<RectTransform>().offsetMax += new Vector2(20, 5);
                    tog.GetComponent<RectTransform>().offsetMin += new Vector2(-5, -5);

                    list = inventory.GetItems(Consumable.ItemType.Meds);

                    break;
                case Tab.Misc:
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, tog.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));

                    tog.GetComponent<RectTransform>().offsetMax += new Vector2(20, 5);
                    tog.GetComponent<RectTransform>().offsetMin += new Vector2(-5, -5);

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
        else if(!tog.isOn)
        {
            if (contentPanel.GetComponent<Image>().color == brightColor)
            {
                StartCoroutine(ColorFade(brightColor, darkColor, fadeDuration / 3f, contentPanel.GetComponent<Image>()));
            }

            foreach (Transform child in headerPanel.transform)
            {
                GameObject toggle = child.gameObject;
                Image img = toggle.GetComponent<Image>();
                if (img.color == brightColor)
                {
                    StartCoroutine(ColorFade(brightColor, darkColor, 0.1f, img));
                    toggle.GetComponent<RectTransform>().offsetMax += new Vector2(-20, 0);
                    if (toggle.name == foodDrinkTog.name)
                    {
                        toggle.GetComponent<RectTransform>().offsetMin += new Vector2(5, 5);
                    }
                    else
                    {
                        toggle.GetComponent<RectTransform>().offsetMin += new Vector2(5, 5);
                        toggle.GetComponent<RectTransform>().offsetMax += new Vector2(0, -5);
                    }
                }
            }
            inventoryController.Clear();
        }
        else
        {
            Debug.Log("OK");
        }
    }

    public void CloseTabs()
    {
        inventoryController.Clear();
        for (int i = 0; i < 4; i++)
        {
            GameObject toggle = headerPanel.transform.GetChild(i).gameObject;
            Image img = toggle.GetComponent<Image>();
            if (img.color == brightColor)
            {
                StartCoroutine(ColorFade(brightColor, darkColor, 0.001f, img));
                toggle.GetComponent<RectTransform>().offsetMax += new Vector2(-20, 0);
                if (toggle.name == foodDrinkTog.name)
                {
                    Debug.Log("OK1");
                    toggle.GetComponent<RectTransform>().offsetMin += new Vector2(5, 5);
                }
                else
                {
                    Debug.Log("OK2");
                    toggle.GetComponent<RectTransform>().offsetMin += new Vector2(5, 5);
                    toggle.GetComponent<RectTransform>().offsetMax += new Vector2(0, -5);
                }
            }
        }
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
