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
    public decimal dManaMultiplier = 1;
    public bool endTurnManaCollected;    // bool to track if "end of turn" mana should be collected or not
    
    // misc attributes
    public string classType;
    public int spellcasterID;
    public bool hasAttacked;
    public bool hasRolled;
    public bool scannedSpaceThisTurn;
    public Chapter chapter;

    // player's collection of spell pieces, glyphs, items, and active spells stored as strings
    public Dictionary<string, int> glyphs;
    public Dictionary<string, int> dice;
    public List<Spell> activeSpells;
    public List<Quest> activeQuests;
    public List<ItemObject> inventory;

    // reference to the character's sprite/background
    public string characterSpritePath;
    public string characterIconPath;
    public string hexStringLight;

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

        dice = new Dictionary<string, int>()
        {
            { "D4", 0 },
            { "D6", 2 },
            { "D8", 0 }
        };
    }

    public void AddToInventory(ItemObject newItem)
    {
        inventory.Add(newItem);
    }
    public void RemoveFromInventory(ItemObject newItem)
    {
        inventory.Remove(newItem);
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
        SoundManager.instance.PlaySingle(SoundManager.manaCollect);
        iMana += manaCount;
        QuestTracker.instance.TrackManaQuest(manaCount);
    }

    public int CollectManaEndTurn()
    {
        SoundManager.instance.PlaySingle(SoundManager.manaCollect);

        int manaCount = (int)UnityEngine.Random.Range(40, 100);
        manaCount = (int)(manaCount * dManaMultiplier);
        iMana += manaCount;

        /// reset mana multiplier
        dManaMultiplier = 1;

        endTurnManaCollected = true;
        QuestTracker.instance.TrackManaQuest(manaCount);
        return manaCount;
    }

    public void LoseMana(int manaCount)
    {
        iMana -= manaCount;
        if (iMana <= 0)
            iMana = 0;
    }

    public void CollectGlyph(string glyphName)
    {
        Sprite sprite = Resources.Load<Sprite>("GlyphArt/" + glyphName);
        PanelHolder.instance.displayBoardScan("You found a Glyph!", "You found 1 " + glyphName + ".", sprite);
        SoundManager.instance.PlaySingle(SoundManager.glyphfound);
        glyphs[glyphName] += 1;
    }

    // find a random glyph
    public string CollectRandomGlyph()
    {
        List<string> glyphList = new List<string>(this.glyphs.Keys);
        int random = (int)UnityEngine.Random.Range(0, glyphList.Count + 1);

        string randomKey = glyphList[random];

        glyphs[randomKey] += 1;
        PanelHolder.instance.displayNotify("You found a Glyph!", "You found a " + randomKey + ".", "OK");

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

    // function that adds spell to player's chapter
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
                PanelHolder.instance.displayNotify(spell.sSpellName, "You already have " + spell.sSpellName + ".", "OK");
            }
            else
            {
                SoundManager.instance.PlaySingle(SoundManager.spellcreate);
                // add spell to its chapter
                chapter.spellsCollected.Add(spell);
                NetworkManager.s_Singleton.notifyHostAboutCollectedSpell(spellcasterID, spell.sSpellName);
                savePlayerData(this);

                // tell player that the spell is collected
                //g.GetComponent<SpellCreateHandler>().inventoryText.text = "You unlocked " + spell.sSpellName + "!";
                PanelHolder.instance.displayNotify(spell.sSpellName, "You unlocked " + spell.sSpellName + "!", "Main");

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

            int itemSize = data.inventory.Length;
            for(int j = 0; j < itemSize; j++)
            {
                //TODO: Reload inventory.
            }


            return spellcaster;
        }
        return null;
    }
}
