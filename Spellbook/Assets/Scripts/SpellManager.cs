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
    [SerializeField] Text inventoryText;
    [SerializeField] GameObject arcaneSP;
    [SerializeField] GameObject alchemySP;
    [SerializeField] GameObject chronomancySP;
    [SerializeField] GameObject elementalSP;
    [SerializeField] GameObject tricksterSP;
    [SerializeField] GameObject summonerSP;
    [SerializeField] GameObject panel;

    private bool bSpellCreated;
    private HashSet<string> hashSpellPieces;

    Player localPlayer;
    SlotHandler slotHandler;
    ArcaneBlast aBlast1 = new ArcaneBlast();
    
    void Start()
    {
        bSpellCreated = false;

        hashSpellPieces = new HashSet<string>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        slotHandler = GameObject.Find("Slot").GetComponent<SlotHandler>();
        HasChanged();
        
        // populate panel with spell pieces depending on how many of each that player has
        if (panel != null)
        {
            while (localPlayer.Spellcaster.iArcanePieces > 0)
            {
                GameObject g0 = (GameObject)Instantiate(arcaneSP);
                g0.transform.SetParent(panel.transform, false);
                localPlayer.Spellcaster.iArcanePieces--;
            }
            while (localPlayer.Spellcaster.iAlchemyPieces > 0)
            {
                GameObject g0 = (GameObject)Instantiate(alchemySP);
                g0.transform.SetParent(panel.transform, false);
                localPlayer.Spellcaster.iAlchemyPieces--;
            }
            while (localPlayer.Spellcaster.iChronomancyPieces > 0)
            {
                GameObject g0 = (GameObject)Instantiate(chronomancySP);
                g0.transform.SetParent(panel.transform, false);
                localPlayer.Spellcaster.iChronomancyPieces--;
            }
            while (localPlayer.Spellcaster.iElementalPieces > 0)
            {
                GameObject g0 = (GameObject)Instantiate(elementalSP);
                g0.transform.SetParent(panel.transform, false);
                localPlayer.Spellcaster.iElementalPieces--;
            }
            while (localPlayer.Spellcaster.iSummoningPieces > 0)
            {
                GameObject g0 = (GameObject)Instantiate(summonerSP);
                g0.transform.SetParent(panel.transform, false);
                localPlayer.Spellcaster.iSummoningPieces--;
            }
            while (localPlayer.Spellcaster.iTricksterPieces > 0)
            {
                GameObject g0 = (GameObject)Instantiate(tricksterSP);
                g0.transform.SetParent(panel.transform, false);
                localPlayer.Spellcaster.iTricksterPieces--;
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

    // eventually this function should be transferred into Chapter.cs, and called in Update(?)
    // this function only checks for arcane blast as of now
    public void CheckSpellSlots()
    {
        // if all slots are filled up
        if(hashSpellPieces.Count == aBlast1.requiredPieces.Count)
        {
            // if the two hashsets match
            if (hashSpellPieces.SetEquals(aBlast1.requiredPieces))
            {
                // add the spell to player's chapter
                localPlayer.Spellcaster.CollectSpell(aBlast1, localPlayer.Spellcaster);
                bSpellCreated = true;
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
