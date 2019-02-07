using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fMaxHealth;
    public float fCurrentHealth;

    private List<string> dropSpellPieces;
    CollectItemScript collectItemScript;

    public Enemy(float maxHealth)
    {
        collectItemScript = GameObject.Find("ScriptContainer").GetComponent<CollectItemScript>();
        fMaxHealth = maxHealth;

        dropSpellPieces = new List<string>()
        {
            { "Alchemy Spell Piece" },
            { "Arcane Spell Piece" },
            { "Elemental Spell Piece" },
            { "Illusion Spell Piece" },
            { "Summoning Spell Piece" },
            { "Time Spell Piece" },
        };
    }

    // drops random spell piece when enemy dies
    public void EnemyDefeated()
    {
        int index = Random.Range(0, 6);
        Debug.Log(index);
        if(fCurrentHealth <= 0)
        {
            collectItemScript.CollectSpellPiece(dropSpellPieces[index]);
        }
    }
}
