﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This class is used to store player data into a local .dat file,  
 for recovery/reconnection purposes.
     */

[Serializable]
public class PlayerData
{
    public string matchname;
    public int spellcasterID;
    public string classType;
    public string characterSpritePath;
    public float fCurrentHealth;
    public int iMana;
    public bool hasAttacked;
    public float fBasicAttackStrength;
    public int numOfTurnsSoFar;
    public string[] spellsCollected;

    // These 2 arrays correspond to make the Glyph Dictionary in SpellCaster.cs
    public string[] glyphNames; 
    public int[] glyphCount;

    public string[] inventory;

    public PlayerData(SpellCaster spellCasterData)
    {
        this.matchname = NetworkGameState.instance.getMatchName();
        this.spellcasterID = spellCasterData.spellcasterID;
        this.classType = spellCasterData.classType;
        this.characterSpritePath = spellCasterData.characterSpritePath;
        this.fCurrentHealth = spellCasterData.fCurrentHealth;
        this.iMana = spellCasterData.iMana;
        this.hasAttacked = spellCasterData.hasAttacked;
        this.fBasicAttackStrength = spellCasterData.fBasicAttackStrength;
        this.numOfTurnsSoFar = spellCasterData.numOfTurnsSoFar;
        this.spellsCollected = new string[spellCasterData.chapter.spellsCollected.Count];
        for(int i = 0; i < spellsCollected.Length; i++)
        {
            this.spellsCollected[i] = spellCasterData.chapter.spellsCollected[i].sSpellName;
        }

      

        int mapSize = spellCasterData.glyphs.Count;
        glyphNames = new string[mapSize];
        glyphCount = new int[mapSize];
        int j = 0;
        foreach (KeyValuePair<string, int> entry in spellCasterData.glyphs)
        {
            glyphNames[j] = entry.Key;
            glyphCount[j] = entry.Value;
            j++;
        }

        int size = spellCasterData.inventory.Count;
        inventory = new string[size];
        for(int i = 0; i < size; i++)
        {
            inventory[i] = spellCasterData.inventory[i].name;
        }


    }

    public void printPlayerData()
    {
        //Debug.Log(this.classType + " class ");
    }
}
