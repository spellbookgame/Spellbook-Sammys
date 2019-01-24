using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because a Trickster is a type of spellcaster
  it should have spellcaster characteristics".

  Trickster also has it's own unique features.
     */
public class Trickster : SpellCaster
{
    public Trickster()
    {
        
        //You can override variables in here.
        //Example:
        numSpellPieces = 5;
        classType = "Trickster";
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
