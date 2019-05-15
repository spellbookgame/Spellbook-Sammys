using Bolt.Samples.Photon.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 A base class that all SpellCaster types/classes will inherit from.
     */

public abstract class SpellCaster 
{
    public string matchname;
    public int numOfTurnsSoFar = 0;
    public int spacesTraveled = 0;

    public float fMaxHealth;
    public float fCurrentHealth;
    public float fBasicAttackStrength;

    public int iMana;
    public decimal dManaMultiplier = 1;
    
    // misc attributes
    public string classType;
    public int spellcasterID;
    public int numSpellsCastThisTurn;
    public bool hasAttacked;
    public bool hasRolled;
    public bool scannedSpaceThisTurn;
    public Chapter chapter;

    // tracking items
    public bool waxCandleUsed;      // set in AddToInventory()
    public bool locationItemUsed;   // set in CustomEventHandler()

    // player's collection of glyphs, dice, items, and active spells/quests stored as strings
    public Dictionary<string, int> glyphs;
    public Dictionary<string, int> dice;
    public Dictionary<string, Spell> combatSpells;
    public Dictionary<string, int> tempDice;
    public List<Spell> activeSpells;
    public List<Quest> activeQuests;
    public List<ItemObject> inventory;

    // reference to the character's sprite/background
    public string characterSpritePath;
    public string characterIconPath;
    public string hexStringLight;
    public string hexStringPanel;
    public string hexString3rdColor;

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

        // remove glyphs entirely eventually
        glyphs = new Dictionary<string, int>();

        dice = new Dictionary<string, int>()
        {
            { "D4", 0 },
            { "D5", 0 },
            { "D6", 2 },
            { "D7", 0 },
            { "D8", 0 },
            { "D9", 0 },
        };

        tempDice = new Dictionary<string, int>();
    }

    public void AddToInventory(ItemObject newItem)
    {
        if (inventory.Count >= 12)
            PanelHolder.instance.displayNotify("Too many items!", "Your inventory is full, you cannot hold any more items.", "OK");
        else
            inventory.Add(newItem);

        // if Collector's Drink is active, add another copy of the item
        if (SpellTracker.instance.SpellIsActive("Brew - Collector's Drink"))
        {
            inventory.Add(newItem);
            SpellTracker.instance.RemoveFromActiveSpells("Brew - Collector's Drink");
        }
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
        iMana += manaCount;
        QuestTracker.instance.TrackManaQuest(manaCount);
    }

    public int CollectManaEndTurn()
    {
        SoundManager.instance.PlaySingle(SoundManager.manaCollect);

        int manaCount = UnityEngine.Random.Range(80, 200);
        manaCount = (int)(manaCount * dManaMultiplier);
        iMana += manaCount;

        /// reset mana multiplier
        dManaMultiplier = 1;

        QuestTracker.instance.TrackManaQuest(manaCount);
        return manaCount;
    }

    public void LoseMana(int manaCount)
    {
        iMana -= manaCount;
        if (iMana <= 0)
            iMana = 0;
    }

    // function that adds spell to player's chapter
    public bool CollectSpell(Spell spell)
    {
        bool spellCollected = false;

        // only add the spell if the player is the spell's class
        if (spell.sSpellClass == classType)
        {
            // if chapter.spellsCollected already contains spell, give error notice
            if (chapter.spellsCollected.Any(x => x.sSpellName.Equals(spell.sSpellName)))
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
                PanelHolder.instance.displayNotify(spell.sSpellName, "You unlocked " + spell.sSpellName + "!", "OK");

                QuestTracker.instance.TrackSpellQuest(spell);

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
                    spellcaster = new Illusionist();
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
