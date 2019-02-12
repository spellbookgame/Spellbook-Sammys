using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            { "Alchemy Spell Piece" },
            { "Arcane Spell Piece" },
            { "Elemental Spell Piece" },
            { "Illusion Spell Piece" },
            { "Summoning Spell Piece" },
            { "Time Spell Piece" },
        };
    }

    // TODO
    public void HitEnemy(float damage)
    {
        Debug.Log("Current Health: " + fCurrentHealth);
        fCurrentHealth -= damage;
        Debug.Log("Health after hit: " + fCurrentHealth);

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
        int manaCollected = Random.Range(50, 300);

        string panelText = "You defeated the enemy!\n\n" +
                "You received 1 " + dropSpellPieces[index].ToString() + ".\nYou also received " + manaCollected.ToString() + " mana.";

        localPlayer.Spellcaster.CollectSpellPiece(dropSpellPieces[index], localPlayer.Spellcaster);
        localPlayer.Spellcaster.iMana += manaCollected;
        panelManager.ShowPanel();
        panelManager.SetPanelText(panelText);
        panelManager.SetPanelImage(dropSpellPieces[index].ToString());

        Destroy(this.gameObject);
    }
}
