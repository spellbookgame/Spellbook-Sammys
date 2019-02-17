using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to enemy prefab
public class Enemy : MonoBehaviour
{
    private Player localPlayer;

    public float fMaxHealth;
    public float fCurrentHealth;
    public float fDamageOutput;

    private List<string> dropSpellPieces;

    // call this after instantiating enemy 
    public void Initialize(float maxHealth)
    {
        DontDestroyOnLoad(this);

        fMaxHealth = maxHealth;
        fCurrentHealth = fMaxHealth;

        dropSpellPieces = new List<string>()
        {
            { "Alchemy A Spell Piece"},
            { "Alchemy B Spell Piece"},
            { "Alchemy C Spell Piece"},
            { "Alchemy D Spell Piece"},
            { "Arcane A Spell Piece"},
            { "Arcane B Spell Piece"},
            { "Arcane C Spell Piece"},
            { "Arcane D Spell Piece"},
            { "Elemental A Spell Piece"},
            { "Elemental B Spell Piece"},
            { "Elemental C Spell Piece"},
            { "Elemental D Spell Piece"},
            { "Illusion A Spell Piece"},
            { "Illusion B Spell Piece"},
            { "Illusion C Spell Piece"},
            { "Illusion D Spell Piece"},
            { "Summoning A Spell Piece"},
            { "Summoning B Spell Piece"},
            { "Summoning C Spell Piece"},
            { "Summoning D Spell Piece"},
            { "Time A Spell Piece"},
            { "Time B Spell Piece"},
            { "Time C Spell Piece"},
            { "Time D Spell Piece"}
        };
    }
    
    public void HitEnemy(float damage)
    {
        if(fCurrentHealth > 0)
            fCurrentHealth -= damage;

        if (fCurrentHealth <= 0)
        {
            fCurrentHealth = 0;
            EnemyDefeated();
        }
    }

    // drops random spell piece & mana when enemy dies
    public void EnemyDefeated()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // receive 2 random spell pieces and mana ranged from 100 - 1000
        string randomSpellPiece1 = localPlayer.Spellcaster.CollectRandomSpellPiece();
        string randomSpellPiece2 = localPlayer.Spellcaster.CollectRandomSpellPiece();

        int manaCount = Random.Range(100, 1000);
        localPlayer.Spellcaster.CollectMana(manaCount);

        // set text and show in panel
        string panelText = "You defeated the enemy!\n" +
                "You received 1 " + randomSpellPiece1 + " and 1 " + randomSpellPiece2 + ".\nYou also received " + manaCount + " mana.";
        PanelHolder.instance.displayNotify(panelText);

        Destroy(this.gameObject);
    }
}
