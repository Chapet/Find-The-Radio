using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingController : MonoBehaviour
{
    public MenuController menuController;
    public CraftsHandler craftsHandler;
    public NumbersHandler numbersHandler;
    private InventoryManager inventoryManager;

    public GameObject descBackground;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image itemPreview;
    public Button craftButton;
    public PopupSystem pop;

    private bool isCoroutineExecuting = false;

    private void Start()
    {
        inventoryManager = InventoryManager.Inventory;
    }

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }

    public void SlotSelected() {
        CraftingSlot selectedSlot = craftsHandler.GetSlotSelected();
        Item selectedItem = selectedSlot.GetItem();

        descBackground.SetActive(true);
        nameText.SetText(selectedItem.name);
        descriptionText.SetText(selectedItem.GetDescription());
        itemPreview.sprite = selectedItem.GetSprite();
        if (selectedSlot.GetIsCraftable()) {
            craftButton.interactable = true;
        } else {
            craftButton.interactable = false;
        }
        numbersHandler.Show(selectedSlot);
    }

    public void UpdateCraftable() {
        craftsHandler.UpdateRecipe();
        CraftingSlot selectedSlot = craftsHandler.GetSlotSelected();
        if (selectedSlot == null) return;
        numbersHandler.UpdateNumbers();
        if (selectedSlot.GetIsCraftable()) {
            StartCoroutine(ExecuteAfterTime(1f));
        }
        else {
            craftButton.interactable = false;
        }
    }

    public void CraftBtnClicked() {
        CraftingSlot selectedSlot = craftsHandler.GetSlotSelected();
        foreach (Item s in selectedSlot.GetRecipe()) {
            Item removedItem = inventoryManager.GetItem(s);
            inventoryManager.RemoveItem(removedItem);
        }
        inventoryManager.AddItem(selectedSlot.GetItem());
        craftButton.interactable = false;
        if (selectedSlot.GetItem().IsSameAs(Resources.Load<Gear>("Items/Gear/Radio")))
        {
            pop.PopMessage(PopupSystem.Popup.Radio);
        }
        UpdateCraftable();
    }

    IEnumerator ExecuteAfterTime(float time) {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        craftButton.interactable = true;

        isCoroutineExecuting = false;
    }
}
