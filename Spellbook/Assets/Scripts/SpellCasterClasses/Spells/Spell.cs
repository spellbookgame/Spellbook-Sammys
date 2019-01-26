using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public int iTier;
    public string sSpellName;
    public string sSpellClass;

    public List<string> requiredPieces;

    // CTOR
    public Spell()
    {
        requiredPieces = new List<string>();
    }
}
