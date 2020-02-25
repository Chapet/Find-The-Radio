using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    public MenuController menuController;

    public void ExitBtnClicked()
    {
        menuController.ExitMenu(gameObject);
    }
}
