using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// drag and drop implementation from Kiwasi Games
public class DiceSlotHandler : MonoBehaviour, IDropHandler
{
    Player localPlayer;
    DiceUIHandler diceUIHandler;

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
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        diceUIHandler = GameObject.Find("Dice Tray").GetComponent<DiceUIHandler>();
    }

    // happens before OnEndDrag in DragHandler.cs
    public void OnDrop(PointerEventData eventData)
    {
        // play drop sound
        SoundManager.instance.PlaySingle(SoundManager.dicePlace);

        // if the slot has no item, then allow item to be dragged in
        if (!item)
        {
            // set item being dragged's parent to current slot's transform
            DiceDragHandler.itemToDrag.transform.SetParent(transform);
        }

        // enable dice roll if it's dropped in the tray
        if(transform.parent.name == "Dice Tray")
        {
            DiceDragHandler.itemToDrag.GetComponent<DiceRoll>().rollEnabled = true;
            diceUIHandler.rollButton.interactable = true;
        }
        else if(transform.parent.name == "Scroll Content")
        {
            DiceDragHandler.itemToDrag.GetComponent<DiceRoll>().rollEnabled = false;
            CheckSlots();
        }
    }

    private void CheckSlots()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        diceUIHandler.rollButton.interactable = false;

        foreach(GameObject g in slots)
        {
            if(g.transform.parent.name.Equals("Dice Tray") && g.transform.childCount > 0)
            {
                diceUIHandler.rollButton.interactable = true;
                break;
            }
        }
    }
}
