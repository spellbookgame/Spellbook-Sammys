using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAllyCastable 
{
    //Require that every spell that can be cast on an ally implements these methods.
    
    //Input: the player to cast the spell on. (local player).
    void RecieveCastFromAlly(SpellCaster player);

    //There is a second part to calling Spellcast() for these types of spells.
    //Input: target ally spllcasterID to cast on
    void SpellcastPhase2(int sID);
}
