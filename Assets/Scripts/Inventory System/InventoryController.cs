﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    public static InventoryController InvController
    {
        get; private set;
    }

    public MenuController menuController;

    private InventoryManager inventory;
    private PlayerController playerController;

    public TabController tabController;
    public ScrollRect scrollRect;

    public GameObject scrollView;
    public CanvasGroup canvasGroup;
    public GameObject contentPanel;

    public GameObject itemView;
    public GameObject playerView;

    public EquipmentSlot[] equipment;

    private SlotsHandler slotsHandler;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public GameObject descBackground;
    public Image itemPreview;
    public Button useButton;
    public Button deleteItemButton;
    public Button equipButton;
    public Button deleteGearButton;
    public GameObject itemProperties;
    public GameObject health;
    public GameObject hunger;
    public GameObject thirst;
    public GameObject energy;

    public Sprite blankSprite;

    public Color normalColor;
    public Color selectedColor;

    private float fadeDuration = 0.3f;

    private IEnumerator coroutine;

    void Awake()
    {
        Clear();
        slotsHandler = contentPanel.GetComponent<SlotsHandler>();
        coroutine = UsedNotification();
        InvController = this;
        equipment = new EquipmentSlot[5];
    }

    private void Start()
    {
        inventory = InventoryManager.Inventory;
        playerController = PlayerController.Player;
    }

    public void AddEquipmentSlot(EquipmentSlot es)
    {
        equipment[(int)es.slotType] = es;
    }

    public void Show(List<Item> items)
    {
        scrollView.SetActive(true);
        slotsHandler.Show(items);
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

    public void StandardTabSelected()
    {
        playerView.SetActive(false);
    }

    public void GearTabSelected()
    {
        itemView.SetActive(false);
        playerView.SetActive(true);
        equipButton.gameObject.SetActive(false);
        deleteGearButton.gameObject.SetActive(false);
        RefreshEquipmentSlots();
    }

    public void UseBtnClicked()
    {
        InventorySlot curr = slotsHandler.GetCurrentSlot();
        if (curr != null)
        {
            Item selectedItem = curr.GetItem();
            Consumable cons = selectedItem as Consumable;

            //playerController.UpdateEnergy(cons.GetEnergy());
            //playerController.UpdateHealth(cons.GetHealth());
            //playerController.UpdateHunger(cons.GetHunger());
            //playerController.UpdateThirst(cons.GetThirst());
            BackgroundTasks.Tasks.Use(cons);

            inventory.RemoveItem(selectedItem);
            slotsHandler.DeleteCurrentSlot();

            ClearInfoPanel();
        }
    }

    private IEnumerator UsedNotification()
    {
        nameText.SetText("");
        itemPreview.sprite = blankSprite;
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        ClearProperties();
        descriptionText.SetText("Item used :-)");

        yield return new WaitForSeconds(1f);
        descriptionText.SetText("");
        descBackground.SetActive(false);
    }

    public void EquipBtnClicked()
    {
        InventorySlot curr = slotsHandler.GetCurrentSlot();
        if (curr != null)
        {

            Item selectedItem = curr.GetItem();
            Gear g = selectedItem as Gear;

            if (playerController.IsEquipped(g))
            {
                playerController.UnequipGear(g);
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Equip");
            }
            else
            {
                playerController.EquipGear(g);
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Unequip");
            }

            slotsHandler.UpdateSlots();
            RefreshEquipmentSlots();
        }
    }
    public void DeleteBtnClicked()
    {
        InventorySlot curr = slotsHandler.GetCurrentSlot();
        if (curr != null)
        {
            Item selectedItem = curr.GetItem();

            inventory.RemoveItem(selectedItem);
            slotsHandler.DeleteCurrentSlot();

            if (selectedItem is Gear)
            {
                equipButton.gameObject.SetActive(false);
                deleteGearButton.gameObject.SetActive(false);
                RefreshEquipmentSlots();
            }
            else
            {
                ClearInfoPanel();
            }
        }
    }

    private void ClearInfoPanel()
    {
        itemView.SetActive(false);

        nameText.SetText("");
        descriptionText.SetText("");

        itemPreview.sprite = blankSprite;

        descBackground.SetActive(false);
        useButton.gameObject.SetActive(false);

        ClearProperties();
    }

    private void SetProperties(Item item)
    {
        if (item.IsConsumable())
        {
            Consumable toUse = (Consumable)item;
            if (toUse.HaveHealthValue())
            {
                SetProperty(health, toUse.GetHealth());
            }
            if (toUse.HaveHungerValue())
            {
                SetProperty(hunger, toUse.GetHunger());
            }
            if (toUse.HaveThirstValue())
            {
                SetProperty(thirst, toUse.GetThirst());
            }
            if (toUse.HaveEnergyValue())
            {
                SetProperty(energy, toUse.GetEnergy());
            }
        }
        else
        {
            Debug.Log("Sorry, item not usable");
        }
        itemProperties.SetActive(true);
    }

    private void ClearProperties()
    {
        health.SetActive(false);
        hunger.SetActive(false);
        thirst.SetActive(false);
        energy.SetActive(false);

        itemProperties.SetActive(false);
    }

    private void SetProperty(GameObject go, int value)
    {
        string str = "";
        if (value < 0)
        {
            value = PropertyRound(-value);
            for (int i = 0; i < value; i++)
            {
                str += "-";
            }
        }
        else
        {
            value = PropertyRound(value);
            for (int i = 0; i < value; i++)
            {
                str += "+";
            }
        }
        go.transform.GetChild(1).GetComponent<TMP_Text>().SetText(str);
        go.SetActive(true);
    }

    public void SlotSelected()
    {
        StopCoroutine(coroutine);

        InventorySlot selected = slotsHandler.GetCurrentSlot();
        descBackground.SetActive(true);

        Item selectedItem = selected.GetItem();

        nameText.SetText(selectedItem.name);
        descriptionText.SetText(selectedItem.GetDescription());
        itemPreview.sprite = selectedItem.GetSprite();
        itemPreview.color = selectedItem.GetMaskColor();

        ClearProperties();

        if (selectedItem.IsConsumable())
        {
            useButton.gameObject.SetActive(true);
            deleteItemButton.gameObject.SetActive(true);
            SetProperties(selectedItem);
            itemView.SetActive(true);
        }
        else if (selectedItem.IsGear())
        {
            equipButton.gameObject.SetActive(true);
            deleteGearButton.gameObject.SetActive(true);
            Gear g = selectedItem as Gear;
            if (playerController.IsEquipped(g))
            {
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Unequip");
            }
            else
            {
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Equip");
            }
        }
        else
        {
            itemView.SetActive(true);
            deleteItemButton.gameObject.SetActive(true);
        }
    }

    public void ExitBtnClicked()
    {
        tabController.CloseTabs();
        menuController.ExitMenu(gameObject);
    }

    private int PropertyRound(int i)
    {
        if (i > 75)
        {
            return 4;
        }
        else if (i > 50)
        {
            return 3;
        }
        else if (i > 25)
        {
            return 2;
        }
        else if (i > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void RefreshEquipmentSlots()
    {
        foreach (EquipmentSlot e in equipment)
        {
            if (e != null)
            {
                e.Refresh();
            }
        }
    }
}
