using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingController : MonoBehaviour
{
    public MenuController menuController;
    public CraftsHandler craftsHandler;
    public InventoryManager inventoryManager;

    public GameObject descBackground;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image itemPreview;
    public Button craftButton;


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
    }

    public void UpdateCraftable() {
        craftsHandler.UpdateRecipe();
        CraftingSlot selectedSlot = craftsHandler.GetSlotSelected();
        if (selectedSlot == null) return;
        if (selectedSlot.GetIsCraftable()) {
            craftButton.interactable = true; 
        }
        else {
            craftButton.interactable = false;
        }
    }

    public void CraftBtnClicked() {
        CraftingSlot selectedSlot = craftsHandler.GetSlotSelected();
        foreach (Item s in selectedSlot.GetRecipe()) {
            inventoryManager.RemoveItem(s);
        }
        inventoryManager.AddItem(selectedSlot.GetItem());
        UpdateCraftable();
    }
}
