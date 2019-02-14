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

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

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

    // TODO
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
        PanelManager panelManager = GameObject.Find("ScriptContainer").GetComponent<PanelManager>();
        int index = Random.Range(0, 6);
        int manaCount = Random.Range(100, 1000);

        string panelText = "You defeated the enemy!\n\n" +
                "You received 1 " + dropSpellPieces[index].ToString() + ".\nYou also received " + manaCount + " mana.";

        localPlayer.Spellcaster.CollectSpellPiece(dropSpellPieces[index], localPlayer.Spellcaster);
        localPlayer.Spellcaster.CollectMana(manaCount, localPlayer.Spellcaster);
        panelManager.ShowPanel();
        panelManager.SetPanelText(panelText);
        // panelManager.SetPanelImage(dropSpellPieces[index].ToString());

        Destroy(this.gameObject);
    }
}
