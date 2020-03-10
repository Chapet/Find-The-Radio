﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabController : MonoBehaviour
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

        foodDrinkTab.onClick.AddListener(delegate { TabBtnListener(Tab.FoodAndDrink); });
        gearTab.onClick.AddListener(delegate { TabBtnListener(Tab.Gear); });
        medsTab.onClick.AddListener(delegate { TabBtnListener(Tab.Meds); });
        ressourcesTab.onClick.AddListener(delegate { TabBtnListener(Tab.Resources); });

        foodDrinkTabText = foodDrinkTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        gearTabText = gearTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        medsTabText = medsTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        ressourcesTabText = ressourcesTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        contentPanel.GetComponent<Image>().color = darkColor;

        unselectedAnchors = foodDrinkTab.GetComponent<RectTransform>().sizeDelta;
    }

    public void TabBtnListener(Tab which)
    {
        //Debug.Log(which);

        if (which != whichTabIsActive)
        {
            if(whichTabIsActive != Tab.None)
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
                inventoryController.Clear();
            }
            List<Item> list = new List<Item>();
            switch (which)
            {
                case Tab.FoodAndDrink:
                    Debug.Log("Food & Drink Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, foodDrinkTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    foodDrinkTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;

                    //Getting food AND drinks
                    list = inventory.GetItems(Consumable.ItemType.Food, Consumable.ItemType.Drink);

                    break;
                case Tab.Gear:
                    Debug.Log("Gear Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, gearTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    gearTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;

                    //Getting all gears    

                    list = inventory.GetItems(typeof(Gear));

                    break;
                case Tab.Meds:
                    Debug.Log("Meds Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, medsTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    medsTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;

                    list = inventory.GetItems(Consumable.ItemType.Meds);

                    break;
                case Tab.Resources:
                    Debug.Log("Ressources Tab");
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, ressourcesTab.GetComponent<Image>()));
                    StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                    ressourcesTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;

                    list = inventory.GetItems(typeof(Resource));

                    break;
                default:
                    Debug.Log("Default case in the switch");
                    break;
            }
            whichTabIsActive = which;
            inventoryController.Show(list);
        }
        else
        {
            Debug.Log("Already in the right tab :-)");
        }

        
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
                btn.GetComponent<RectTransform>().sizeDelta = unselectedAnchors;
            }
        }
        StartCoroutine(ColorFade(brightColor, darkColor, 0.001f, contentPanel.GetComponent<Image>()));
        whichTabIsActive = Tab.None;
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
        Resources
    }
}
