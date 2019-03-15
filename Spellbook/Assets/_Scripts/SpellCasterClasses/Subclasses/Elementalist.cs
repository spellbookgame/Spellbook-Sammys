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
        classType = "Elementalist";
        spellcasterID = 2;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Elementalist";
        characterIconPath = "Characters/symbol_glow_elementalist";

        hexStringDark = "#500000";
        hexStringLight = "#910000";
    }
}
