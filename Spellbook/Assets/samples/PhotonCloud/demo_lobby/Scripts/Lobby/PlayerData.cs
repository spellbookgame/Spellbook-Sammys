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
    //public List<Spell> activeSpells;

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
        //this.activeSpells = spellCasterData.activeSpells;

    }

    public void printPlayerData()
    {
        Debug.Log(this.classType + " class ");
    }
}
