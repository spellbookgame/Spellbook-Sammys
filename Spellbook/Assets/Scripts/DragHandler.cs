using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// script from Kiwasi Games
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemToDrag;
    public GameObject itemBeingDragged;
    private Vector3 startPos;
    private Transform startParent;

    public static Transform originalParent;
    SpellManager spellManager;

    void Start()
    {
        spellManager = GameObject.Find("Canvas").GetComponent<SpellManager>();

        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // itemToDrag is the game object that this script is on
        itemToDrag = gameObject;
        startPos = transform.position;
        startParent = transform.parent;
        // allows to pass events through the item being dragged
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // setting transform position to current mouse position
        transform.position = Input.mousePosition;
        itemBeingDragged = itemToDrag;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemToDrag = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // if item was dragged back into its original parent, then set its position back to where it was
        if (transform.parent == startParent)
        {
            transform.position = startPos;
        }
    }
}
