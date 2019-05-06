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

    Spell spell1;
    Spell spell2;
    Spell spell3;

    Spell[] spells;
    Button[] spellButtons;

    Color colorGemstone1;
    Color colorGemstone2;
    Color colorGemstone3;
    public Text SpellDescription;

    public GameObject ChargePanel;
    public Button EquipedSpellButton;
    public SpriteRenderer swipeGuide1;
    public SpriteRenderer swipeGuide2;
    public SpriteRenderer swipeGuide3;
    SpellCaster localSpellcaster;
    // Start is called before the first frame update
    void Start()
    {
        //Todo get the spellcasters spells.
        GameObject p =  GameObject.FindGameObjectWithTag("LocalPlayer");
        spells = new Spell[] { spell1, spell2, spell3 };
        spellButtons = new Button[]{SpellButton1, SpellButton2, SpellButton3};
        if (p != null)
        {
            localSpellcaster = p.GetComponent<Player>().Spellcaster;
            /*Prepare Spell Buttons**/


        }
        else
        {
            localSpellcaster = new Alchemist();
            //localSpellcaster.CollectSpell(new ToxicPotion());
        }

        int i = 0;
        foreach(KeyValuePair<string, Spell> entry in localSpellcaster.combatSpells)
        {
            spells[i] = entry.Value;
            Color c1 = entry.Value.colorPrimary;
            Color c2 = entry.Value.colorSecondary;
            spellButtons[i++].GetComponent<UIAutoColor>().DecorateSpellButton(c1, c2);
        }
        ColorUtility.TryParseHtmlString(localSpellcaster.hexStringPanel, out colorGemstone1);;
        SpellButton1.image.color = colorGemstone1;        
        ColorUtility.TryParseHtmlString(localSpellcaster.hexStringLight, out colorGemstone2);;
        SpellButton2.image.color = colorGemstone2;        
        ColorUtility.TryParseHtmlString(localSpellcaster.hexString3rdColor, out colorGemstone3);;
        SpellButton3.image.color = colorGemstone3;        
        SpellButton1.onClick.AddListener(clickedSpellButton1);
        SpellButton2.onClick.AddListener(clickedSpellButton2);
        SpellButton3.onClick.AddListener(clickedSpellButton3);
        ReadyButton.onClick.AddListener(clickedReady);

    }

    private void clickedSpellButton1()
    {
        SpellDescription.text = spells[0].sSpellInfo;
        swipeGuide1.gameObject.SetActive(true);
        var tColor = SpellButton1.image.color;
        tColor.a = 0.5f;
        SpellButton1.image.color = tColor;
    }

    private void clickedSpellButton2()
    {
        SpellDescription.text = spells[1].sSpellInfo;
        swipeGuide2.gameObject.SetActive(true);
        var tColor = SpellButton2.image.color;
        tColor.a = 0.5f;
        SpellButton2.image.color = tColor;
    }

    private void clickedSpellButton3()
    {
        SpellDescription.text = spells[2].sSpellInfo;
        swipeGuide3.gameObject.SetActive(true);
        var tColor = SpellButton3.image.color;
        tColor.a = 0.5f;
        SpellButton3.image.color = tColor;
    }

    private void clickedReady()
    {
        ChargePanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
