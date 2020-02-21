using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryCategory
{
    Food,Drink,Medicalion,Ressource
}

public class InventoryMenuButtonListener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Tooltip("Link to the UI gameObject that contain the inventory items")]private UiInventoryManager inventoryManager;
    [SerializeField] private Button foodButton;
    [SerializeField] private Button drinkButton;
    [SerializeField] private Button medicationButton;
    [SerializeField] private Button ressourceButton;
    public InventoryCategory selectedCategory;
    

    

    public void OnClickFood()
    {
        selectedCategory = InventoryCategory.Food;
        inventoryManager.LoadFoodUiItems();
    }

    public void OnClickDrink()
    {
        selectedCategory = InventoryCategory.Drink;
        inventoryManager.LoadDrinkItems();
    }

    public void OnClickMedication()
    {
        selectedCategory = InventoryCategory.Medicalion;
        inventoryManager.LoadMedicationItems();
    }

    public void OnClickRessource()
    {
        selectedCategory = InventoryCategory.Ressource;
        inventoryManager.LoadRessourcesItems();
    }
}
