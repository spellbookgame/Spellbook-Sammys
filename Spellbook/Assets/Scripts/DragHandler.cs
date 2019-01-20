using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// code by Kiwasi Games
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemToDrag;
    private Vector3 startPos;
    private Transform startParent;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        startParent = transform.parent;
        // allows to pass events through the item being dragged
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //itemToDrag = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // checking to see if the item is being dragged into a new slot
        if (transform.parent == startParent)
        {
            transform.position = startPos;
        }
        
    }
}
