using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuButtonListener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UiInventoryManager inventoryManager;
    [SerializeField] private Button allButton;
    [SerializeField] private Button foodButton;
    [SerializeField] private Button drinkButton;
    [SerializeField] private Button medicationButton;
    [SerializeField] private Button ressourceButton;

    public void OnClickAll()
    {
        inventoryManager.LoadAllUiItems();
    }

    public void OnClickFood()
    {
        inventoryManager.LoadFoodUiItems();
    }

    public void OnClickDrink()
    {
        inventoryManager.LoadDrinkItems();
    }

    public void OnClickMedication()
    {
        inventoryManager.LoadMedicationItems();
    }

    public void OnClickRessource()
    {
        inventoryManager.LoadRessourcesItems();
    }
}
