using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 A base class that all SpellCaster types/classes will inherit from.
     */
public abstract class SpellCaster 
{
    private int numOfTurnsSoFar = 0;

    public float fMaxHealth;
    public float fCurrentHealth;
    public float fBasicAttackStrength;

    public int iMana;
    
    public string classType;
    public bool hasAttacked;
    public Chapter chapter;

    // player's collection of spell pieces, glyphs, and active spells stored as strings
    public Dictionary<string, int> glyphs;
    public List<Spell> activeSpells;

    // reference to the character's sprite
    public string characterSpritePath;

    // TODO:
    //private string backGroundStory; 
    //private Inventory inventory;
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
        hasAttacked = false;

        activeSpells = new List<Spell>();

        glyphs = new Dictionary<string, int>()
        {
            { "Alchemy A Glyph", 0 },
            { "Alchemy B Glyph", 0 },
            { "Alchemy C Glyph", 0 },
            { "Alchemy D Glyph", 0 },
            { "Arcane A Glyph", 0},
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

    public void AddToInventory(string item, int count)
    {
        //inventory.add(item, count);
    }

    public void TakeDamage(int dmg)
    {
        if(fCurrentHealth > 0)
            fCurrentHealth -= dmg;
        if (fCurrentHealth <= 0)
            fCurrentHealth = 0;
    }

    public void HealDamage(int heal)
    {
        fCurrentHealth += heal;
        if(fCurrentHealth > fMaxHealth)
        {
            fCurrentHealth = fMaxHealth;
        }
    }

    public void CollectMana(int manaCount)
    {
        // if Crystal Scent is active, add 20% more mana
        if(this.classType.Equals("Alchemist") && this.activeSpells.Contains(this.chapter.spellsAllowed[1]))
        {
            manaCount += (int)(manaCount * 0.2);
            Debug.Log("Crystal Scent is active, gained 20% more mana");
        }
        this.iMana += manaCount;
    }
    public void LoseMana(int manaCount)
    {
        this.iMana -= manaCount;
    }

    public void CollectGlyph(string glyphName)
    {
        this.glyphs[glyphName] += 1;
        Debug.Log("You collected " + glyphName + ". You now have " + this.glyphs[glyphName]);
    }

    public string CollectRandomGlyph()
    {
        List<string> glyphList = new List<string>(this.glyphs.Keys);
        int random = (int)Random.Range(0, glyphList.Count);

        string randomKey = glyphList[random];
        this.glyphs[randomKey] += 1;

        return randomKey;
    }

    public string LoseRandomGlyph()
    {
        List<string> glyphList = new List<string>(this.glyphs.Keys);
        int random = (int)Random.Range(0, glyphList.Count);

        string randomKey = glyphList[random];

        if(this.glyphs[randomKey] > 0)
            this.glyphs[randomKey] -= 1;

        return randomKey;
    }

    // method that adds spell to player's chapter
    // called from Chapter.cs
    public void CollectSpell(Spell spell)
    {
        GameObject g = GameObject.FindWithTag("SpellManager");

        // only add the spell if the player is the spell's class
        if (spell.sSpellClass == this.classType)
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

    public int NumOfTurnsSoFar
    {
        get => numOfTurnsSoFar;
        set => numOfTurnsSoFar = value;
    }
}
