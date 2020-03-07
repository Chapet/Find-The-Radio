using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SlotsHandler : MonoBehaviour
{
    List<InventorySlot> slots = new List<InventorySlot>();
    private GameObject contentPanel;
    public GameObject slotPrefab;
    public PlayerController player;
    public InventoryController inventoryController;
    private InventorySlot currentlySelected = null;


    public void Awake()
    {
        contentPanel = this.gameObject;
    }

    public void SlotSelected(InventorySlot selected)
    {
        currentlySelected = selected;
        inventoryController.SlotSelected();
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (InventorySlot s in slots)
        {
            if (s != currentlySelected)
            {
                s.Unselect();
            }
            else
            {
                s.Select();
            }
            s.Render();
        }
    }

    public void Show(List<Item> items)
    {
        slots = new List<InventorySlot>();
        foreach (Item i in items)
        {
            GameObject obj = Instantiate(slotPrefab);

            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.AddItem(i);
            slot.slotsHandler = this;
            slot.player = player;
            obj.transform.SetParent(contentPanel.transform, false);

            slots.Add(slot);
        }
        UpdateSlots();
    }

    public void DeleteCurrentSlot()
    {
        slots.Remove(currentlySelected);
        Destroy(currentlySelected.gameObject);
    }

    public InventorySlot GetCurrentSlot()
    {
        return currentlySelected;
    }
}
