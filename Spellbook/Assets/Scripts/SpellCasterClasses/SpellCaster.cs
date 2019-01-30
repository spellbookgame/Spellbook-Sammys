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
    public int numMana;

    public string classType;
    public Chapter chapter;

    // player's collection of spell pieces stored as strings
    // public List<string> spellPieces;
    // switch List<> spellPieces to dictionary that holds the spell piece and the player's # of that spellPiece
    public Hashtable dspellPieces;

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
        numMana = 1000;
        // spellPieces = new List<string>();

        // initializing dictionary and adding values
        dspellPieces = new Hashtable()
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
            // TODO: if chapter.spellsAllowed already contains spell, give error notice
            // add spell to its chapter
            chapter.spellsCollected.Add(spell);

            // tell player that the spell is collected
            g.GetComponent<SpellManager>().inventoryText.text = "You unlocked " + spell.sSpellName + "!";
            Debug.Log("In your chapter you have:");
            for (int i = 0; i < chapter.spellsCollected.Count; ++i)
                Debug.Log(chapter.spellsCollected[i].sSpellName);

            Debug.Log("You have " + chapter.spellsCollected.Count + " spells collected.");

            // remove spell pieces from player's library
            RemoveSpellPieces(player, spell);
        }
    }
    
    // removes spell pieces from player's "inventory"
    public void RemoveSpellPieces(SpellCaster player, Spell spell)
    {
        GameObject g = GameObject.FindWithTag("SpellManager");

        int oldValue;

        // subtract spell pieces from player's inventory
        for (int i = 0; i < spell.requiredPiecesList.Count; ++i)
        {
            oldValue = (int)player.dspellPieces[spell.requiredPiecesList[i]];
            player.dspellPieces[spell.requiredPiecesList[i]] = oldValue - 1;

            Debug.Log("Removed " + spell.requiredPiecesList[i]);
        }
        // call function that removes prefabs in SpellManager.cs
        g.GetComponent<SpellManager>().RemovePrefabs(spell);
    }
}
