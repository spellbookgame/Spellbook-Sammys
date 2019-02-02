using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// script from Kiwasi Games
public class SlotHandler : MonoBehaviour, IDropHandler
{
    SpellManager spellManager;
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

    void Start()
    {
        spellManager = GameObject.Find("Canvas").GetComponent<SpellManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // if the slot has no item, then allow item to be dragged in
        if(!item)
        {
            // set item being dragged's parent to current slot's transform
            DragHandler.itemToDrag.transform.SetParent(transform);

            // if item being dragged's parent's parent is the panel, then remove its element from the hashset
            if(DragHandler.itemToDrag.transform.parent.transform.parent == spellManager.panel.transform)
            {
                spellManager.hashSpellPieces.Remove(DragHandler.itemToDrag.name);
                Debug.Log("removed " + DragHandler.itemToDrag.name);
                Debug.Log("hashset size: " + spellManager.hashSpellPieces.Count);
            }

            // using lambda function to call HasChanged method in SpellManager.cs
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
}
