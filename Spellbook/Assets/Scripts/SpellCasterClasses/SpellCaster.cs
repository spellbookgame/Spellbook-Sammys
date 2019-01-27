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
    public int numSpellPieces;

    public string classType;
    public Chapter chapter;

    // TODO:
    //private string backGroundStory; 
    //private Inventory inventory;
    //public Image icon;
    //Implement:
    //Object DeleteFromInventory(string itemName, int count); 

    // Virtual Functions
    public abstract void SpellCast();

    // CTOR
    public SpellCaster()
    {
        //fMaxHealth = 20.0f;     //Commented out in case Spellcasters have different max healths.
        numSpellPieces = 0;
        numMana = 0;
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

    // TODO: right now, any class can collect any spell; we need to fix this
    public void CollectSpell(Spell spell)
    {
        // add it to its chapter
        chapter.spellsCollected.Add(spell);

        // tell player that the spell is collected
        Debug.Log(spell.sSpellName + " was added to your chapter!");
        for(int i = 0; i < chapter.spellsCollected.Count; i++)
            Debug.Log(chapter.spellsCollected[i].sSpellName);
    }
}
