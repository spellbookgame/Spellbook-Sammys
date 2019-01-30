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
    [SerializeField] GameObject arcaneSP;
    [SerializeField] GameObject alchemySP;
    [SerializeField] GameObject chronomancySP;
    [SerializeField] GameObject elementalSP;
    [SerializeField] GameObject tricksterSP;
    [SerializeField] GameObject summonerSP;
    [SerializeField] public GameObject panel;

    private bool bSpellCreated;
    public HashSet<string> hashSpellPieces;

    public List<GameObject> generatedObjects;
    public GameObject g0;
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;

    Player localPlayer;
    
    void Start()
    {
        bSpellCreated = false;

        hashSpellPieces = new HashSet<string>();
        generatedObjects = new List<GameObject>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        HasChanged();
        
        // populate panel with spell pieces depending on how many of each that player has
        // add generated prefab into List<GameObject> to be referenced later
        if (panel != null)
        {
            foreach(string s in localPlayer.Spellcaster.dspellPieces.Keys)
            {
                switch(s)
                {
                    case "Alchemy Spell Piece":
                        for(int i = 0; i < (int)localPlayer.Spellcaster.dspellPieces["Alchemy Spell Piece"]; ++i)
                        {
                            g0 = (GameObject)Instantiate(alchemySP);
                            generatedObjects.Add(g0);
                            Debug.Log("Added " + g0.name + " to generated objects list.");
                            g0.transform.SetParent(panel.transform, false);
                        }
                        break;
                    case "Arcane Spell Piece":
                        for (int i = 0; i < (int)localPlayer.Spellcaster.dspellPieces["Arcane Spell Piece"]; ++i)
                        {
                            g1 = (GameObject)Instantiate(arcaneSP);
                            generatedObjects.Add(g1);
                            Debug.Log("Added " + g1.name + " to generated objects list.");
                            g1.transform.SetParent(panel.transform, false);
                        }
                        break;
                    case "Elemental Spell Piece":
                        for (int i = 0; i < (int)localPlayer.Spellcaster.dspellPieces["Elemental Spell Piece"]; ++i)
                        {
                            g2 = (GameObject)Instantiate(elementalSP);
                            generatedObjects.Add(g2);
                            Debug.Log("Added " + g2.name + " to generated objects list.");
                            g2.transform.SetParent(panel.transform, false);
                        }
                        break;
                    case "Illusion Spell Piece":
                        for (int i = 0; i < (int)localPlayer.Spellcaster.dspellPieces["Illusion Spell Piece"]; ++i)
                        {
                            g3 = (GameObject)Instantiate(tricksterSP);
                            generatedObjects.Add(g3);
                            Debug.Log("Added " + g3.name + " to generated objects list.");
                            g3.transform.SetParent(panel.transform, false);
                        }
                        break;
                    case "Summoning Spell Piece":
                        for (int i = 0; i < (int)localPlayer.Spellcaster.dspellPieces["Summoning Spell Piece"]; ++i)
                        {
                            g4 = (GameObject)Instantiate(summonerSP);
                            generatedObjects.Add(g4);
                            Debug.Log("Added " + g4.name + " to generated objects list.");
                            g4.transform.SetParent(panel.transform, false);
                        }
                        break;
                    case "Time Spell Piece":
                        for (int i = 0; i < (int)localPlayer.Spellcaster.dspellPieces["Time Spell Piece"]; ++i)
                        {
                            g5 = (GameObject)Instantiate(chronomancySP);
                            generatedObjects.Add(g5);
                            Debug.Log("Added " + g5.name + " to generated objects list.");
                            g5.transform.SetParent(panel.transform, false);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
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

    // removes item from generatedObjects list and destroys prefab
    // BUG: this destroys all duplicates of the prefab (only the item, not the slot)
    public void RemovePrefabs(Spell spell)
    {
        // remove slots in scrolling panel
        foreach (string s in spell.requiredPiecesList)
        {
            switch(s)
            {
                case "Alchemy Spell Piece":
                    Destroy(generatedObjects[generatedObjects.IndexOf(g0)]);
                    generatedObjects.Remove(g0);
                    break;
                case "Arcane Spell Piece":
                    Destroy(generatedObjects[generatedObjects.IndexOf(g1)]);
                    generatedObjects.Remove(g1);
                    break;
                case "Elemental Spell Piece":
                    Destroy(generatedObjects[generatedObjects.IndexOf(g2)]);
                    generatedObjects.Remove(g2);
                    break;
                case "Illusion Spell Piece":
                    Destroy(generatedObjects[generatedObjects.IndexOf(g3)]);
                    generatedObjects.Remove(g3);
                    break;
                case "Summoning Spell Piece":
                    Destroy(generatedObjects[generatedObjects.IndexOf(g4)]);
                    generatedObjects.Remove(g4);
                    break;
                case "Time Spell Piece":
                    Destroy(generatedObjects[generatedObjects.IndexOf(g5)]);
                    generatedObjects.Remove(g5);
                    break;
                default:
                    break;
            }
        }
        Debug.Log("Generated Objects size: " + generatedObjects.Count);
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
