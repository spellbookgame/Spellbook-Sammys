using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    public int iPagesRequired;
    public int iPagesComplete;
    public bool bChapterComplete;

    public string sChapterName;

    // reference to spell
    private Spell Spell;

    // list of spells of type Spell
    public List<Spell> spells;

    public Chapter()
    {
        spells = new List<Spell>();
    }
}
