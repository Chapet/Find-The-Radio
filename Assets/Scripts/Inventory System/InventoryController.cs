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
    public Button useButton;
    public Button equipButton;

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

        if(selectedItem.IsOfType(ItemType.Usable))
        {
            useButton.gameObject.SetActive(true);
        }
        else if (selectedItem.IsOfType(ItemType.Gear))
        {
            equipButton.gameObject.SetActive(true);
        }
    }

    public void UseBtnClicked()
    {
        if (selectedItem != null)
        {
            Usable usable = (Usable)selectedItem;
            Debug.Log("This item is usable, updating corresponding values ...");
            player.UpdateEnergy(usable.GetEnergy());
            player.UpdateHealth(usable.GetHealth());
            player.UpdateHunger(usable.GetHunger());
            player.UpdateThirst(usable.GetThirst());
            inventory.RemoveItem(selectedItem);
            Destroy(prevSelectedSlot.gameObject);
            selectedItem = null;

            ClearInfoPanel();

            Debug.Log(selectedItem + " used!");
        }
    }

    public void EquipBtnClicked()
    {
        if (selectedItem != null)
        {
            Debug.Log("Equipped !");
            //equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Equipped");
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
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
    }
}
