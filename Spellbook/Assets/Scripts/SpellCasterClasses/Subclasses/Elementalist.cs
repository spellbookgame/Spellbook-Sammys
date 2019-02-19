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
        fMaxHealth = 20.0f;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/ElementalWizardFlat";

        // for playtesting purposes - delete later
        if (easyMode)
        {
            spellPieces["Elemental D Spell Piece"] += 1;

            glyphs["Elemental D Glyph"] += 4;
        }
    }
}
