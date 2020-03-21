using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftsHandler : MonoBehaviour
{
    List<CraftingSlot> slots = new List<CraftingSlot>();
    public List<Item> craftingList;
    public GameObject craftPrefab;
    private GameObject contentPanel;

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

            obj.transform.SetParent(contentPanel.transform, false);

            slots.Add(slot);
        }
    }
}
