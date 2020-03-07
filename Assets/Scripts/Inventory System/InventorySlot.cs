using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image icon;
    public Image slot;
    public Image check;
    public Color equippedColor;
    public Color standardColor;
    public Color selectedColor;
    public PlayerController player;
    public SlotsHandler slotsHandler;
    private bool selected = false;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.GetSprite();
        icon.enabled = true;
        check.enabled = false;
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
        selected = true;
    }

    public void Unselect()
    {
        selected = false;
    }

    public void Render()
    {
        Gear g = item as Gear;
        if (g != null && player.IsEquipped(g))
        {
            check.enabled = true;
        }
        else
        {
            check.enabled = false;
        }

        if (selected)
        {
            slot.color = selectedColor;
        }
        else
        {
            
            slot.color = standardColor;
        }
    }

    public void OnClick()
    {
        Select();
        slotsHandler.SlotSelected(this);
    }
}
