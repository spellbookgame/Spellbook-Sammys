using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    public int iPagesRequired;
    public int iPagesComplete;
    public bool bChapterComplete;
    public string sChapterName;

    public List<Spell> spellsAllowed;
    public List<Spell> spellsCollected;
    public Dictionary<string, Spell> spellNamePairs;
    public Chapter(string classType)
    {
        spellsAllowed = new List<Spell>();
        spellsCollected = new List<Spell>();
        spellNamePairs = new Dictionary<string, Spell>();
        sChapterName = classType;

        // add spells into spellsAllowed list depending on class type
        // add them in order from tier 1 to tier 3
        switch (classType)
        {
            case "Alchemist":
                spellsAllowed.Add(new PotionofLuck());
                spellsAllowed.Add(new CrystalScent());
                break;
            case "Arcanist":
                spellsAllowed.Add(new Transcribe());
                spellsAllowed.Add(new RuneConversion());
                spellsAllowed.Add(new ArcanaHarvest());
                break;
            case "Chronomancer":
                spellsAllowed.Add(new DelayTime());
                spellsAllowed.Add(new Echo());
                break;
            case "Elementalist":
                spellsAllowed.Add(new Tailwind());
                break;
            case "Summoner":
                spellsAllowed.Add(new CoSLeon());
                break;
            case "Trickster":
                spellsAllowed.Add(new Playwright());
                spellsAllowed.Add(new Allegro());
                break;
            default:
                break;
        }
        MapToDictionary();
    }

    /*For reconnecting purposes, reloads the spellcaster's collected spells.*/
    public void DeserializeSpells(SpellCaster spellCaster, string[] spellNames)
    {
        foreach(string spellName in spellNames)
        {
            foreach(Spell spell in spellsAllowed)
            {
                if(spellName == spell.sSpellName)
                {
                    spellsCollected.Add(spellNamePairs[spellName]);
                    //NetworkManager.s_Singleton.notifyHostAboutCollectedSpell(spellcasterID, spell.sSpellName);
                    break;
                }
            }
        }
    }

    private void MapToDictionary()
    {
        foreach(Spell spell in spellsAllowed)
        {
            spellNamePairs[spell.sSpellName] = spell;
        }
    }
}
