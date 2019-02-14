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
    public float fBasicAttackStrength;

    public int iMana;

    public string classType;
    public Chapter chapter;

    // player's collection of spell pieces stored as strings
    public Dictionary<string, int> spellPieces;
    public Dictionary<string, int> glyphs;
    public List<string> activeSpells;

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

        activeSpells = new List<string>();

        // initializing dictionary and adding values
        spellPieces = new Dictionary<string, int>()
        {
            { "Alchemy A Spell Piece", 0 },
            { "Alchemy B Spell Piece", 0 },
            { "Alchemy C Spell Piece", 0 },
            { "Alchemy D Spell Piece", 0 },
            { "Arcane A Spell Piece", 0 },
            { "Arcane B Spell Piece", 0 },
            { "Arcane C Spell Piece", 0 },
            { "Arcane D Spell Piece", 0 },
            { "Elemental A Spell Piece", 0 },
            { "Elemental B Spell Piece", 0 },
            { "Elemental C Spell Piece", 0 },
            { "Elemental D Spell Piece", 0 },
            { "Illusion A Spell Piece", 0 },
            { "Illusion B Spell Piece", 0 },
            { "Illusion C Spell Piece", 0 },
            { "Illusion D Spell Piece", 0 },
            { "Summoning A Spell Piece", 0 },
            { "Summoning B Spell Piece", 0 },
            { "Summoning C Spell Piece", 0 },
            { "Summoning D Spell Piece", 0 },
            { "Time A Spell Piece", 0 },
            { "Time B Spell Piece", 0 },
            { "Time C Spell Piece", 0 },
            { "Time D Spell Piece", 0 }
        };

        /*foreach(KeyValuePair<string, int> kvp in spellPieces)
        {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }*/

        glyphs = new Dictionary<string, int>()
        {
            { "Alchemy A Glyph", 0 },
            { "Alchemy B Glyph", 0 },
            { "Alchemy C Glyph", 0 },
            { "Alchemy D Glyph", 0 },
            { "Arcane A Glyph", 0 },
            { "Arcane B Glyph", 0 },
            { "Arcane C Glyph", 0 },
            { "Arcane D Glyph", 0 },
            { "Elemental A Glyph", 0 },
            { "Elemental B Glyph", 0 },
            { "Elemental C Glyph", 0 },
            { "Elemental D Glyph", 0 },
            { "Illusion A Glyph", 0 },
            { "Illusion B Glyph", 0 },
            { "Illusion C Glyph", 0 },
            { "Illusion D Glyph", 0 },
            { "Summoning A Glyph", 0 },
            { "Summoning B Glyph", 0 },
            { "Summoning C Glyph", 0 },
            { "Summoning D Glyph", 0 },
            { "Time A Glyph", 0 },
            { "Time B Glyph", 0 },
            { "Time C Glyph", 0 },
            { "Time D Glyph", 0 },
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

    // adds spell piece to player's collection
    public void CollectSpellPiece(string spellPieceName, SpellCaster player)
    {
        player.spellPieces[spellPieceName] += 1;
        Debug.Log("Collected " + spellPieceName + ". You now have " + player.spellPieces[spellPieceName] + "pieces.");
    }

    public string CollectRandomSpellPiece(SpellCaster player)
    {
        List<string> spellPieceList = new List<string>(player.spellPieces.Keys);
        int random = (int)Random.Range(0, spellPieceList.Count);

        string randomKey = spellPieceList[random];
        player.spellPieces[randomKey] += 1;

        return randomKey;
    }

    public int CollectMana(SpellCaster player)
    {
        int manaAmount = (int)Random.Range(100, 1000);
        player.iMana += manaAmount;

        return manaAmount;
    }

    public string CollectRandomGlyph(SpellCaster player)
    {
        List<string> glyphList = new List<string>(player.glyphs.Keys);
        int random = (int)Random.Range(0, glyphList.Count);

        string randomKey = glyphList[random];
        player.glyphs[randomKey] += 1;

        return randomKey;
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
