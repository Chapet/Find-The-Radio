using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabManager : MonoBehaviour
{
    public GameObject headerPanel;
    public GameObject contentPanel;

    public Button foodTab;
    public Button drinkTab;
    public Button medsTab;
    public Button ressourcesTab;

    private TMP_Text foodTabText;
    private TMP_Text drinkTabText;
    private TMP_Text medsTabText;
    private TMP_Text ressourcesTabText;

    private Vector2 unselectedAnchors;
    private Vector2 selectedAnchors = new Vector2(240f, 5f);
    

    public float fadeDuration = 0.3f;

    public Color brightColor;
    public Color darkColor;

    void Awake()
    {
        foodTab.GetComponent<Image>().color = darkColor;
        drinkTab.GetComponent<Image>().color = darkColor;
        medsTab.GetComponent<Image>().color = darkColor;
        ressourcesTab.GetComponent<Image>().color = darkColor;

        foodTab.onClick.AddListener(delegate { TabBtnListener(Tab.Food); });
        drinkTab.onClick.AddListener(delegate { TabBtnListener(Tab.Drink); });
        medsTab.onClick.AddListener(delegate { TabBtnListener(Tab.Meds); });
        ressourcesTab.onClick.AddListener(delegate { TabBtnListener(Tab.Ressources); });

        foodTabText = foodTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        drinkTabText = drinkTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        medsTabText = medsTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        ressourcesTabText = ressourcesTab.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        /*
        foodTabText.color = brightColor;
        drinkTabText.color = brightColor;
        medsTabText.color = brightColor;
        ressourcesTabText.color = brightColor;
        */

        contentPanel.GetComponent<Image>().color = darkColor;

        unselectedAnchors = foodTab.GetComponent<RectTransform>().sizeDelta;
    }

    public void TabBtnListener(Tab which)
    {
        if (contentPanel.GetComponent<Image>().color == brightColor)
        {
            StartCoroutine(ColorFade(brightColor, darkColor, fadeDuration / 3f, contentPanel.GetComponent<Image>()));
        }
        switch (which)
        {
            case Tab.Food:
                Debug.Log("Food Tab");
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, foodTab.GetComponent<Image>()));
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                foodTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                //foodTabText.color = darkColor;
                break;
            case Tab.Drink:
                Debug.Log("Drink Tab");
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, drinkTab.GetComponent<Image>()));
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                drinkTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                //drinkTabText.color = darkColor;
                break;
            case Tab.Meds:
                Debug.Log("Meds Tab");
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, medsTab.GetComponent<Image>()));
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                medsTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                //medsTabText.color = darkColor;
                break;
            case Tab.Ressources:
                Debug.Log("Ressources Tab");
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, ressourcesTab.GetComponent<Image>()));
                StartCoroutine(ColorFade(darkColor, brightColor, fadeDuration, contentPanel.GetComponent<Image>()));
                ressourcesTab.GetComponent<RectTransform>().sizeDelta = selectedAnchors;
                //ressourcesTabText.color = darkColor;
                break;
            default:
                Debug.Log("Default case in the switch");
                break;
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject btn = headerPanel.transform.GetChild(i).gameObject;
            Image img = btn.GetComponent<Image>();
            if (img.color == brightColor)
            {
                StartCoroutine(ColorFade(brightColor, darkColor, 0.1f, img));
                //btn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = brightColor;
                btn.GetComponent<RectTransform>().sizeDelta = unselectedAnchors;
            }
        }
    }

    public void CloseTabs()
    {
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
        Food = 0,
        Drink,
        Meds,
        Ressources
    }
}
