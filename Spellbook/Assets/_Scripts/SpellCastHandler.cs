using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Written by Grace Ko
/// Similar to SpellCreateHandler.cs,
/// but tailored to spell casting
/// </summary>
public class SpellCastHandler : MonoBehaviour
{
    [SerializeField] private Button spellButton;
    [SerializeField] private Button selectedSpell;
    [SerializeField] private Button closePanelButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button backButton;
    [SerializeField] public GameObject panel;
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private GameObject glyphPieceContainer;
    [SerializeField] private Transform slots;

    private int iSlotCount;
    private bool spellWasCast;
    private bool spellPanelOpen;
    private RectTransform panelRect;
    private Spell currentSpell;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        iSlotCount = 0;
        currentSpell = null;
        panelRect = panel.GetComponent<RectTransform>();

        // adding onclick listeners to UI buttons
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
        selectedSpell.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookopen);
            OpenClosePanel();
        });
        closePanelButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookopen);
            OpenClosePanel();
        });

        // add glyph slots to the upper panel
        GenerateSpellSlots();

        // add buttons for each spell the player has collected
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsCollected.Count; i++)
        {
            // get the button from spell panel
            Button newSpellButton = spellPanel.transform.GetChild(0).GetChild(i).GetComponent<Button>();

            // set its text to spell's name
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsCollected[i].sSpellName;

            // helper variable to pass in incremental value
            int i2 = i;
            // add onclick listener to button
            newSpellButton.onClick.AddListener(() => SpellButtonClicked(localPlayer.Spellcaster.chapter.spellsCollected[i2]));
        }
    }

    private void Update()
    {
        // checking to see if each slot is filled
        int i = 0;
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            if (item)
            {
                ++i;
            }
        }
        // if all slots are filled, call the SpellCast() function
        if (i >= 4)
        {
            // check if player has enough mana
            if (localPlayer.Spellcaster.iMana < currentSpell.iManaCost)
            {
                PanelHolder.instance.displayNotify("Not enough mana!", "You don't have enough mana to cast this spell.", "OK");
                spellWasCast = false;
                RemovePrefabs(spellWasCast);
            }
            else
            {
                currentSpell.SpellCast(localPlayer.Spellcaster);
                SoundManager.instance.PlaySingle(SoundManager.spellcast);
                spellWasCast = true;
                RemovePrefabs(spellWasCast);
            }
        }
    }

    // removes all glyphs from casting circle once the spell is cast
    public void RemovePrefabs(bool spellWasCast)
    {
        // remove slot children
        foreach (Transform slotTransform in slots)
        {
            // if there is a glyph in the current slot
            if(slotTransform.childCount > 0)
            {
                Destroy(slotTransform.GetChild(0).gameObject);
                // if the spell wasn't cast, return the glyphs to player's inventory
                if (!spellWasCast)
                {
                    localPlayer.Spellcaster.glyphs[slotTransform.GetChild(0).name] += 1;
                }
            }
        }
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

    // generates the glyphs in the upper panel
    private void GenerateSpellSlots()
    {
        // for each glyph player has, child its spell slot to panel
        foreach(KeyValuePair<string, int> kvp in localPlayer.Spellcaster.glyphs)
        {
            if (kvp.Value > 0)
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
        if (iSlotCount > 4)
        {
            for (int i = 4; i < iSlotCount; ++i)
            {
                panelRect.sizeDelta = new Vector2((float)panelRect.sizeDelta.x + 250, panelRect.sizeDelta.y);
            }
        }
    }

    // panel handler to open and close spell select panel
    private void OpenClosePanel()
    {
        if(!spellPanelOpen)
        {
            spellPanel.SetActive(true);
            spellPanelOpen = true;
        }
        else
        {
            spellPanel.SetActive(false);
            spellPanelOpen = false;
        }
    }

    // when the button is clicked, add its required glyphs into the casting circle
    private void SpellButtonClicked(Spell spell)
    {
        // open up spell panel
        OpenClosePanel();
        // change selectedSpell button text to spell name
        selectedSpell.GetComponentInChildren<Text>().text = spell.sSpellName;

        // update current spell
        currentSpell = spell;
        // remove any glyphs from spell casting circle
        spellWasCast = false;
        RemovePrefabs(spellWasCast);
        StartCoroutine("WaitALittle", spell);
    }

    // creates a delay before populating circle
    IEnumerator WaitALittle(Spell spell)
    {
        yield return new WaitForSeconds(0.1f);
        PopulateCircle(spell);
    }

    // add required glyphs into the caster's circle
    private void PopulateCircle(Spell spell)
    {
        foreach (KeyValuePair<string, int> kvp in spell.requiredRunes)
        {
            // if player doesn't have this glyph in the inventory, notify them.
            if (localPlayer.Spellcaster.glyphs[kvp.Key] <= 0)
            {
                PanelHolder.instance.displayNotify("Not enough glyphs!", "You do not have enough glyphs to cast this spell.", "OK");
            }
            // else, add that glyph to the casting circle
            else
            {
                // find the glyph in the panel
                Transform glyphSlot = panel.transform.Find(kvp.Key + " Slot");
                Transform glyphObject = glyphSlot.GetChild(0);
                // change its parent to be the first available slot in casting circle
                foreach (Transform slotTransform in slots)
                {
                    if (slotTransform.childCount <= 0)
                    {
                        glyphObject.SetParent(slotTransform);
                        break;
                    }
                }
                // destroy its text object
                if (glyphObject.childCount > 0)
                {
                    Destroy(glyphObject.GetChild(0).gameObject);
                }
                // add a clone of the glyph and set its parent to its slot
                GameObject clone = Instantiate((GameObject)Resources.Load("Glyphs/" + kvp.Key), glyphSlot);
                clone.name = kvp.Key;
                clone.AddComponent<DragHandler>();
                // decrement its count in the player's inventory
                localPlayer.Spellcaster.glyphs[kvp.Key] -= 1;
            }
        }
    }
}
