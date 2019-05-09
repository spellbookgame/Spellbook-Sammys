using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelectionPanel : MonoBehaviour
{
    public Button SpellButton1;
    public Button SpellButton2;
    public Button SpellButton3;
    public Button ReadyButton;
    public GameObject SelectedSpellButton;

    Spell spell1;
    Spell spell2;
    Spell spell3;
    Spell selectedSpell;

    Spell[] spells;
    Button[] spellButtons;

    Color colorGemstone1;
    Color colorGemstone2;
    Color colorGemstone3;
    public Text SpellDescription;
    public Text SpellName;

    public GameObject ChargePanel;
    public Button EquipedSpellButton;
    public SpriteRenderer swipeGuide;
    SpellCaster localSpellcaster;
    // Start is called before the first frame update
    void Start()
    {
        ReadyButton.interactable = false;
        //Todo get the spellcasters spells.
        GameObject p =  GameObject.FindGameObjectWithTag("LocalPlayer");
        //BoltConsole.print("player = " + p);
        spells = new Spell[] { spell1, spell2, spell3 };
        spellButtons = new Button[]{SpellButton1, SpellButton2, SpellButton3};
        if (p != null)
        {
            localSpellcaster = p.GetComponent<Player>().Spellcaster;
            /*Prepare Spell Buttons**/


        }
        else
        {
            localSpellcaster = new Summoner();
            //localSpellcaster.CollectSpell(new ToxicPotion());
        }

        BoltConsole.Write("player = " + p.name);
        BoltConsole.Write("spellcaster = " + p.GetComponent<Player>().Spellcaster);
        int i = 0;
        foreach(KeyValuePair<string, Spell> entry in localSpellcaster.combatSpells)
        {
            BoltConsole.Write("entry = " + entry.Key);
            BoltConsole.Write("spell = " + entry.Value.sSpellName);

            spells[i] = entry.Value;
            Color c1 = entry.Value.colorPrimary;
            Color c2 = entry.Value.colorSecondary;
            Color c3 = entry.Value.colorTertiary;
            spellButtons[i++].GetComponent<UIAutoColor>().DecorateSpellButton(c1, c2, c3);
        }
        SpellButton1.onClick.AddListener(clickedSpellButton1);
        SpellButton2.onClick.AddListener(clickedSpellButton2);
        SpellButton3.onClick.AddListener(clickedSpellButton3);
        ReadyButton.onClick.AddListener(clickedReady);

    }

    private void clickedSpellButton1()
    {
        if(SelectedSpellButton != null)
        {
            SelectedSpellButton.GetComponent<RectTransform>().localScale = new Vector3(0.65024f, 0.65024f, 1f);
        }
        SpellButton1.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1f);
        ReadyButton.interactable = true;
        ReadyButton.GetComponentInChildren<Text>().text = "Ready";
        selectedSpell = spells[0];
        SelectedSpellButton = SpellButton1.gameObject;
        SpellName.text = selectedSpell.sSpellName;
        SpellDescription.text = selectedSpell.sSpellInfo;
        swipeGuide.sprite = selectedSpell.guideLine;
        var tColor = SpellButton1.image.color;
        tColor.a = 0.5f;
        SpellButton1.image.color = tColor;
    }

    private void clickedSpellButton2()
    {
        if(SelectedSpellButton != null)
        {
            SelectedSpellButton.GetComponent<RectTransform>().localScale = new Vector3(0.65024f, 0.65024f, 1f);
        }
        SpellButton2.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1f);
        ReadyButton.interactable = true;
        ReadyButton.GetComponentInChildren<Text>().text = "Ready";
        selectedSpell = spells[1];
        SelectedSpellButton = SpellButton2.gameObject;
        SpellName.text = selectedSpell.sSpellName;
        SpellDescription.text = selectedSpell.sSpellInfo;
        swipeGuide.sprite = selectedSpell.guideLine;
        var tColor = SpellButton2.image.color;
        tColor.a = 0.5f;
        SpellButton2.image.color = tColor;
    }

    private void clickedSpellButton3()
    {
        if(SelectedSpellButton != null)
        {
            SelectedSpellButton.GetComponent<RectTransform>().localScale = new Vector3(0.65024f, 0.65024f, 1f);
        }
        SpellButton3.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1f);
        ReadyButton.interactable = true;
        ReadyButton.GetComponentInChildren<Text>().text = "Ready";
        selectedSpell = spells[2];
        SelectedSpellButton = SpellButton3.gameObject;
        SpellName.text = selectedSpell.sSpellName;
        SpellDescription.text = selectedSpell.sSpellInfo;
        swipeGuide.sprite = selectedSpell.guideLine;
        var tColor = SpellButton3.image.color;
        tColor.a = 0.5f;
        SpellButton3.image.color = tColor;
    }

    private void clickedReady()
    {
        ChargePanel.SetActive(true);
        ChargePanel.GetComponent<ChargeSpell>().SetCombatSpell(selectedSpell, SelectedSpellButton);
        this.gameObject.SetActive(false);
    }


}
