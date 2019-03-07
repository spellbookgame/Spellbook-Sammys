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
        characterBackgroundPath = "Characters/Arcane bgd blank";
        characterIconPath = "Characters/symbol_glow_arcanist";

        glyphs["Alchemy A Glyph"] += 3;
        glyphs["Alchemy B Glyph"] += 3;
        glyphs["Alchemy C Glyph"] += 3;
        glyphs["Alchemy D Glyph"] += 3;
        glyphs["Arcane A Glyph"] += 3;
        glyphs["Arcane B Glyph"] += 3;
        glyphs["Arcane C Glyph"] += 3;
        glyphs["Arcane D Glyph"] += 3;
        glyphs["Elemental A Glyph"] += 3;
        glyphs["Elemental B Glyph"] += 3;
        glyphs["Elemental C Glyph"] += 3;
        glyphs["Elemental D Glyph"] += 3;
        glyphs["Illusion A Glyph"] += 3;
        glyphs["Illusion B Glyph"] += 3;
        glyphs["Illusion C Glyph"] += 3;
        glyphs["Illusion D Glyph"] += 3;
        glyphs["Summoning A Glyph"] += 3;
        glyphs["Summoning B Glyph"] += 3;
        glyphs["Summoning C Glyph"] += 3;
        glyphs["Summoning D Glyph"] += 3;
        glyphs["Time A Glyph"] += 3;
        glyphs["Time B Glyph"] += 3;
        glyphs["Time C Glyph"] += 3;
        glyphs["Time D Glyph"] += 3;
    }
}
