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
        classType = "Illusionist";
        spellcasterID = 4;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Trickster";
        characterIconPath = "Characters/symbol_glow_illusion";

        hexStringLight = "#4A7C75";
        hexStringPanel = "#8BD3FF";
    }
}
