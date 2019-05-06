using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because a Illusionist is a type of spellcaster
  it should have spellcaster characteristics".

  Illusionist also has it's own unique features.
     */
public class Illusionist : SpellCaster
{
    public Illusionist()
    {
        //You can override variables in here.
        classType = "Illusionist";
        spellcasterID = 4;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Illusionist";
        characterIconPath = "Characters/symbol_glow_illusion";

        hexStringLight = "#4A7C75";
        hexStringPanel = "#8BD3FF";
        hexString3rdColor = "B3BA55";
    }
}
