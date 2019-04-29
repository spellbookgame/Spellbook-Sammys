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

        int yPos = 475;
        // add buttons for each spell the player can collect
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsAllowed.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton, GameObject.Find("Canvas").transform);
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsAllowed[i].sSpellName;
            newSpellButton.transform.localPosition = new Vector3(0, yPos, 0);

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => ShowSpellInfo(localPlayer.Spellcaster.chapter.spellsAllowed[i2]));

            // to position new button underneath prev button
            yPos -= 250;
        }
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
        spellInfoPanel.transform.GetChild(1).GetComponent<Text>().text = "Tier: " + spell.iTier.ToString() + "  |  Cost: " + spell.iManaCost.ToString();
        spellInfoPanel.transform.GetChild(2).GetComponent<Text>().text = spell.sSpellInfo;
        
        // add glyph images to the panel to show player required glyphs
        foreach (KeyValuePair<string, int> kvp in spell.requiredRunes)
        {
            GameObject runeImage = runeContainer.transform.Find(kvp.Key).gameObject;
            runeImage.transform.SetParent(runePanel.transform, false);  // false means object won't scale with Parent
        }
    }
}
