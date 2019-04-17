using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// drag and drop implementation from Kiwasi Games
public class DiceSlotHandler : MonoBehaviour, IDropHandler
{
    Player localPlayer;

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
        //localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
    }

    // happens before OnEndDrag in DragHandler.cs
    public void OnDrop(PointerEventData eventData)
    {
        // play drop sound
        SoundManager.instance.PlaySingle(SoundManager.placespellpiece);

        // if the slot has no item, then allow item to be dragged in
        if (!item)
        {
            // set item being dragged's parent to current slot's transform
            DiceDragHandler.itemToDrag.transform.SetParent(transform);
        }
        // enable dice roll if it's in the tray
        if(transform.parent.name == "Dice Tray")
        {
            DiceDragHandler.itemToDrag.GetComponent<DiceRoll>().enabled = true;
        }
    }
}
