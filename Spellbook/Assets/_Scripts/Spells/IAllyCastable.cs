using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAllyCastable 
{
    //Require that every spell that can be cast on an ally implements this method.
    //Input: the player to cast the spell on. (local player).
    void RecieveCastFromAlly(SpellCaster player);
}
