using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image icon;
    public Image slot;
    public Color equippedColor;
    public Color standardColor;
    public Color selectedColor;
    public PlayerController player;
    public SlotsHandler slotsHandler;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.GetSprite();
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public Item GetItem()
    {
        return item;
    }

    public void Select()
    {
        Gear g = item as Gear;
        if (g!=null && player.IsEquipped(g))
        {
            Color c = (equippedColor + selectedColor)/2;
            slot.color = c;
        }
        else
        {
            slot.color = selectedColor;
        }
    }

    public void Unselect()
    {
        Gear g = item as Gear;
        if (g != null && player.IsEquipped(g))
        {
            slot.color = equippedColor;
        }
        else
        {
            slot.color = standardColor;
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked");
        Select();
        slotsHandler.SlotSelected(this);
    }
}
