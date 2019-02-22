using UnityEngine;

// example spell for Alchemy class
public class ManaConstruct : Spell
{
    public ManaConstruct()
    {
        sSpellName = "Mana Construct";
        iTier = 3;
        iManaCost = 700;
        sSpellClass = "Alchemist";

        requiredGlyphs.Add("Alchemy D Spell Piece", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Alchemy D Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Alchemy D Glyph"] -= 4;
            string collectedGlyph = player.CollectRandomGlyph();

            PanelHolder.instance.displayNotify("You spent " + iManaCost + " mana and created a " + collectedGlyph + ".");
        }
        else if (player.glyphs["Alchemy D Glyph"] < 4)
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
    }
}
