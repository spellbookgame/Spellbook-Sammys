using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because an Arcanist is a type of spellcaster
  it should have spellcaster characteristics".

  Arcanist also has it's own unique features.
     */
public class Arcanist : SpellCaster
{
    public Arcanist()
    {
        //You can override variables in here.
        //Example:
        // numSpellPieces = 5;
        classType = "Arcanist";
        fMaxHealth = 20.0f;

        // adding arcane blast to chapter
        Spell aBlast = new ArcaneBlast();
        chapter.spells.Add(aBlast);

        // testing
        for (int i = 0; i < aBlast.requiredPieces.Count; i++)
            Debug.Log(aBlast.requiredPieces[i]);
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
