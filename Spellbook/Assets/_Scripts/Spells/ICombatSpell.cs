using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatSpell {
    //Require that every combat spell implements this method.
    void CombatCast(SpellCaster player);
}
