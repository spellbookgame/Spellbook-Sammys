using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// script from Kiwasi Games
public class SlotHandler : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            // if it has a child, return the first child
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        // if the slot has no item, then allow item to be dragged in
        if(!item)
        {
            // set item being dragged's transform to current slot transform
            DragHandler.itemToDrag.transform.SetParent(transform);

            // using lambda function to call HasChanged method in SpellCreateScript
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
}
