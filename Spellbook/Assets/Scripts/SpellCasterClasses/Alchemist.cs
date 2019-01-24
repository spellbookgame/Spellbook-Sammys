using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because an alchemist is a type of spellcaster
  it should have spellcaster characteristics"
     */
public class Alchemist : SpellCaster
{
    public Alchemist()
    {
        
        //You can override variables in here.
        //Example:
        numSpellPieces = 5;
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
