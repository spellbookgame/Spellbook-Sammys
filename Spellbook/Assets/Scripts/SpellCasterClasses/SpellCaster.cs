using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 A base class that all SpellCaster types/classes will inherit from.
     */
public abstract class SpellCaster 
{
    public float fMaxHealth;
    public float fCurrentHealth;

    public int iBasicAttackStrength;
    public int iMana;

    public string classType;
    public Chapter chapter;

    // player's collection of spell pieces stored as strings
    public Dictionary<string, int> spellPieces;

    // TODO:
    //private string backGroundStory; 
    //private Inventory inventory;
    //public Image icon;
    //Implement:
    //Object DeleteFromInventory(string itemName, int count); 

    // Virtual Functions
    // SpellCast() moved to Spell.cs
    // public abstract void SpellCast();

    // CTOR
    public SpellCaster()
    {
        //fMaxHealth = 20.0f;     //Commented out in case Spellcasters have different max healths.
        iMana = 1000;

        // initializing dictionary and adding values
        spellPieces = new Dictionary<string, int>()
        {
            { "Alchemy Spell Piece", 0 },
            { "Arcane Spell Piece", 0 },
            { "Elemental Spell Piece", 0 },
            { "Illusion Spell Piece", 0 },
            { "Summoning Spell Piece", 0 },
            { "Time Spell Piece", 0 }
        };
    }

    void AddToInventory(string item, int count)
    {
        //inventory.add(item, count);
    }

    void TakeDamage(int dmg)
    {
        fCurrentHealth -= dmg;
    }

    void HealDamage(int heal)
    {
        fCurrentHealth += heal;
        if(fCurrentHealth > fMaxHealth)
        {
            fCurrentHealth = fMaxHealth;
        }
    }

    // method that adds spell to player's chapter
    // called from Chapter.cs
    public void CollectSpell(Spell spell, SpellCaster player)
    {
        GameObject g = GameObject.FindWithTag("SpellManager");

        // only add the spell if the player is the spell's class
        if (spell.sSpellClass == player.classType)
        {
            // if chapter.spellsAllowed already contains spell, give error notice
            if (chapter.spellsCollected.Contains(spell))
            {
                g.GetComponent<SpellManager>().inventoryText.text = "You already have " + spell.sSpellName + ".";
            }
            else
            {
                // add spell to its chapter
                chapter.spellsCollected.Add(spell);

                // tell player that the spell is collected
                g.GetComponent<SpellManager>().inventoryText.text = "You unlocked " + spell.sSpellName + "!";
                Debug.Log("In your chapter you have:");
                for (int i = 0; i < chapter.spellsCollected.Count; ++i)
                    Debug.Log(chapter.spellsCollected[i].sSpellName);

                Debug.Log("You have " + chapter.spellsCollected.Count + " spells collected.");

                // call function that removes prefabs in SpellManager.cs
                g.GetComponent<SpellManager>().RemovePrefabs(spell);
            }
        }
    }
}
