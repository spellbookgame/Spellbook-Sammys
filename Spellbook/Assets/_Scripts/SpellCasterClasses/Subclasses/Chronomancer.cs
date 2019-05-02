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
        classType = "Chronomancer";
        spellcasterID = 3;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Chronomancer";
        characterIconPath = "Characters/symbol_glow_chronomancer";

        hexStringLight = "#6D4021";
        hexStringPanel = "#FFFFFF";
    }
}
