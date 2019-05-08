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
    public int iCharges;

    public string sSpellName;
    public string sSpellClass;
    public string sSpellInfo;

    public bool combatSpell;

    public Dictionary<string, int> requiredRunes;

    public Color colorPrimary;
    public Color colorSecondary;
    public Color colorTertiary;

    // A sprite used to guide the player's finger swipe to cast this spell.
    // Currently only being used in combat.
    public Sprite guideLine;



    // CTOR
    public Spell()
    {
        requiredRunes = new Dictionary<string, int>();
    }

    // Abstract functions
    public abstract void SpellCast(SpellCaster player);

    public virtual void Charge(SpellCaster player)
    {
        if (player.iMana < iManaCost)
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to charge this spell.", "OK");
        else
        {
            player.LoseMana(iManaCost);
            ++iCharges;
        }
    }
}
