using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image icon;
    public Image slot;
    public Image check;
    public Color equippedColor;
    public Color standardColor;
    public Color selectedColor;
    private PlayerController player;
    public SlotsHandler slotsHandler;
    private bool selected = false;
    [SerializeField] private int instanceID;

    private void Start()
    {
        player = PlayerController.Player;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.GetSprite();
        icon.enabled = true;
        check.enabled = false;
        player = PlayerController.Player;
        instanceID = item.GetInstanceID();
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public Item GetItem()
    {
        return item;
    }

    public void Select()
    {
        selected = true;
    }

    public void Unselect()
    {
        selected = false;
    }

    public void Render()
    {
        Gear g = item as Gear;
        /*
        if (player==null)
        {
            player = PlayerController.GetPlayer;
        }
        */
        if (g != null && player.IsEquipped(g))
        {
            check.enabled = true;
        }
        else
        {
            check.enabled = false;
        }

        if (selected)
        {
            slot.color = selectedColor;
        }
        else
        {
            
            slot.color = standardColor;
        }
    }

    public void PlayDeleteAnimation(float animDuration)
    {
        var canvGroup = gameObject.GetComponent<CanvasGroup>();
        Animator anim = gameObject.GetComponent<Animator>();
        StartCoroutine(DoFade(canvGroup, 0, 1, animDuration));
        anim.SetTrigger("PopTrigger");
    }

    private IEnumerator DoFade(CanvasGroup c, float start, float end, float animDuration)
    {
        float counter = 0f;

        while (counter < animDuration)
        {
            counter += Time.deltaTime;
            c.alpha = Mathf.Lerp(start, end, counter / animDuration);

            yield return null;
        }
    }

    public void OnClick()
    {
        Select();
        slotsHandler.SlotSelected(this);
    }
}
