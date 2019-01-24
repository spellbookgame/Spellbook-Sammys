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
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
