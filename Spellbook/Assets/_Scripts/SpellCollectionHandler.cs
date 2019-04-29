using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellCollectionHandler : MonoBehaviour
{
    [SerializeField] private Button spellButton;
    [SerializeField] private Button castButton;
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private Text noSpellsText;

    private bool spellPanelOpen;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        if (localPlayer.Spellcaster.chapter.spellsCollected.Count > 0)
            noSpellsText.text = "";

        int yPos = 105;
        // add buttons for each spell the player has collected
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsCollected.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton, GameObject.Find("Canvas").transform);
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsCollected[i].sSpellName;
            newSpellButton.transform.localPosition = new Vector3(0, yPos, 0);
            newSpellButton.transform.SetAsFirstSibling();

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => OpenSpellPanel(localPlayer.Spellcaster.chapter.spellsCollected[i2]));

            // to position new button underneath prev button
            yPos -= 250;
        }
    }

    private void Update()
    {
        // disable spell casting if it's not player's turn
        if(!localPlayer.bIsMyTurn)
        {
            castButton.interactable = false;
        }
    }

    private void OpenSpellPanel(Spell spell)
    {
        SoundManager.instance.PlaySingle(SoundManager.spellbookopen);

        // set spell name and info
        spellPanel.transform.Find("text_spellname").GetComponent<Text>().text = spell.sSpellName;
        spellPanel.transform.Find("text_spellinfo").GetComponent<Text>().text = "Cost: " + spell.iManaCost + "\n\n" + spell.sSpellInfo;

        // add onclick listener to close button
        spellPanel.transform.Find("button_exit").GetComponent<Button>().onClick.AddListener(CloseSpellPanel);

        // add onclick listener to cast button
        spellPanel.transform.Find("button_cast").GetComponent<Button>().onClick.AddListener(() =>
        {
            spell.SpellCast(localPlayer.Spellcaster);
            CloseSpellPanel();
        });

        spellPanel.SetActive(true);
        spellPanelOpen = true;
    }
    
    private void CloseSpellPanel()
    {
        if(spellPanelOpen == true)
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            spellPanel.SetActive(false);
            spellPanelOpen = false;
        }
    }
}
