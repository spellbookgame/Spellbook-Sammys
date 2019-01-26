using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* 
 * namespace / HasChanged written by Kiwasi Games
 * this script creates a builder that builds strings of item 
 * names as they are dropped into slots 
*/
public class SpellManager : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;
    [SerializeField] GameObject spellPiece;
    [SerializeField] GameObject panel;

    private bool bSpellCreated;

    Player localPlayer;
    SlotHandler slotHandler;
    ArcaneBlast aBlast1 = new ArcaneBlast();
   
    // Start is called before the first frame update
    void Start()
    {
        bSpellCreated = false;

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        slotHandler = GameObject.Find("Slot").GetComponent<SlotHandler>();
        HasChanged();

        int numSpellPieces = localPlayer.Spellcaster.numSpellPieces;
        // populate panel with spell pieces depending on how many player has
        if (panel != null)
        {
            while(numSpellPieces > 0)
            {
                GameObject g = (GameObject)Instantiate(spellPiece);
                g.transform.SetParent(panel.transform, false);
                numSpellPieces--;
            }
        }
    }

    void Update()
    {
        // ideally this shouldnt be in Update, because we dont want it to keep collecting spells
        // the if statement ensures that this only runs once; once a spell is created, it will stop checking
        if(bSpellCreated == false)
            CheckSpellSlots();
    }

    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(" - ");
        foreach(Transform slotTransform in slots)
        {
            // will return game object if there is one, or null if there isnt
            GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            // if there is an item returned
            if(item)
            {
                // add item name to builder
                builder.Append(item.name);
                builder.Append(" - ");
            }
        }
        inventoryText.text = builder.ToString();
    }

    public void CheckSpellSlots()
    {
        // TODO: make this more efficient?
        // if all the slots are filled
        // iterate through the slots
        // if the pieces in the slots match the required pieces, unlock spell
        // right now, this method makes it so that order matters (left to right, top to bottom)
        int i = 0;
        foreach (Transform slotTransform in slots)
        {
            if(slotTransform.GetChild(0) != null)
            {
                if (slotTransform.GetChild(0).name == aBlast1.requiredPieces[i])
                {
                    i++;
                    if (i >= 4)
                    {
                        localPlayer.Spellcaster.CollectSpell(aBlast1);
                        bSpellCreated = true;
                    }
                }
                else
                    break;
            }
        }
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged: IEventSystemHandler
    {
        void HasChanged();
    }
}
