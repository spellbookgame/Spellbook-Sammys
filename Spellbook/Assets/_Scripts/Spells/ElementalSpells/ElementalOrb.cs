using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Elementalist class
public class ElementalOrb : Spell
{
    public ElementalOrb()
    {
        iTier = 2;
        iManaCost = 700;
        iCoolDown = 1;

        sSpellName = "Elemental Orb";
        sSpellClass = "Elementalist";
        sSpellInfo = "Create an orb of all elements combined. It will deal 4 damage per elemental glyph you own.";

        requiredGlyphs.Add("Elemental B Glyph", 1);
        requiredGlyphs.Add("Arcane B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        // calculating number of elemental glyphs
        int elementGlyphCount = 0;
        elementGlyphCount += player.glyphs["Elemental A Glyph"];
        elementGlyphCount += player.glyphs["Elemental B Glyph"];
        elementGlyphCount += player.glyphs["Elemental C Glyph"];
        elementGlyphCount += player.glyphs["Elemental D Glyph"];

        int damage = elementGlyphCount * 4;
        enemy.HitEnemy(damage);
        PanelHolder.instance.displayNotify("You cast " + sSpellName, "It did " + damage + " damage!", "OK");
    }
}
