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
                spellsAllowed.Add(new PotionofBlessing());
                spellsAllowed.Add(new DistilledPotion());
                spellsAllowed.Add(new CharmingNegotiator());
                spellsAllowed.Add(new CollectorsDrink());
                spellsAllowed.Add(new ToxicPotion());
                spellsAllowed.Add(new CrystalScent());
                break;
            case "Arcanist":
                spellsAllowed.Add(new Transcribe());
                spellsAllowed.Add(new MarcellasBlessing());
                spellsAllowed.Add(new Archive());
                spellsAllowed.Add(new CombinedKnowledge());
                spellsAllowed.Add(new RunicDarts());
                spellsAllowed.Add(new RuneConversion());
                spellsAllowed.Add(new ArcanaHarvest());
                break;
            case "Chronomancer":
                spellsAllowed.Add(new DelayTime());
                spellsAllowed.Add(new DejaVu());
                spellsAllowed.Add(new Manipulate());
                spellsAllowed.Add(new Forecast());
                spellsAllowed.Add(new ReverseWounds());
                spellsAllowed.Add(new Echo());
                spellsAllowed.Add(new Chronoblast());
                break;
            case "Elementalist":
                spellsAllowed.Add(new NaturalDisaster());
                spellsAllowed.Add(new Tailwind());
                spellsAllowed.Add(new EyeOfTheStorm());
                spellsAllowed.Add(new TerraformingEarthquake());
                spellsAllowed.Add(new FrozenTerrain());
                spellsAllowed.Add(new Fireball());
                spellsAllowed.Add(new Growth());
                break;
            case "Summoner":
                spellsAllowed.Add(new CoSLeon());
                spellsAllowed.Add(new Ravenssong());
                spellsAllowed.Add(new Bearsfury());
                spellsAllowed.Add(new CoMUmbra());
                spellsAllowed.Add(new CoDCorpse());
                spellsAllowed.Add(new Skeletons());
                spellsAllowed.Add(new CoSRigel());
                break;
            case "Illusionist":
                spellsAllowed.Add(new Playwright());
                spellsAllowed.Add(new CheatOut());
                spellsAllowed.Add(new Playback());
                spellsAllowed.Add(new Catastrophe());
                spellsAllowed.Add(new Catharsis());
                spellsAllowed.Add(new Tragedy());
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
