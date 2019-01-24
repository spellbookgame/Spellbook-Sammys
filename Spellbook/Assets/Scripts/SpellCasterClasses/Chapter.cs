using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    public int iPagesRequired;
    public int iPagesComplete;
    public bool bChapterComplete;

    public string sChapterName;

    private Spell Spell;
    private ArrayList spells;

    public Chapter()
    {
        spells = new ArrayList();
    }
}
