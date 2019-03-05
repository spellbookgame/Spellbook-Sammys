﻿using Bolt.Samples.Photon.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
 A base class that all SpellCaster types/classes will inherit from.
     */

public abstract class SpellCaster 
{
    public string matchname;
    public int numOfTurnsSoFar = 0;

    public float fMaxHealth;
    public float fCurrentHealth;
    public float fBasicAttackStrength;

    public int iMana;
    
    public string classType;
    public int spellcasterID;
    public bool hasAttacked;
    public Chapter chapter;

    // player's collection of spell pieces, glyphs, and active spells stored as strings
    public Dictionary<string, int> glyphs;
    public List<Spell> activeSpells;

    // reference to the character's sprite/background
    public string characterSpritePath;
    public string characterBackgroundPath;
    public string characterIconPath;

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
            PanelHolder.instance.displayEvent("Brew - Crystal Scent", "You found " + manaCount + " mana!");
        }
        // if Arcana Harvest is active, double mana
        else if(this.classType.Equals("Arcanist") && this.activeSpells.Contains(this.chapter.spellsAllowed[1]))
        {
            manaCount *= 2;
            Debug.Log("Arcana Harvest is active, gained double mana");
            PanelHolder.instance.displayEvent("Arcana Harvest", "You found " + manaCount + " mana!");
        }
        else
        {
            PanelHolder.instance.displayEvent("You found Mana!", "You earned " + manaCount + " mana.");
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
        PanelHolder.instance.displayEvent("You found a glyph!", "You found 1 " + glyphName + ".");
    }

    public string CollectRandomGlyph()
    {
        List<string> glyphList = new List<string>(this.glyphs.Keys);
        int random = (int)UnityEngine.Random.Range(0, glyphList.Count + 1);

        string randomKey = glyphList[random];

        // if arcana harvest is active
        if (this.classType.Equals("Arcanist") && this.activeSpells.Contains(this.chapter.spellsAllowed[1]))
        {
            this.glyphs[randomKey] += 2;
            PanelHolder.instance.displayEvent("Arcana Harvest", "You found 2 " + randomKey + ".");
        }
        else
        {
            this.glyphs[randomKey] += 1;
            PanelHolder.instance.displayEvent("You found a Glyph!", "You found a " + randomKey + ".");
        }
        return randomKey;
    }

    public string LoseRandomGlyph()
    {
        List<string> glyphList = new List<string>(this.glyphs.Keys);
        int random = (int)UnityEngine.Random.Range(0, glyphList.Count);

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
                g.GetComponent<SpellCreateHandler>().inventoryText.text = "You already have " + spell.sSpellName + ".";
            }
            else
            {
                // add spell to its chapter
                chapter.spellsCollected.Add(spell);
                LobbyManager.s_Singleton.notifyHostAboutCollectedSpell(spellcasterID, spell.sSpellName);
                savePlayerData(this);

                // tell player that the spell is collected
                g.GetComponent<SpellCreateHandler>().inventoryText.text = "You unlocked " + spell.sSpellName + "!";
                Debug.Log("In your chapter you have:");
                for (int i = 0; i < chapter.spellsCollected.Count; ++i)
                    Debug.Log(chapter.spellsCollected[i].sSpellName);

                Debug.Log("You have " + chapter.spellsCollected.Count + " spells collected.");

                // call function that removes prefabs in SpellManager.cs
                g.GetComponent<SpellCreateHandler>().RemovePrefabs(spell);
            }
        }
    }

    public int NumOfTurnsSoFar
    {
        get => numOfTurnsSoFar;
        set => numOfTurnsSoFar = value;
    }

    public static void savePlayerData(SpellCaster s)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat");
        BoltConsole.Write("Saved in" + Application.persistentDataPath + "/playerData.dat");
        PlayerData pd = new PlayerData(s);
        bf.Serialize(file, pd);
        file.Close();
    }

    

    public static SpellCaster loadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            BoltConsole.Write("checkpoint0");

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();
            data.printPlayerData();
            BoltConsole.Write("checkpoint1");

            SpellCaster spellcaster;
            switch (data.spellcasterID)
            {
                case 0:
                    spellcaster = new Alchemist();
                    break;
                case 1:
                    spellcaster = new Arcanist();
                    break;
                case 2:
                    spellcaster = new Elementalist();
                    break;
                case 3:
                    spellcaster = new Chronomancer();
                    break;
                case 4:
                    spellcaster = new Trickster();
                    break;
                default:
                    spellcaster = new Summoner();
                    break;
            }
            BoltConsole.Write("checkpoint3");
            spellcaster.spellcasterID = data.spellcasterID;
            spellcaster.classType = data.classType;
            spellcaster.characterSpritePath = data.characterSpritePath;
            spellcaster.fCurrentHealth = data.fCurrentHealth;
            spellcaster.iMana = data.iMana;
            spellcaster.hasAttacked = data.hasAttacked;
            spellcaster.fBasicAttackStrength = data.fBasicAttackStrength;
            spellcaster.numOfTurnsSoFar = data.numOfTurnsSoFar;
            //spellcaster.activeSpells = data.activeSpells;
            BoltConsole.Write("Before Forloop ");
            spellcaster.chapter.DeserializeSpells(spellcaster, data.spellsCollected);
            int mapSize = data.glyphNames.Length;
           
            for (int j = 0; j < mapSize; j++ )
            {
                spellcaster.glyphs[data.glyphNames[j]] = data.glyphCount[j];
            }


            return spellcaster;
        }
        return null;
    }
}
