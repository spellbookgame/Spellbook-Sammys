using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public ItemBox itemBox;
    public GameObject item
    {
        get
        {
            foreach(Transform child in transform)
            {
                if (child.GetComponent<Image>().IsActive())
                {
                    return child.gameObject;
                }
            }
            return null;
        }
    }
    // happens before OnEndDrag in DragHandler.cs
    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log("OnDrop");
        // play drop sound
        //SoundManager.instance.PlaySingle(SoundManager.);
        
        // if the slot has no item, then allow item to be dragged in
        if (!item)
        {
            // set item being dragged's parent to current slot's transform
            ItemDragHandler.itemToDrag.transform.SetParent(transform);
            if(gameObject.name == "ItemSlot")
            {
                itemBox.DropItemToNetwork();
            }
            else
            {
                itemBox.CheckIfFromNetwork();
            }
        }
    }
}
