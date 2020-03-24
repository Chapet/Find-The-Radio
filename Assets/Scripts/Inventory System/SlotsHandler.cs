using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SlotsHandler : MonoBehaviour
{
    List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] private GameObject contentPanel;
    public GameObject slotPrefab;
    public InventoryController inventoryController;
    private InventorySlot currentlySelected = null;

    private void Awake()
    {
        Debug.Log("WTF");
        contentPanel = gameObject;
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
            obj.transform.SetParent(contentPanel.transform, false);

            slots.Add(slot);
        }
        UpdateSlots();
    }

    public void DeleteCurrentSlot()
    {
        slots.Remove(currentlySelected);
        currentlySelected.PlayDeleteAnimation(17f / 60f);
        Destroy(currentlySelected.gameObject, 20f / 60f);
    }

    public InventorySlot GetCurrentSlot()
    {
        return currentlySelected;
    }
}
