using UnityEngine;
using UnityEngine.SceneManagement;

// example spell for Elementalist class
public class Fireball : Spell
{
    public Fireball()
    {
        sSpellName = "Fireball";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Elementalist";

        requiredPieces.Add("Elemental D Spell Piece", 1);

        requiredGlyphs.Add("Elemental D Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Elemental D Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            int damage = Random.Range(2, 12);
            enemy.HitEnemy(damage);

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Elemental D Glyph"] -= 4;

            PanelHolder.instance.displayCombat("You cast Fireball and it did " + damage + " damage!");
        }
        else if (player.glyphs["Elemental D Glyph"] < 4)
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
    }
}
