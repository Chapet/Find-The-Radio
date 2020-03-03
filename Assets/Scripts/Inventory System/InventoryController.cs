using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    public PlayerController player;

    public GameObject scrollView;
    public GameObject slotPrefab;
    public CanvasGroup canvasGroup;
    public GameObject contentPanel;
    public InventoryManager inventory;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public GameObject descBackground;
    public Image itemPreview;

    public Sprite blankSprite;

    public Color normalColor;
    public Color selectedColor;

    Item selectedItem = null;
    Button prevSelectedSlot = null;

    private float fadeDuration = 0.3f;

    public void Awake()
    {
        Clear();
    }

    public void Show(List<Item> items)
    {
        foreach(Item i in items)
        {
            GameObject obj = Instantiate(slotPrefab);
            Button btn = obj.GetComponent<Button>();
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.AddItem(i);
            obj.transform.SetParent(contentPanel.transform, false);
            btn.onClick.AddListener(delegate
            {
                SlotListener(slot, btn);
            });
        }
        scrollView.SetActive(true);
        StartCoroutine(DoFade(0, 1));
    }

    public void Clear()
    {
        foreach (Transform child in contentPanel.transform)
        {
            Destroy(child.gameObject);
        }
        if (scrollView.activeSelf)
        {
            StartCoroutine(DoFade(1, 0));
            scrollView.SetActive(false);
        }
        ClearInfoPanel();
    }

    private IEnumerator DoFade(float start, float end)
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            yield return null;
        }
    }

    public void SlotListener(InventorySlot slot, Button btn)
    {
        if (prevSelectedSlot != null)
        {
            prevSelectedSlot.GetComponent<Image>().color = normalColor;
        }

        descBackground.SetActive(true);

        Item i = slot.GetItem();
        Debug.Log("It is : " + i.name);

        btn.GetComponent<Image>().color = selectedColor;
        nameText.SetText(i.name);
        descriptionText.SetText(i.GetDescription());
        Debug.Log("Setting item preview to item image");
        itemPreview.sprite = i.GetSprite();
        selectedItem = i;
        prevSelectedSlot = btn;
    }

    public void UseBtnClicked()
    {
        if (selectedItem != null)
        {
            Debug.Log(selectedItem + " used!");
            if (selectedItem is Usable)
            {
                Debug.Log("This item is usable!");
            }
            inventory.RemoveItem(selectedItem);
            Destroy(prevSelectedSlot.gameObject);
            selectedItem = null;

            ClearInfoPanel();
        }
    }

    private void ClearInfoPanel()
    {
        Debug.Log("Clearing info panel ...");
        nameText.SetText("");
        descriptionText.SetText("");
        Debug.Log("Setting item preview to blank sprite");
        itemPreview.sprite = blankSprite;
        descBackground.SetActive(false);
    }
}
