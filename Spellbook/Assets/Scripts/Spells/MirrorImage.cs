using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Trickster class
public class MirrorImage : Spell
{
    public MirrorImage()
    {
        sSpellName = "Mirror Image";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Trickster";

        requiredPieces.Add("Illusion Spell Piece", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        Debug.Log(sSpellName + " was cast!");
        player.iMana -= iManaCost;
    }
}
