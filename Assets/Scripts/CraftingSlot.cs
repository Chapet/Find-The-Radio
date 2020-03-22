using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    private List<RecipeSlot> recipeSlots = new List<RecipeSlot>();
    private Item item;
    private List<Item> recipe;
    private bool selected = false;
    private bool isCraftable = true;


    public GameObject contentPanel;
    public Image icon;
    public Image slot;
    public GameObject ingredientPrefab;
    public CraftsHandler craftsHandler;
    public InventoryManager inventoryManager;
    public Color selectedColor;
    public Color standardColor;

    public List<Item> GetRecipe() {
        return recipe;
    }

    public Item GetItem() {
        return item;
    }

    public bool GetIsCraftable() {
        return isCraftable;
    }

    public void AddItem(Item newItem) {
        item = newItem;
        recipe = newItem.Recipe;
        icon.sprite = newItem.GetSprite();
        icon.enabled = true;
        Show();
    }

    public void Show() {
        Clear();
        recipeSlots = new List<RecipeSlot>();

        foreach (Item item in recipe) {
            GameObject obj = Instantiate(ingredientPrefab);

            RecipeSlot slot = obj.GetComponent<RecipeSlot>();
            slot.AddItem(item);

            obj.transform.SetParent(contentPanel.transform, false);
            recipeSlots.Add(slot);
        }
        UpdateRecipe();
    }

    public void Clear() {
        foreach (Transform child in contentPanel.transform) {
            Destroy(child.gameObject);
        }
    }

    public void Select() {
        selected = true;
    }

    public void Unselect() {
        selected = false;
    }

    public void Render() {
        if (selected) slot.color = selectedColor;
        else slot.color = standardColor;
    }

    public void OnCLick() {
        Select();
        craftsHandler.SlotSelected(this);
    }

    public void UpdateRecipe() {
        isCraftable = true;
        if (recipeSlots.Count == 0) return;
        Item currentItem = recipeSlots[0].GetItem();
        int n = 0;
        foreach (RecipeSlot i in recipeSlots) {
            if (currentItem != i.GetItem()) {
                currentItem = i.GetItem();
                n = 1;
            } else {
                n++;
            }
            if (inventoryManager.GetItems(currentItem, n) == null) {
                i.Mask();
                isCraftable = false;
            } else {
                i.UnMask();
            }
        }
    }
}
