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
        classType = "Trickster";
        fMaxHealth = 20.0f;

        // creating the class-specific chapter
        chapter = new Chapter(classType);
    }
}
