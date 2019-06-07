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

        hexStringLight = "#910000";
        hexStringPanel = "#744246";
        hexString3rdColor = "7D59CC";

        combatSpells = new Dictionary<string, Spell>()
        {
            { "Natural Disaster", new NaturalDisaster()},
            { "Eye of the Storm", new EyeOfTheStorm()},
            { "Fireball", new Fireball()}
        };

        // for demo build
        chapter.spellsCollected.Add(new Growth());
        chapter.spellsCollected.Add(new Fireball());
        chapter.spellsCollected.Add(new Tailwind());
    }
}
