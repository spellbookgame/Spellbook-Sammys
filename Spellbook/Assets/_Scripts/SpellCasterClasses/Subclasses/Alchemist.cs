using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This class inherits from SpellCaster,
  "because an alchemist is a type of spellcaster
  it should have spellcaster characteristics".

  Alchemist also has it's own unique features.
     */
public class Alchemist : SpellCaster
{
    public Alchemist()
    {
        
        //You can override variables in here.
        classType = "Alchemist";
        spellcasterID = 0;
        fMaxHealth = 20.0f;
        fCurrentHealth = fMaxHealth;

        // creating the class-specific chapter
        chapter = new Chapter(classType);

        characterSpritePath = "Characters/Alchemist";
        characterIconPath = "Characters/symbol_glow_alchemist";

        hexStringLight = "#218221";
        hexStringPanel = "#ABFFB4";
        hexString3rdColor = "#295135";

        combatSpells = new Dictionary<string, Spell>()
        {
            { "Brew - Potion of Blessing", new PotionofBlessing()},
            { "Brew - Toxic Potion", new ToxicPotion()},
            { "Brew - Distilled Potion", new DistilledPotion()}
        };

    }
}
