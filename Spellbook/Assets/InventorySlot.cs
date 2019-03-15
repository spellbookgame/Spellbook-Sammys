using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    ItemObject item;
    

    public void AddItem (ItemObject newItem)
    {
        item = newItem;

        icon.sprite = newItem.sprite;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot() 
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() 
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.RemoveFromInventory(item);
    }

}
