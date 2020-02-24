using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryMenuButtonListener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Tooltip("Link to the UI gameObject that contain the inventory items")]private UiInventoryManager inventoryManager;
    [SerializeField] private Button foodButton;
    [SerializeField] private Button drinkButton;
    [SerializeField] private Button medicationButton;
    [SerializeField] private Button ressourceButton;

    [SerializeField] private UiInventoryManager uiInventoryManager;
    
    

    

    public void OnClickFood()
    {
        uiInventoryManager.selectedCategory = InventoryButton.Food;
        inventoryManager.LoadFoodUiItems();
    }

    public void OnClickDrink()
    {
        uiInventoryManager.selectedCategory = InventoryButton.Drink;
        inventoryManager.LoadDrinkItems();
    }

    public void OnClickMedication()
    {
        uiInventoryManager.selectedCategory = InventoryButton.Medication;
        inventoryManager.LoadMedicationItems();
    }

    public void OnClickRessource()
    {
        uiInventoryManager.selectedCategory = InventoryButton.Ressource;
        inventoryManager.LoadRessourcesItems();
    }
}
