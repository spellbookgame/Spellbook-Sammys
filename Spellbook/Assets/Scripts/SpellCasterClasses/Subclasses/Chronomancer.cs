using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because a Chronomancer is a type of spellcaster
  it should have spellcaster characteristics".

  Chronomancer also has it's own unique features.
     */
public class Chronomancer : SpellCaster
{
    public Chronomancer()
    {
        
        //You can override variables in here.
        //Example:
        // numSpellPieces = 5;
        classType = "Chronomancer";

        // creating the class-specific chapter
        chapter = new Chapter(classType);
    }

    public override void SpellCast()
    {
        Debug.Log("SpellCast not implemented");
    }
}
