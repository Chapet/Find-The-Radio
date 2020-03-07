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

    //InventorySlot selectedSlot = null;
    //Button prevSelectedSlot = null;

    private float fadeDuration = 0.3f;

    void Awake()
    {
        Clear();
        slotsHandler = contentPanel.GetComponent<SlotsHandler>();
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

    /*
    public void SlotListener(InventorySlot slot, Button btn)
    {
        
        if (prevSelectedSlot != null)
        {
            ClearProperties();
            InventorySlot s = prevSelectedSlot.GetComponent<InventorySlot>();
            Gear g = s.GetItem() as Gear;
            if (g != null && player.IsEquipped(g))
            {
                slot.Unselect();
            }
            else
            {
                prevSelectedSlot.GetComponent<Image>().color = normalColor;
            }         
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

        if(selectedItem.IsConsumable())
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
    */

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

            ClearInfoPanel();

            Debug.Log(selectedItem + " used!");
        }
    }

    public void EquipBtnClicked()
    {
        InventorySlot curr = slotsHandler.GetCurrentSlot();
        if (curr != null)
        {
            Item selectedItem = curr.GetItem();
            Gear g = selectedItem as Gear;
            if (g.IsOfType(Gear.ItemType.Head))
            {
                player.PutHeadGearOn(g);
            }
            else if (g.IsOfType(Gear.ItemType.Chest))
            {
                player.PutChestGearOn(g);
            }
            else if (g.IsOfType(Gear.ItemType.Legs))
            {
                player.PutLegsGearOn(g);
            }
            else if (g.IsOfType(Gear.ItemType.Weapon))
            {
                player.EquipWeapon(g);
            }
            else
            {
                Debug.Log("Gear of type NONE : unable to equip this type!");
            }
            Debug.Log("Equipped !");
            equipButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Unequip");
            curr.Select();
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
        InventorySlot selected = slotsHandler.GetCurrentSlot();
        descBackground.SetActive(true);

        Item selectedItem = selected.GetItem();
        Debug.Log("It is : " + selectedItem.name);

        nameText.SetText(selectedItem.name);
        descriptionText.SetText(selectedItem.GetDescription());
        Debug.Log("Setting item preview to item image");
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
