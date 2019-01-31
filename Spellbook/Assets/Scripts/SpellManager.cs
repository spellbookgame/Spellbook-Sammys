using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* 
 * namespace / HasChanged() written by Kiwasi Games
 * this script creates a builder that builds strings of item 
 * names as they are dropped into slots 
*/
public class SpellManager : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] public Text inventoryText;
    [SerializeField] public GameObject panel;

    public HashSet<string> hashSpellPieces;

    Player localPlayer;
    
    void Start()
    {
        hashSpellPieces = new HashSet<string>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        HasChanged();
    }

    void Update()
    {
        // checking to see if each slot is filled
        int i = 0;
        foreach(Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            if(item)
            {
                ++i;
            }
        }
        // if all slots are filled, call the CompareSpells() function
        if(i >= 4)
        {
            localPlayer.Spellcaster.chapter.CompareSpells(localPlayer.Spellcaster, hashSpellPieces);
        }
    }

    // iterates through each slot and deletes child
    public void RemovePrefabs(Spell spell)
    {
        // remove slot children
        foreach(Transform slotTransform in slots)
        {
            Destroy(slotTransform.GetChild(0).gameObject);
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
                // add the spellPiece name to hashset
                hashSpellPieces.Add(slotTransform.GetChild(0).name);
                Debug.Log("Added: " + slotTransform.GetChild(0).name);
                Debug.Log("HashSet count: " + hashSpellPieces.Count);

                // add item name to builder
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
