using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to enemy prefab
public class Enemy : MonoBehaviour
{
    private PanelManager panelManager;
    private Player localPlayer;

    public float fMaxHealth;
    public float fCurrentHealth;

    private List<string> dropSpellPieces;

    // call this after instantiating enemy 
    public void Initialize(float maxHealth)
    {
        panelManager = GameObject.Find("ScriptContainer").GetComponent<PanelManager>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

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

    // drops random spell piece & mana when enemy dies
    public void EnemyDefeated()
    {
        int index = Random.Range(0, 6);
        int manaCollected = Random.Range(50, 300);

        string panelText = "You defeated the enemy!\n\n" +
                "You received 1 " + dropSpellPieces[index].ToString() + ".\nYou also received " + manaCollected.ToString() + " mana.";

        localPlayer.Spellcaster.CollectSpellPiece(dropSpellPieces[index], localPlayer.Spellcaster);
        panelManager.setPanelText(panelText);
        panelManager.showPanel();
    }
}
