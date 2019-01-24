using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because an alchemist is a type of spellcaster
  it should have spellcaster characteristics".

  Alchemist also has it's own unique features.
     */
public class Alchemist : SpellCaster
{
    public Alchemist()
    {
        
        //You can override variables in here.
        //Example:
        // numSpellPieces = 5;
        classType = "Alchemist";
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
