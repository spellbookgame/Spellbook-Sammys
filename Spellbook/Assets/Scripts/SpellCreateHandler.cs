using System.Collections;
using System.Collections.Generic;
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
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged: IEventSystemHandler
    {
        void HasChanged();
    }
}
