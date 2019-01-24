using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public string sSpellName;
    public int iTier;
    public string spellClass;

    public ArrayList requiredPieces;

    public abstract void collectSpell();

    // CTOR
    public Spell()
    {
        requiredPieces = new ArrayList();
    }
}
