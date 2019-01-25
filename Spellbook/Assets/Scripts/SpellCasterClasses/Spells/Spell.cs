using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public int iTier;
    public string sSpellName;
    public string spellClass;

    public List<string> requiredPieces;

    public abstract void collectSpell();

    // CTOR
    public Spell()
    {
        requiredPieces = new List<string>();
    }
}
