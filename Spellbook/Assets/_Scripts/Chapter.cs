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
                Spell potionOfBlessing = new PotionofBlessing();
                spellsAllowed.Add(potionOfBlessing);
                Spell charmingNegotiator = new CharmingNegotiator();
                spellsAllowed.Add(charmingNegotiator);
                Spell toxicPotion = new ToxicPotion();
                spellsAllowed.Add(toxicPotion);
                Spell crystalScent = new CrystalScent();
                spellsAllowed.Add(crystalScent);
                break;
            case "Arcanist":
                Spell transcribe = new Transcribe();
                spellsAllowed.Add(transcribe);
                Spell combinedKnowledge = new CombinedKnowledge();
                spellsAllowed.Add(combinedKnowledge);
                Spell arcanaHarvest = new ArcanaHarvest();
                spellsAllowed.Add(arcanaHarvest);
                Spell arcaneConversion = new ArcaneConversion();
                spellsAllowed.Add(arcaneConversion);
                break;
            case "Chronomancer":
                Spell delayTime = new DelayTime();
                spellsAllowed.Add(delayTime);
                Spell teleport = new Teleport();
                spellsAllowed.Add(teleport);
                Spell accelerate = new Accelerate();
                spellsAllowed.Add(accelerate);
                break;
            case "Elementalist":
                Spell naturalDisaster = new NaturalDisaster();
                spellsAllowed.Add(naturalDisaster);
                Spell elementalOrb = new ElementalOrb();
                spellsAllowed.Add(elementalOrb);
                Spell fireball = new Fireball();
                spellsAllowed.Add(fireball);
                break;
            case "Summoner":
                Spell cosLeon = new CoSLeon();
                spellsAllowed.Add(cosLeon);
                Spell cowRaven = new CoWRaven();
                spellsAllowed.Add(cowRaven);
                Spell codSkeletons = new CoDSkeletons();
                spellsAllowed.Add(codSkeletons);
                break;
            case "Trickster":
                Spell playwright = new Playwright();
                spellsAllowed.Add(playwright);
                Spell playBack = new Playback();
                spellsAllowed.Add(playBack);
                Spell marionetteCatharsis = new MarionetteCatharsis();
                spellsAllowed.Add(marionetteCatharsis);
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
