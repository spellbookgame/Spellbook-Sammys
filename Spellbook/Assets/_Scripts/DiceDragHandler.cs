using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Kiwasi Games, Grace Ko
/// script created to handle dragging of dice
/// </summary>
public class DiceDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemToDrag;
    public GameObject itemBeingDragged;
    public Transform originalParent;

    private Vector3 startPos;
    private Transform startParent;

    Player localPlayer;

    void Start()
    {
        originalParent = transform.parent;

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // play grab sound
        SoundManager.instance.PlaySingle(SoundManager.dicePickUp);

        // itemToDrag is the game object that this script is on
        itemToDrag = gameObject;
        startPos = transform.position;
        startParent = transform.parent;

        // set the anchors of the dice so it'll follow mouse correctly
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

        // set the parent to canvas so the dice slot will no longer have a child
        transform.SetParent(GameObject.Find("UI Canvas").transform);

        // allows to pass events through the item being dragged
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // setting transform position to current mouse position
        Vector3 screenpoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        screenpoint.z = 10.0f;
        itemToDrag.transform.position = Camera.main.ScreenToWorldPoint(screenpoint);

        itemBeingDragged = itemToDrag;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemToDrag = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // if item's parent is where it started from onBeginDrag() and drag ended without changing parent, snap it back
        if (transform.parent == startParent || !transform.parent.tag.Equals("Slot"))
        {
            transform.position = startPos;
        }
    }
}
