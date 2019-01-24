using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* 
 * code from Kiwasi Games
 * this script creates a builder that builds strings of item 
 * names as they are dropped into slots 
*/
public class SpellManager : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;
    [SerializeField] GameObject spellSlot;
    [SerializeField] GameObject panel;

    Player localPlayer;
   
    // Start is called before the first frame update
    void Start()
    {

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        HasChanged();

        int numSpellPieces = localPlayer.Spellcaster.numSpellPieces;
        // populate panel with spell pieces depending on how many player has
        if (panel != null)
        {
            while(numSpellPieces > 0)
            {
                GameObject g = (GameObject)Instantiate(spellSlot);
                g.transform.SetParent(panel.transform, false);
                numSpellPieces--;
            }
        }
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
                builder.Append(item.name);
                builder.Append(" - ");
            }
        }
        inventoryText.text = builder.ToString();
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged: IEventSystemHandler
    {
        void HasChanged();
    }
}
