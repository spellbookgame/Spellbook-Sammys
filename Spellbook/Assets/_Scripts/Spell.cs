using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    // turn these to private later
    public int iTier;
    public int iManaCost;
    public int iCoolDown;
    public int iTurnsActive;
    public int iCastedTurn;

    public string sSpellName;
    public string sSpellClass;
    public string sSpellInfo;

    public Dictionary<string, int> requiredRunes;

    // CTOR
    public Spell()
    {
        requiredRunes = new Dictionary<string, int>();
    }

    // Abstract functions
    public abstract void SpellCast(SpellCaster player);
}
