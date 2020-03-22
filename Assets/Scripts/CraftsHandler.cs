using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftsHandler : MonoBehaviour
{
    public List<Item> craftingList;
    public GameObject craftPrefab;
    public CraftingController craftingController;

    private List<CraftingSlot> slots = new List<CraftingSlot>();
    private GameObject contentPanel;
    private CraftingSlot slotSelected;

    void Awake() {
        contentPanel = gameObject;
    }

    // Start is called before the first frame update
    void Start() {
        Clear();
        Show();
    }

    public void Clear() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void Show() {
        slots = new List<CraftingSlot>();

        foreach (Item item in craftingList) {
            GameObject obj = Instantiate(craftPrefab);

            CraftingSlot slot = obj.GetComponent<CraftingSlot>();
            slot.AddItem(item);
            slot.craftsHandler = this;

            obj.transform.SetParent(contentPanel.transform, false);

            slots.Add(slot);
        }
    }

    public void SlotSelected(CraftingSlot slot) {
        slotSelected = slot;
        craftingController.SlotSelected();
        UpdateSlots();
    }

    public void UpdateSlots() {
        foreach (CraftingSlot s in slots) {
            if (s != slotSelected) s.Unselect();
            else s.Select();
            s.Render();
        }
    }

    public CraftingSlot GetSlotSelected() {
        return slotSelected;
    }
}
