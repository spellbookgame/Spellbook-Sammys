using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    public int iPagesRequired;
    public int iPagesComplete;
    public bool bChapterComplete;
    public string sChapterName;

    public List<Spell> spellsAllowed;
    public List<Spell> spellsCollected;

    public Chapter(string classType)
    {
        spellsAllowed = new List<Spell>();
        spellsCollected = new List<Spell>();

        sChapterName = classType;

        // add spells into spellsAllowed list depending on class type
        switch (classType)
        {
            case "Alchemist":
                Spell manaConstruct = new ManaConstruct();
                spellsAllowed.Add(manaConstruct);
                break;
            case "Arcanist":
                Spell magicMissiles = new MagicMissiles();
                spellsAllowed.Add(magicMissiles);
                Spell arcanaHarvest = new ArcanaHarvest();
                spellsAllowed.Add(arcanaHarvest);
                break;
            case "Summoner":
                Spell cowBear = new CoWBear();
                spellsAllowed.Add(cowBear);
                break;
            case "Chronomancer":
                Spell accelerate = new Accelerate();
                spellsAllowed.Add(accelerate);
                break;
            case "Elementalist":
                Spell fireball = new Fireball();
                spellsAllowed.Add(fireball);
                break;
            case "Trickster":
                Spell playwright = new Playwright();
                spellsAllowed.Add(playwright);
                break;
            default:
                break;
        }
    }

    /* called in SpellManager.cs
     * compares 2 dictionaries (requiredPieces and slotPieces)
     * if they match, calls CollectSpell in SpellCaster.cs
     */
    public void CompareSpells(SpellCaster player, Dictionary<string, int> slotPieces)
    {
        bool equal = false;

        Dictionary<string, int> dictionary1 = slotPieces;

        // iterate through each spell that player can collect
        for(int i = 0; i < player.chapter.spellsAllowed.Count; ++i)
        {
            Dictionary<string, int> dictionary2 = player.chapter.spellsAllowed[i].requiredPieces;
            
            // tier 3 spells: only need 1 required piece
            if (player.chapter.spellsAllowed[i].iTier == 3)
            {
                if(dictionary2.Keys.All(k => dictionary1.ContainsKey(k)))
                {
                    equal = true;
                    // if equal and player does not have the spell yet
                    if (equal && !player.chapter.spellsCollected.Contains(player.chapter.spellsAllowed[i]))
                    {
                        player.CollectSpell(player.chapter.spellsAllowed[i], player);
                        break;
                    }
                }
            }
            // tier 2 spells: need 3 required pieces
            else if(player.chapter.spellsAllowed[i].iTier == 2)
            {
                foreach(KeyValuePair<string, int> kvp in dictionary2)
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
                    player.CollectSpell(player.chapter.spellsAllowed[i], player);
                    break;
                }
            }
            // tier 1 spell: need 4 required pieces
            else if(player.chapter.spellsAllowed[i].iTier == 1)
            {
                if(dictionary1.Keys.Count == dictionary2.Keys.Count && dictionary1.Keys.All(k => dictionary2.ContainsKey(k) 
                    && object.Equals(dictionary2[k], dictionary1[k])))
                {
                    equal = true;
                    if (equal)
                    {
                        player.CollectSpell(player.chapter.spellsAllowed[i], player);
                        break;
                    }
                }
            }
        }
    }
}
