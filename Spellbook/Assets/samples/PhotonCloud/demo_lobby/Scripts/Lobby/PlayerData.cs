using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
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

    public PlayerData(SpellCaster spellCasterData)
    {
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


    }

    public void printPlayerData()
    {
        Debug.Log(this.classType + " class ");
    }
}
