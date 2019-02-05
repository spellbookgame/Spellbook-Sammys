using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    public int iPagesRequired;
    public int iPagesComplete;
    public bool bChapterComplete;
    public string sChapterName;

    public List<Spell> spellsAllowed;
    public List<Spell> spellsCollected;

    // reference to spell - not sure if we need this
    // private Spell spell;

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

    // compare spellSlots[i] to spell[i].requiredPieces[i]
    // if all elements match each other, then add spell to chapter
    // this only works for the first spell in each chapter as of now
    public void CompareSpells(SpellCaster player, HashSet<string> slotHash)
    {
        // iterate through the player's spells allowed
        for(int i = 0; i < player.chapter.spellsAllowed.Count; ++i)
        {
            if (slotHash.Count == player.chapter.spellsAllowed[i].requiredPieces.Count)
            {
                // if the two hashsets match
                if (slotHash.SetEquals(player.chapter.spellsAllowed[i].requiredPieces))
                {
                    // add the spell to player's chapter
                    player.CollectSpell(player.chapter.spellsAllowed[i], player);
                }
            }
        }
    }
}
