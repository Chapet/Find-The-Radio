using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    private Gear gear;
    public Gear.ItemType slotType;
    public Image icon;
    public Image slot;
    public Sprite empty;
    private PlayerController player;

    private void Start()
    {
        player = PlayerController.Player;
        InventoryController.InvController.AddEquipmentSlot(this);
    }

    public void Refresh()
    {
        gear = player.GetGear(slotType);
        if(gear != null)
        {
            icon.sprite = gear.GetSprite();
        }
        else
        {
            icon.sprite = empty;
        }
    }
}
