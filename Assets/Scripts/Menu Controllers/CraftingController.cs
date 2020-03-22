using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingController : MonoBehaviour
{
    public MenuController menuController;
    public CraftsHandler craftsHandler;

    public GameObject descBackground;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image itemPreview;


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
    }
}
