using Bolt.Samples.Photon.Lobby;
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
    // to track panel being open only once at start of game
    public bool procPanelShown;

    public string matchname;
    public int numOfTurnsSoFar = 0;
    public int spacesTraveled = 0;

    public float fMaxHealth;
    public float fCurrentHealth;
    public float fBasicAttackStrength;

    public int iMana;
    
    public string classType;
    public int spellcasterID;
    public bool hasAttacked;
    public Chapter chapter;

    // player's collection of spell pieces, glyphs, items, and active spells stored as strings
    public Dictionary<string, int> glyphs;
    public List<Spell> activeSpells;
    public List<Quest> activeQuests;
    public List<ItemObject> inventory;

    // reference to the character's sprite/background
    public string characterSpritePath;
    public string characterIconPath;
    public string hexStringLight;
    public string hexStringDark;

    // TODO:
    //private string backGroundStory; 
    //Implement:
    //Object DeleteFromInventory(string itemName, int count); 

    // CTOR
    public SpellCaster()
    {
        //fMaxHealth = 20.0f;     //Commented out in case Spellcasters have different max healths.
        iMana = 1000;
        hasAttacked = false;

        activeSpells = new List<Spell>();
        activeQuests = new List<Quest>();
        inventory = new List<ItemObject>();

        glyphs = new Dictionary<string, int>()
        {
            { "Alchemy A Glyph", 3 },
            { "Alchemy B Glyph", 3 },
            { "Alchemy C Glyph", 3 },
            { "Alchemy D Glyph", 3 },
            { "Arcane A Glyph", 3 },
            { "Arcane B Glyph", 3 },
            { "Arcane C Glyph", 3 },
            { "Arcane D Glyph", 3 },
            { "Elemental A Glyph", 3 },
            { "Elemental B Glyph", 3 },
            { "Elemental C Glyph", 3 },
            { "Elemental D Glyph", 3 },
            { "Illusion A Glyph", 3 },
            { "Illusion B Glyph", 3 },
            { "Illusion C Glyph", 3 },
            { "Illusion D Glyph", 3 },
            { "Summoning A Glyph", 3 },
            { "Summoning B Glyph", 3 },
            { "Summoning C Glyph", 3 },
            { "Summoning D Glyph", 3 },
            { "Time A Glyph", 3 },
            { "Time B Glyph", 3 },
            { "Time C Glyph", 3 },
            { "Time D Glyph", 3 },
        };
    }

    public void AddToInventory(ItemObject newItem)
    {
        inventory.Add(newItem);
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
        Debug.Log("mana count initial: " + manaCount);
        manaCount = SpellTracker.instance.CheckManaSpell(manaCount);
        Debug.Log("mana count after: " + manaCount);
        QuestTracker.instance.CheckManaQuest(manaCount);
        this.iMana += manaCount;
    }
    public void LoseMana(int manaCount)
    {
        this.iMana -= manaCount;
    }

    public void CollectGlyph(string glyphName)
    {
        int glyphCount = SpellTracker.instance.CheckGlyphSpell(glyphName);
        this.glyphs[glyphName] += glyphCount;
    }

    // fix this up after removing from eventhandler and enemy drop
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
    public bool CollectSpell(Spell spell)
    {
        bool spellCollected = false;
        GameObject g = GameObject.FindGameObjectWithTag("SpellManager");

        // only add the spell if the player is the spell's class
        if (spell.sSpellClass == this.classType)
        {
            // if chapter.spellsCollected already contains spell, give error notice
            if (chapter.spellsCollected.Contains(spell))
            {
                Debug.Log("You already have " + spell.sSpellName + ".");
            }
            else
            {
                SoundManager.instance.PlaySingle(SoundManager.spellcreate);
                // add spell to its chapter
                chapter.spellsCollected.Add(spell);
                LobbyManager.s_Singleton.notifyHostAboutCollectedSpell(spellcasterID, spell.sSpellName);
                savePlayerData(this);

                // tell player that the spell is collected
                g.GetComponent<SpellCreateHandler>().inventoryText.text = "You unlocked " + spell.sSpellName + "!";

                Debug.Log("You have " + chapter.spellsCollected.Count + " spells collected.");

                spellCollected = true;
            }
        }
        return spellCollected;
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
