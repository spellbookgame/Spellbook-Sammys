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
        fMaxHealth = 20.0f;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/TimeWizardFlat";

        // for playtesting purposes - delete later
        if (easyMode)
        {
            spellPieces["Time C Spell Piece"] += 1;

            glyphs["Time C Glyph"] += 4;
        }
    }
}
