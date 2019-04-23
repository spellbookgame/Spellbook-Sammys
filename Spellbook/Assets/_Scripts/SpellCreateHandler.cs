using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* namespace / HasChanged() written by Kiwasi Games
 * this script creates a builder that builds strings of item 
 * names as they are dropped into slots 
 */
public class SpellCreateHandler : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] public Text inventoryText;
    [SerializeField] public GameObject panel;
    [SerializeField] private GameObject glyphPieceContainer;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button backButton;
    
    public Dictionary<string, int> slotPieces;
    private RectTransform panelRect;
    private float fWidth;
    private int iSlotCount;
    private bool spellCollected;

    Player localPlayer;
    
    void Start()
    {
        // setting onClick function for button
        mainButton.onClick.AddListener(() => 
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });

        fWidth = 610f;
        iSlotCount = 0;

        slotPieces = new Dictionary<string, int>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        panelRect = panel.GetComponent<RectTransform>();

        HasChanged();

        GenerateSpellSlots();
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
            CompareSpells();
        }
    }

    // iterates through each slot and deletes child
    public void RemovePrefabs(bool collected)
    {
        // remove slot children
        foreach(Transform slotTransform in slots)
        {
            if(slotTransform.childCount > 0)
            {
                Destroy(slotTransform.GetChild(0).gameObject);
                // if the spell wasn't collected, return the glyphs to player's inventory
                if (!collected)
                {
                    localPlayer.Spellcaster.glyphs[slotTransform.GetChild(0).name] += 1;
                    inventoryText.text = "This spell was NOT created.";
                }
            }
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
                localPlayer.Spellcaster.glyphs[slotTransform.GetChild(0).name] += 1;
            }
        }
    }

    private void GenerateSpellSlots()
    {
        // for each glyph player has, child its spell slot to panel
        foreach(KeyValuePair<string, int> kvp in localPlayer.Spellcaster.glyphs)
        {
            if(kvp.Value > 0)
            {
                string glyphName = kvp.Key;
                Transform slotTransform = glyphPieceContainer.transform.Find(glyphName + " Slot");
                slotTransform.SetParent(panel.transform);
                slotTransform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.glyphs[glyphName].ToString();
                slotTransform.GetChild(0).gameObject.AddComponent<DragHandler>();
                ++iSlotCount;
            }
        }
        
        // if there are more than 4 different glyphs in the panel, resize panel to fit all slots
        if(iSlotCount > 4)
        {
            for (int i = 4; i < iSlotCount; ++i)
            {
                panelRect.sizeDelta = new Vector2((float)panelRect.sizeDelta.x + 250, panelRect.sizeDelta.y);
            }
        }
    }

    /* called in SpellCreateHandler.cs
     * compares 2 dictionaries (requiredPieces and slotPieces)
     * if they match, calls CollectSpell in SpellCaster.cs
     */
    public void CompareSpells()
    {
        bool equal = false;

        Dictionary<string, int> dictionary1 = slotPieces;

        // iterate through each spell that player can collect
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsAllowed.Count; ++i)
        {
            Dictionary<string, int> dictionary2 = localPlayer.Spellcaster.chapter.spellsAllowed[i].requiredRunes;

            // checking for tier 2 and tier 1 spells
            if (localPlayer.Spellcaster.chapter.spellsAllowed[i].iTier == 2 || localPlayer.Spellcaster.chapter.spellsAllowed[i].iTier == 1)
            {
                foreach (KeyValuePair<string, int> kvp in dictionary2)
                {
                    if (dictionary1.ContainsKey(kvp.Key))
                    {
                        equal = true;
                    }
                    else
                    {
                        equal = false;
                        spellCollected = false;
                        break;
                    }
                }
                if (equal && !localPlayer.Spellcaster.chapter.spellsCollected.Contains(localPlayer.Spellcaster.chapter.spellsAllowed[i]))
                {
                    spellCollected = localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[i]);
                    break;
                }
            }
            // tier 3 spells: only need 1 required piece
            else if (localPlayer.Spellcaster.chapter.spellsAllowed[i].iTier == 3)
            {
                if (dictionary2.Keys.All(k => dictionary1.ContainsKey(k)))
                {
                    equal = true;
                    // if equal and player does not have the spell yet
                    if (equal && !localPlayer.Spellcaster.chapter.spellsCollected.Contains(localPlayer.Spellcaster.chapter.spellsAllowed[i]))
                    {
                        // collect the spell and remove the glyphs from the circle
                        spellCollected = localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[i]);
                        break;
                    }
                    else
                        spellCollected = false;
                }
            }
        }
        RemovePrefabs(spellCollected);
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged: IEventSystemHandler
    {
        void HasChanged();
    }
}
