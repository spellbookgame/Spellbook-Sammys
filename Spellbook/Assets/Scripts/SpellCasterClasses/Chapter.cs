using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    public int iPagesRequired;
    public int iPagesComplete;
    public bool bChapterComplete;
    public string sChapterName;

    public List<string> spellsAllowed;
    public List<string> spellsCollected;

    // reference to spell - not sure if we need this
    // private Spell spell;

    // list of spells of type Spell
    public List<Spell> spells;

    public Chapter(string classType)
    {
        spells = new List<Spell>();
        spellsAllowed = new List<string>();
        spellsCollected = new List<string>();

        sChapterName = classType;
    }
}
