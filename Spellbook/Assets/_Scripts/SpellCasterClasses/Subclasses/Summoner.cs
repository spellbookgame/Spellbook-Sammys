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
        spellcasterID = 5;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Summoner";
        characterIconPath = "Characters/symbol_glow_summoner";

        hexStringLight = "#000091";
        hexStringPanel = "#3A3E61";
        hexString3rdColor = "FFEE93";
        combatSpells = new Dictionary<string, Spell>()
        {
            { "Call of the Wild - Raven's Song", new Ravenssong()},
            { "Call of the Wild - Bear's Fury", new Bearsfury()},
            { "Call of the Dead - Skeletons", new Skeletons()}
        };
    }
}
