using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        switch (classType)
        {
            case "Alchemist":
                Spell toxicPotion = new ToxicPotion();
                spellsAllowed.Add(toxicPotion);
                Spell crystalScent = new CrystalScent();
                spellsAllowed.Add(crystalScent);
                Spell charmingNegotiator = new CharmingNegotiator();
                spellsAllowed.Add(charmingNegotiator);
                Spell potionOfBlessing = new PotionofBlessing();
                spellsAllowed.Add(potionOfBlessing);
                break;
            case "Arcanist":
                Spell arcaneConversion = new ArcaneConversion();
                spellsAllowed.Add(arcaneConversion);
                Spell arcanaHarvest = new ArcanaHarvest();
                spellsAllowed.Add(arcanaHarvest);
                Spell combinedKnowledge = new CombinedKnowledge();
                spellsAllowed.Add(combinedKnowledge);
                Spell transcribe = new Transcribe();
                spellsAllowed.Add(transcribe);
                break;
            case "Chronomancer":
                Spell accelerate = new Accelerate();
                spellsAllowed.Add(accelerate);
                Spell teleport = new Teleport();
                spellsAllowed.Add(teleport);
                Spell delayTime = new DelayTime();
                spellsAllowed.Add(delayTime);
                break;
            case "Elementalist":
                Spell fireball = new Fireball();
                spellsAllowed.Add(fireball);
                Spell elementalOrb = new ElementalOrb();
                spellsAllowed.Add(elementalOrb);
                Spell naturalDisaster = new NaturalDisaster();
                spellsAllowed.Add(naturalDisaster);
                break;
            case "Summoner":
                Spell codSkeletons = new CoDSkeletons();
                spellsAllowed.Add(codSkeletons);
                Spell cowRaven = new CoWRaven();
                spellsAllowed.Add(cowRaven);
                Spell cosLeon = new CoSLeon();
                spellsAllowed.Add(cosLeon);
                break;
            case "Trickster":
                Spell playwright = new Playwright();
                spellsAllowed.Add(playwright);
                Spell marionetteCatharsis = new MarionetteCatharsis();
                spellsAllowed.Add(marionetteCatharsis);
                Spell playBack = new Playback();
                spellsAllowed.Add(playBack);
                break;
            default:
                break;
        }
        MapToDictionary();
    }

    /* called in SpellCreateHandler.cs
 * compares 2 dictionaries (requiredPieces and slotPieces)
 * if they match, calls CollectSpell in SpellCaster.cs
 */
    /*public void CompareSpells(SpellCaster player, Dictionary<string, int> slotPieces)
    {
        bool equal = false;

        Dictionary<string, int> dictionary1 = slotPieces;

        // iterate through each spell that player can collect
        for (int i = 0; i < player.chapter.spellsAllowed.Count; ++i)
        {
            Dictionary<string, int> dictionary2 = player.chapter.spellsAllowed[i].requiredGlyphs;

            // tier 3 spells: only need 1 required piece
            if (player.chapter.spellsAllowed[i].iTier == 3)
            {
                if (dictionary2.Keys.All(k => dictionary1.ContainsKey(k)))
                {
                    equal = true;
                    // if equal and player does not have the spell yet
                    if (equal && !player.chapter.spellsCollected.Contains(player.chapter.spellsAllowed[i]))
                    {
                        player.CollectSpell(player.chapter.spellsAllowed[i]);
                        break;
                    }
                }
            }
            // tier 2 spells: need 2 required pieces
            else if (player.chapter.spellsAllowed[i].iTier == 2 || player.chapter.spellsAllowed[i].iTier == 1)
            {
                foreach (KeyValuePair<string, int> kvp in dictionary2)
                {
                    if (dictionary1.ContainsKey(kvp.Key))
                    {
                        equal = true;
                    }
                    else
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal && !player.chapter.spellsCollected.Contains(player.chapter.spellsAllowed[i]))
                {
                    player.CollectSpell(player.chapter.spellsAllowed[i]);
                    break;
                }
            }
        }
    }*/

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
                    //LobbyManager.s_Singleton.notifyHostAboutCollectedSpell(spellcasterID, spell.sSpellName);
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
