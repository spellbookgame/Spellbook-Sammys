using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    GameObject g;
    DragHandler dragHandler;

    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Spell Piece");
        dragHandler = g.GetComponent<DragHandler>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // if the slot has no item, then allow item to be dragged in
        if(!item)
        {
            dragHandler.itemToDrag.transform.SetParent(transform);
        }
    }
}
