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

    public Combat spellSwiper;
    public GameObject ChargePanel;
    public Button EquipedSpellButton;
    public SpriteRenderer swipeGuide;
    SpellCaster localSpellcaster;

    Image gemImage;
    public Gradient colorGrade;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    // Start is called before the first frame update
    void Start()
    {
        ReadyButton.interactable = false;
        //Todo get the spellcasters spells.
        //BoltConsole.print("player = " + p);
        spells = new Spell[] { spell1, spell2, spell3 };
        spellButtons = new Button[] { SpellButton1, SpellButton2, SpellButton3 };

        localSpellcaster = spellSwiper.localSpellcaster;
        spellSwiper.PlayerHealthBar.GetComponent<UIHealthbarController>().healthPercentage = localSpellcaster.fCurrentHealth / localSpellcaster.fMaxHealth;
        int i = 0;
        foreach (Spell entry in localSpellcaster.chapter.spellsCollected)
        //foreach (KeyValuePair<string, Spell> entry in localSpellcaster.combatSpells)
        {
            if (!entry.combatSpell) continue;
            if (i >= 3) break;

            spells[i] = entry;
            Color c1 = entry.colorPrimary;
            Color c2 = entry.colorSecondary;
            Color c3 = entry.colorTertiary;

            GradientColorKey[] colorKeyGem = new GradientColorKey[2];
            colorKeyGem[0].color = c1;
            colorKeyGem[0].time = 0.0f;

            colorKeyGem[1].color = c2;
            colorKeyGem[1].time = 0.0f;

            GradientColorKey[] colorKeyBut = new GradientColorKey[2];
            colorKeyBut[0].color = c1;
            colorKeyBut[0].time = 0.0f;

            colorKeyBut[1].color = c2;
            colorKeyBut[1].time = 0.0f;


            GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.75f;
            alphaKey[1].time = 1.0f;

            spellButtons[i].GetComponent<UIAutoColorSprite>().colorGrade.SetKeys(colorKeyGem, alphaKey);
            spellButtons[i].GetComponent<UIAutoColorImage>().colorGrade.SetKeys(colorKeyBut, alphaKey);
            i++;
        }
        SpellButton1.onClick.AddListener(clickedSpellButton1);
        SpellButton2.onClick.AddListener(clickedSpellButton2);
        SpellButton3.onClick.AddListener(clickedSpellButton3);
        ReadyButton.onClick.AddListener(clickedReady);

    }

    private void clickedSpellButton1()
    {
        if (SelectedSpellButton != null)
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
        if (SelectedSpellButton != null)
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
        if (SelectedSpellButton != null)
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
        spellSwiper.selectedSpell = selectedSpell;
        spellSwiper.localSpellcaster = localSpellcaster;
        ChargePanel.GetComponent<ChargeSpell>().SetCombatSpell(selectedSpell, SelectedSpellButton, localSpellcaster);
        this.gameObject.SetActive(false);
    }


    public void DecorateSpellButton(Color c1, Color c2, Color c3)
    {
        Debug.Log(c1.ToString());
        Debug.Log(c2.ToString());
        Debug.Log(c3.ToString());
        Debug.Log(c3.r + "");
        gemImage.color = c3;

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = c1;
        //colorKey[0].time = 0.0f;
        colorKey[1].color = c2;

        //colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.7f;
        alphaKey[1].time = 1.0f;

        colorGrade.SetKeys(colorKey, alphaKey);

        // What's the color at the relative time 0.25 (25 %) ?
        //Debug.Log(gradient.Evaluate(0.25f));
    }


}
