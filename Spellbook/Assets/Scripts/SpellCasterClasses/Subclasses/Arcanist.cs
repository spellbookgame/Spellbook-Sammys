﻿using System.Collections;
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
        fMaxHealth = 20.0f;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/ArcaneWizardFlat";

        // for playtesting purposes - delete later
        if (easyMode)
        {
            spellPieces["Arcane A Spell Piece"] += 1;
            spellPieces["Arcane D Spell Piece"] += 1;

            glyphs["Arcane A Glyph"] += 4;
            glyphs["Arcane D Glyph"] += 4;
        }
    }
}
