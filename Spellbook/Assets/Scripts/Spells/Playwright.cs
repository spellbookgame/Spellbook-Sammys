using UnityEngine;

// example spell for Trickster class
public class Playwright : Spell
{
    public Playwright()
    {
        sSpellName = "Playwright";
        iTier = 2;
        iManaCost = 400;
        sSpellClass = "Trickster";

        requiredPieces.Add("Illusion B Spell Piece", 1);
        requiredPieces.Add("Illusion C Spell Piece", 1);
        requiredPieces.Add("Time C Spell Piece", 1);

        requiredGlyphs.Add("Illusion B Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Illusion B Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            PanelManager panelManager = GameObject.Find("ScriptContainer").GetComponent<PanelManager>();
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Illusion B Glyph"] -= 4;

            panelManager.ShowPanel();
            panelManager.SetPanelText("You cast Playwright. You may control your next roll to roll a 1, 2, 3, 4, 5, or 6.");

            player.activeSpells.Add(sSpellName);
        }
    }
}
