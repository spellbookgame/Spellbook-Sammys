using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because an Arcanist is a type of spellcaster
  it should have spellcaster characteristics".

  Arcanist also has it's own unique features.
     */
public class Arcanist : SpellCaster
{
    public Arcanist()
    {
        //You can override variables in here.
        classType = "Arcanist";
        spellcasterID = 1;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/ArcaneWizardFlat";
        characterIconPath = "Characters/symbol_glow_arcanist";

        hexStringDark = "#300049";
        hexStringLight = "#853CBF";
    }
}
