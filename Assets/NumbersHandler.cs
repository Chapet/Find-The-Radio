using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersHandler : MonoBehaviour {

    private List<Item> recipe;
    private List<NumberSlot> slots;
    private GameObject contentPanel;

    public GameObject numberPrefab;
    private InventoryManager inventoryManager;

    void Awake() {
        contentPanel = gameObject;
    }

    void Start() {
        Clear();
        inventoryManager = InventoryManager.Inventory;
    }

    public void Clear() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void Show(CraftingSlot selectedItem) {
        Clear();

        recipe = selectedItem.GetRecipe();
        slots = new List<NumberSlot>();
        GameObject obj;
        NumberSlot slot;

        int n = 1;
        Item previous = recipe[0];
        for (int i = 1; i < recipe.Count; i++) {
            if (!previous.IsSameAs(recipe[i])) {
                obj = Instantiate(numberPrefab);

                slot = obj.GetComponent<NumberSlot>();
                slot.AddItem(previous);
                slot.SetNumbers(getNumberInInvenotry(previous), n);

                obj.transform.SetParent(contentPanel.transform, false);

                slots.Add(slot);
                n = 1;
            } else {
                n++;
            }
            previous = recipe[i];
        }
        obj = Instantiate(numberPrefab);

        slot = obj.GetComponent<NumberSlot>();
        slot.AddItem(previous);
        slot.SetNumbers(getNumberInInvenotry(previous), n);

        obj.transform.SetParent(contentPanel.transform, false);

        slots.Add(slot);
    }

    public int getNumberInInvenotry(Item item) {
        int n = 0;
        while (inventoryManager.GetItems(item, n + 1) != null) {
            n++;
        }
        return n;
    }

    public void UpdateNumbers() {
        foreach(NumberSlot slot in slots) {
            slot.SetNumbers(getNumberInInvenotry(slot.GetItem()), slot.GetCraftingNumber());
        }
    }
}
