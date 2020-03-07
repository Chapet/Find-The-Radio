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
        foreach(InventorySlot s in slots)
        {
            if (s != selected)
            {
                s.Unselect();
            }
        }
        inventoryController.SlotSelected();
    }

    public void Show(List<Item> items)
    {
        slots = new List<InventorySlot>();
        foreach (Item i in items)
        {
            GameObject obj = Instantiate(slotPrefab);
            //Button btn = obj.GetComponent<Button>();
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.AddItem(i);
            slot.slotsHandler = this;
            slot.player = player;
            obj.transform.SetParent(contentPanel.transform, false);
            /*
            btn.onClick.AddListener(delegate
            {
                SlotListener(slot, btn);
            });
            */
            slot.Unselect();
            slots.Add(slot);
        }
    }

    public void DeleteCurrentSlot()
    {
        Destroy(currentlySelected.gameObject);
    }

    public InventorySlot GetCurrentSlot()
    {
        return currentlySelected;
    }
}
