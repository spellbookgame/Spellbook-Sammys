using UnityEngine;

// example spell for Chronomancy class
public class Accelerate : Spell
{
    public Accelerate()
    {
        sSpellName = "Accelerate";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Chronomancer";

        requiredPieces.Add("Time C Spell Piece", 1);

        requiredGlyphs.Add("Time C Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        PanelManager panelManager = GameObject.Find("ScriptContainer").GetComponent<PanelManager>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Time C Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Time C Glyph"] -= 4;

            panelManager.ShowPanel();
            panelManager.SetPanelText("You cast Accelerate. Your next move dice will roll a five or a six.");

            player.activeSpells.Add(sSpellName);
        }
        else if (player.glyphs["Time C Glyph"] < 4)
        {
            panelManager.ShowPanel();
            panelManager.SetPanelText("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            panelManager.ShowPanel();
            panelManager.SetPanelText("You don't have enough mana to cast this spell.");
        }
    }
}
