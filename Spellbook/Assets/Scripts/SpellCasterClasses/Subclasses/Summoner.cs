using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because a Summoner is a type of spellcaster
  it should have spellcaster characteristics".

  Summoner also has it's own unique features.
     */
public class Summoner : SpellCaster
{

    public Summoner()
    {
        //You can override variables in here.
        classType = "Summoner";
        fMaxHealth = 20.0f;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Summoner casting";

        // for playtesting purposes - delete later
        if (easyMode)
        {
            spellPieces["Summoning B Spell Piece"] += 1;
            spellPieces["Summoning C Spell Piece"] += 1;

            glyphs["Summoning B Glyph"] += 4;
        }
    }
}
