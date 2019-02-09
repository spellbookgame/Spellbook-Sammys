using UnityEngine;
using UnityEngine.UI;

// script to manage UI in CombatScene
public class CombatUIManager : MonoBehaviour
{
    // serializefield private variables
    [SerializeField] private GameObject Panel_help;
    [SerializeField] private Text Text_mana;

    // private variables
    private bool bHelpOpen = false;
    private HealthManager healthManager;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        healthManager = gameObject.GetComponent<HealthManager>();

        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();
    }

    private void Update()
    {
        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();
    }

    // when help button is clicked
    public void helpClick()
    {
        if (bHelpOpen == false)
        {
            Panel_help.SetActive(true);
            bHelpOpen = true;
        }
        else if (bHelpOpen == true)
        {
            Panel_help.SetActive(false);
            bHelpOpen = false;
        }
    }

    public void attackClick()
    {
        healthManager.HitEnemy(localPlayer.Spellcaster.fBasicAttackStrength);
    }

    // ----------------------------------- DEBUGGING: ALL SPELL PIECE BUTTONS ------------------------------------------
    public void arcaneSPClick()
    {
        localPlayer.Spellcaster.CollectSpellPiece("Arcane Spell Piece", localPlayer.Spellcaster);
    }
    public void alchemySPClick()
    {
        localPlayer.Spellcaster.CollectSpellPiece("Alchemy Spell Piece", localPlayer.Spellcaster);
    }
    public void chronomancySPClick()
    {
        localPlayer.Spellcaster.CollectSpellPiece("Time Spell Piece", localPlayer.Spellcaster);
    }
    public void elementalSPClick()
    {
        localPlayer.Spellcaster.CollectSpellPiece("Elemental Spell Piece", localPlayer.Spellcaster);
    }
    public void summoningSPClick()
    {
        localPlayer.Spellcaster.CollectSpellPiece("Summoning Spell Piece", localPlayer.Spellcaster);
    }
    public void tricksterSPClick()
    {
        localPlayer.Spellcaster.CollectSpellPiece("Illusion Spell Piece", localPlayer.Spellcaster);
    }
}
