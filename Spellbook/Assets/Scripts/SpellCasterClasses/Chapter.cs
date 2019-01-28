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
                Spell arcaneBlast = new ArcaneBlast();
                spellsAllowed.Add(arcaneBlast);
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
                Spell iceBarrier = new IceBarrier();
                spellsAllowed.Add(iceBarrier);
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
    public void CompareSpells(SpellCaster player, Transform slotTransform)
    {
        // TODO
    }
}
