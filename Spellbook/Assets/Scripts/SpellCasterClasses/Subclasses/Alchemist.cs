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
        classType = "Alchemist";
        fMaxHealth = 20.0f;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/AlchemyWizardFlat";
    }
}
