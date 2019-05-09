using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LibraryHandler : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private Button spellButton;
    [SerializeField] private GameObject buttonScrollRect;
    [SerializeField] private GameObject runeContainer;
    [SerializeField] private GameObject spellInfoPanel;
    [SerializeField] private GameObject runePanel;

    Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // adding onClick listener for UI buttons
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
            SceneManager.LoadScene("MainPlayerScene");
        });

        // add buttons to scroll rect for each spell the player can collect
        foreach(Spell s in localPlayer.Spellcaster.chapter.spellsAllowed)
        {
            Button newSpellButton = Instantiate(spellButton, buttonScrollRect.transform);
            newSpellButton.GetComponentInChildren<Text>().text = s.sSpellName;

            // add listener to button
            newSpellButton.onClick.AddListener(() => ShowSpellInfo(s));
        }
        // if there are more than 4 buttons in the scroll rect, expand the scroll rect panel
        if(buttonScrollRect.transform.childCount > 4)
        {
            RectTransform rect = buttonScrollRect.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (float)rect.sizeDelta.y + (210 * (buttonScrollRect.transform.childCount - 4)));
        }

        // set panel holder as last sibling
        PanelHolder.instance.SetPanelHolderLast();
    }

    private void ShowSpellInfo(Spell spell)
    {
        // if the panel still has glyphs in it, return them to the glyph container
        while(runePanel.transform.childCount > 0)
        {
            Transform child = runePanel.transform.GetChild(0);
            child.SetParent(runeContainer.transform);
            child.localPosition = Vector3.zero;
        }

        spellInfoPanel.transform.GetChild(0).GetComponent<Text>().text = spell.sSpellName;
        string combat;
        if (spell.combatSpell)
            combat = "Combat";
        else
            combat = "Non-Combat";
        spellInfoPanel.transform.GetChild(1).GetComponent<Text>().text = "Tier: " + spell.iTier.ToString() + "  |  Cost: " + spell.iManaCost.ToString()
                                                                            + "   |   " + combat;
        spellInfoPanel.transform.GetChild(2).GetComponent<Text>().text = spell.sSpellInfo;
        
        // add glyph images to the panel to show player required glyphs
        foreach (KeyValuePair<string, int> kvp in spell.requiredRunes)
        {
            GameObject runeImage = runeContainer.transform.Find(kvp.Key).gameObject;
            runeImage.transform.SetParent(runePanel.transform, false);  // false means object won't scale with Parent
        }
    }
}
