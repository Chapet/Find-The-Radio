using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    private List<RecipeSlot> recipeSlots = new List<RecipeSlot>();
    private Item item;
    private List<Item> recipe;
    public GameObject contentPanel;

    public Image icon;
    public GameObject ingredientPrefab;

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
    }

    public void Clear() {
        foreach (Transform child in contentPanel.transform) {
            Destroy(child.gameObject);
        }
    }
}
