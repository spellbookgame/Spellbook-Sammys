using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Button button;
    public Image icon;

    ItemObject item;

    public void AddItem (ItemObject newItem)
    {
        item = newItem;

        icon.sprite = item.sprite;
        icon.enabled = true;
    }

    public void ClearSlot() 
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
