using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabManager : MonoBehaviour
{
    public InventoryManager inventory;
    public InventoryController inventoryController;

    public GameObject headerPanel;
    public GameObject contentPanel;
    public GameObject scrollView;

    public Button foodDrinkTab;
    public Button gearTab;
    public Button medsTab;
    public Button ressourcesTab;

    private TMP_Text foodDrinkTabText;
    private TMP_Text gearTabText;
    private TMP_Text medsTabText;
    private TMP_Text ressourcesTabText;

    private Vector2 unselectedAnchors;
    private Vector2 selectedAnchors = new Vector2(215f, 5f);
    

    public float fadeDuration = 0.3f;

    public Color brightColor;
    public Color darkColor;

    Tab whichTabIsActive = Tab.None;

    void Awake()
    {
        foodDrinkTab.GetComponent<Image>().color = darkColor;
        gearTab.GetComponent<Image>().color = darkColor;
        medsTab.GetComponent<Image>().color = darkColor;
        ressourcesTab.GetComponent<Image>().color = darkColor;

        foodDrinkTab.onClick.AddListener(delegate { TabBtnListener(Tab.FoodDrink); });
        gearTab.onClick.AddListener(delegate { TabBtnListener(Tab.Gear); });
        medsTab.onClick.AddListener(delegate { TabBtnListener(Tab.Meds); });
        ressourcesTab.onClick.AddListener(delegate { TabBtnListener(Tab.Ressources); });

        foodDrinkTabText = foodDrinkTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        gearTabText = gearTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        medsTabText = medsTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        ressourcesTabText = ressourcesTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        contentPanel.GetComponent<Image>().color = darkColor;

        unselectedAnchors = foodDrinkTab.GetComponent<RectTransform>().sizeDelta;
    }

    public void TabBtnListener(Tab which)
    {
        
        if (which != whichTabIsActive)
        {
            Debug.Log("Disabling current tab ...");
            if (contentPanel.GetComponent<Image>().color == brightColor)
            {
                StartCoroutine(ColorFade(brightColor, darkColor, fadeDuration / 3f, contentPanel.GetComponent<Image>()));
            }

            foreach (Transform child in headerPanel.transform)
            {
                GameObject btn = child.gameObject;
                Image img = btn.GetComponent<Image>();
                if (img.color == brightColor)
                {
                    StartCoroutine(ColorFade(brightColor, darkColor, 0.1f, img));
                    btn.GetComponent<RectTransform>().sizeDelta = unselectedAnchors;
                }
            }
        }

        switch (which)
        {
            case Tab.FoodDrink:
                if (whichTabIsActive != Tab.FoodDrink)
                {
                    scrollView.GetComponent<InventoryController>().Clear();
                    Debug.Log("Food & Drink Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, foodDrinkTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    foodDrinkTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                    inventoryController.Show(inventory.GetItems(ItemType.Food));
                    whichTabIsActive = Tab.FoodDrink;
                }
                else
                {
                    Debug.Log("Already in the right tab :-)");
                }
                break;
            case Tab.Gear:
                if (whichTabIsActive != Tab.Gear)
                {
                    scrollView.GetComponent<InventoryController>().Clear();
                    Debug.Log("Gear Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, gearTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    gearTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                    inventoryController.Show(inventory.GetItems(ItemType.Gear));
                    whichTabIsActive = Tab.Gear;
                }
                else
                {
                    Debug.Log("Already in the right tab :-)");
                }
                break;
            case Tab.Meds:
                if (whichTabIsActive != Tab.Meds)
                {
                    scrollView.GetComponent<InventoryController>().Clear();
                    Debug.Log("Meds Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, medsTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    medsTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                    inventoryController.Show(inventory.GetItems(ItemType.Heal));
                    whichTabIsActive = Tab.Meds;
                }
                break;
            case Tab.Ressources:
                if (whichTabIsActive != Tab.Ressources)
                {
                    scrollView.GetComponent<InventoryController>().Clear();
                    Debug.Log("Ressources Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, ressourcesTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    ressourcesTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                    inventoryController.Show(inventory.GetItems(ItemType.Usable));
                    whichTabIsActive = Tab.Ressources;
                }
                else
                {
                    Debug.Log("Already in the right tab :-)");
                }
                break;
            default:
                Debug.Log("Default case in the switch");
                break;
        }
    }

    void OnDisable()
    {
        //Debug.Log("PrintOnDisable: script was disabled");
        scrollView.SetActive(false);
    }

    void OnEnable()
    {
        //Debug.Log("PrintOnEnable: script was enabled");
        scrollView.SetActive(false);
    }

    public void CloseTabs()
    {
        inventoryController.Clear();
        for (int i = 0; i < 4; i++)
        {
            GameObject btn = headerPanel.transform.GetChild(i).gameObject;
            Image img = btn.GetComponent<Image>();
            if (img.color == brightColor)
            {
                StartCoroutine(ColorFade(brightColor, darkColor, 0.001f, img));
                //btn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = brightColor;
                btn.GetComponent<RectTransform>().sizeDelta = unselectedAnchors;
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
        FoodDrink,
        Gear,
        Meds,
        Ressources
    }
}
