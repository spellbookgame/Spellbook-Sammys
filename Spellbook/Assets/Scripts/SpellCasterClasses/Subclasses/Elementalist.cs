using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because an Elementalist is a type of spellcaster
  it should have spellcaster characteristics".

  Elementalist also has it's own unique features.
     */
public class Elementalist : SpellCaster
{
    public Elementalist()
    {
        
        //You can override variables in here.
        //Example:
        // numSpellPieces = 5;
        classType = "Elementalist";
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
