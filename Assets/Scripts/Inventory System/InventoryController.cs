using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    public PlayerController player;

    public TabController tabController;
    public ScrollRect scrollRect;

    public GameObject scrollView;
    public CanvasGroup canvasGroup;
    public GameObject contentPanel;
    public InventoryManager inventory;

    private SlotsHandler slotsHandler;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public GameObject descBackground;
    public Image itemPreview;
    public Button useButton;
    public Button equipButton;
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
        /*
        Gear gunFromResources = Resources.Load<Gear>("Items/Gears/Gun");
        if (gunFromResources != null)
        {
            Debug.Log("Successfully getting : " + gunFromResources);
        }
        else
        {
            Debug.Log("Unable to reach the requested object!");
        }
        */
    }

    void OnEnable()
    {
        StartCoroutine(StartOnTabAfterWait(15));
    }

    public void Show(List<Item> items)
    {
        slotsHandler.Show(items);
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

    public void UseBtnClicked()
    {
        InventorySlot curr = slotsHandler.GetCurrentSlot();
        if (curr != null)
        {
            Item selectedItem = curr.GetItem();
            Consumable cons = selectedItem as Consumable;
            Debug.Log("This item is usable, updating corresponding values ...");
            player.UpdateEnergy(cons.GetEnergy());
            player.UpdateHealth(cons.GetHealth());
            player.UpdateHunger(cons.GetHunger());
            player.UpdateThirst(cons.GetThirst());

            inventory.RemoveItem(selectedItem);
            slotsHandler.DeleteCurrentSlot();

            coroutine = UsedNotification();

            StartCoroutine(coroutine);

            Debug.Log(selectedItem + " used!");
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

            if (player.IsEquipped(g))
            {
                player.UnequipGear(g);
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Equip");
            }
            else
            {
                player.EquipGear(g);
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Unequip");
            }

            slotsHandler.UpdateSlots();
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
        ClearProperties();
    }

    private void SetProperties(Item item)
    {
        if(item.IsConsumable())
        {
            Consumable toUse = (Consumable)item;
            if(toUse.HaveHealthValue())
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
            for(int i = 0; i< value; i++)
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

        if (selectedItem.IsConsumable())
        {
            useButton.gameObject.SetActive(true);
            SetProperties(selectedItem);
        }
        else if (selectedItem.IsGear())
        {
            equipButton.gameObject.SetActive(true);
            Gear g = selectedItem as Gear;
            if (player.IsEquipped(g))
            {
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Unequip");
            }
            else
            {
                equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Equip");
            }
        }
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
        else if(i > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }           
    }

    IEnumerator StartOnTabAfterWait(int msec)
    {
        Debug.Log("Wait for " + msec + " millisecond(s)");
        yield return new WaitForSeconds((float)msec / 1000f);
        tabController.TabBtnListener(TabController.Tab.FoodAndDrink);
    }
}
