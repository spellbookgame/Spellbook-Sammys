using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* namespace / HasChanged() written by Kiwasi Games
 * this script creates a builder that builds strings of item 
 * names as they are dropped into slots 
 */
public class SpellManager : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] public Text inventoryText;
    [SerializeField] public GameObject panel;

    [SerializeField] GameObject alchemySP;
    [SerializeField] GameObject arcaneSP;
    [SerializeField] GameObject elementalSP;
    [SerializeField] GameObject illusionSP;
    [SerializeField] GameObject summoningSP;
    [SerializeField] GameObject timeSP;

    public HashSet<string> hashSpellPieces;
    public Dictionary<string, int> slotPieces;

    Player localPlayer;
    
    void Start()
    {
        hashSpellPieces = new HashSet<string>();
        slotPieces = new Dictionary<string, int>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        HasChanged();
        
        // set text of all these to the number that player has collected
        alchemySP.transform.GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces[alchemySP.name].ToString();
        arcaneSP.transform.GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces[arcaneSP.name].ToString();
        elementalSP.transform.GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces[elementalSP.name].ToString();
        illusionSP.transform.GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces[illusionSP.name].ToString();
        summoningSP.transform.GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces[summoningSP.name].ToString();
        timeSP.transform.GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces[timeSP.name].ToString();
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
            localPlayer.Spellcaster.chapter.CompareSpells(localPlayer.Spellcaster, slotPieces);
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
        slotPieces.Clear();
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

    // if the scene is changed while spell pieces are still in slots, return them to player's inventory
    void OnDestroy()
    {
        foreach (Transform slotTransform in slots)
        {
            if (slotTransform.childCount > 0)
            {
                localPlayer.Spellcaster.spellPieces[slotTransform.GetChild(0).name] += 1;
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
