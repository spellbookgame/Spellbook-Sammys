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
                Spell polymorph = new Polymorph();
                spellsAllowed.Add(polymorph);
                break;
            case "Arcanist":
                Spell magicMissiles = new MagicMissiles();
                spellsAllowed.Add(magicMissiles);
                Spell arcanaHarvest = new ArcanaHarvest();
                spellsAllowed.Add(arcanaHarvest);
                break;
            case "Summoner":
                Spell summonTree = new SummonTree();
                spellsAllowed.Add(summonTree);
                break;
            case "Chronomancer":
                Spell alterTime = new AlterTime();
                spellsAllowed.Add(alterTime);
                break;
            case "Elementalist":
                Spell fireball = new Fireball();
                spellsAllowed.Add(fireball);
                break;
            case "Trickster":
                Spell mirrorImage = new MirrorImage();
                spellsAllowed.Add(mirrorImage);
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
        // iterate through the player's spells allowed
        for (int i = 0; i < player.chapter.spellsAllowed.Count; ++i)
        {
            // if tier 3 spell, only 1 from slotPieces has to match requiredPieces
            // ISSUE: arcana harvest and magic missiles both fit this requirement, so player collects both
            if(player.chapter.spellsAllowed[i].iTier == 3)
            {
                if (slotPieces.ContainsKey(player.chapter.spellsAllowed[i].requiredPieces.ElementAt(0).Key))
                    equal = true;
            }
            // if tier 1 spell, all 4 must match
            // the following comparison is from dotnetperls.com
            else if (slotPieces.Count == player.chapter.spellsAllowed[i].requiredPieces.Count)
            {
                equal = true;
                foreach (var pair in player.chapter.spellsAllowed[i].requiredPieces)
                {
                    int value;
                    if (slotPieces.TryGetValue(pair.Key, out value))
                    {
                        // require value to be equal
                        if (value != pair.Value)
                        {
                            equal = false;
                            break;
                        }
                    }
                    else
                    {
                        // require key be present
                        equal = false;
                        break;
                    }
                }
            }
            if (equal)
            {
                Debug.Log("Player collected spell!");
                player.CollectSpell(player.chapter.spellsAllowed[i], player);
            }
        }
    }
}
