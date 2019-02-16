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
            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Illusion B Glyph"] -= 4;
            
            PanelHolder.instance.displayNotify(sSpellName + " was cast. You may control your next roll to be a 1, 2, 3, 4, 5, or 6.");

            player.activeSpells.Add(sSpellName);
        }
        else if (player.glyphs["Illusion B Glyph"] < 4)
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
    }
}
