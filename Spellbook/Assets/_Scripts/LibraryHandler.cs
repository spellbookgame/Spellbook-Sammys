using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LibraryHandler : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Sprite combatIcon;
    [SerializeField] private Sprite nonCombatIcon;
    [SerializeField] private GameObject spellButtonPrefab;
    [SerializeField] private GameObject UIScrollable;
    [SerializeField] private GameObject runeContainer;
    [SerializeField] private GameObject spellInfoPanel;
    [SerializeField] private GameObject runePanel;

    [SerializeField] private Color tier3Color;
    [SerializeField] private Color tier2Color;
    [SerializeField] private Color tier1Color;

    Player localPlayer;
    private UIScrollableController scrollController;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        scrollController = UIScrollable.GetComponent<UIScrollableController>();

        // adding onClick listener for UI buttons
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
            SceneManager.LoadScene("MainPlayerScene");
        });

        settingsButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
            SceneManager.LoadScene("SettingsScene");
        });

        foreach (Spell s in localPlayer.Spellcaster.chapter.spellsAllowed)
        {
            GameObject spellButton = Instantiate(spellButtonPrefab);
            UISpellButtonController buttonController = spellButton.GetComponent<UISpellButtonController>();
            buttonController.SetTitle(s.sSpellName);
            buttonController.SetText(s.sSpellInfo);

            // set button icons
            if (s.combatSpell)
                buttonController.SetIcon(combatIcon);
            else
                buttonController.SetIcon(nonCombatIcon);

            // set button colors
            if (s.iTier == 3)
                buttonController.SetColor(tier3Color);
            else if (s.iTier == 2)
                buttonController.SetColor(tier2Color);
            else
                buttonController.SetColor(tier1Color);

            scrollController.AddElement(spellButton);
            spellButton.GetComponent<Button>().onClick.AddListener(() => ShowSpellInfo(s));
        }

        // set panel holder as last sibling
        PanelHolder.instance.SetPanelHolderLast();
    }

    private void ShowSpellInfo(Spell spell)
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        // if the panel still has runes in it, return them to the rune container
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
        
        // add rune images to the panel to show player required runes
        foreach (KeyValuePair<string, int> kvp in spell.requiredRunes)
        {
            GameObject runeImage = runeContainer.transform.Find(kvp.Key).gameObject;
            runeImage.transform.SetParent(runePanel.transform, false);  // false means object won't scale with Parent
        }

        spellInfoPanel.SetActive(true);
    }
}
