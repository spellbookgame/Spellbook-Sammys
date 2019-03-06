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
    [SerializeField] private Button mainButton;
    [SerializeField] private Button backButton;
    [SerializeField] public GameObject panel;
    [SerializeField] private GameObject glyphPieceContainer;
    [SerializeField] private Transform slots;

    private int iSlotCount;
    private RectTransform panelRect;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        iSlotCount = 0;
        panelRect = panel.GetComponent<RectTransform>();

        // adding onclick listeners to buttons
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

        int yPos = 780;
        // add buttons for each spell the player has collected
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsCollected.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton);
            newSpellButton.transform.parent = GameObject.Find("Canvas").transform;
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsCollected[i].sSpellName;
            newSpellButton.transform.localPosition = new Vector3(0, yPos, 0);

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => localPlayer.Spellcaster.chapter.spellsCollected[i2].SpellCast(localPlayer.Spellcaster));

            // to position new button underneath prev button
            yPos -= 150;
        }

        GenerateSpellSlots();
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
        foreach (KeyValuePair<string, int> kvp in localPlayer.Spellcaster.glyphs)
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
}
