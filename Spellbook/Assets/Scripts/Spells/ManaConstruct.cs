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

        requiredPieces.Add("Alchemy D Spell Piece", 1);

        requiredGlyphs.Add("Alchemy D Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Alchemy D Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            PanelManager panelManager = GameObject.Find("ScriptContainer").GetComponent<PanelManager>();
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Alchemy D Glyph"] -= 4;
            string collectedGlyph = player.CollectRandomGlyph(player);

            panelManager.ShowPanel();
            panelManager.SetPanelText("You spent " + iManaCost + " mana and created a " + collectedGlyph + "."); 
        }
    }
}
