using UnityEngine;

public class CollectItemScript : MonoBehaviour
{
    // [SerializeField] private GameObject scriptContainer;
    private CombatUIManager combatUIManager;
    Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        combatUIManager = gameObject.GetComponent<CombatUIManager>();
    }

    // currently called in CombatUIManager.cs for debugging
    public void CollectSpellPiece(string spellPieceType)
    {
        // setting notifyPanel to active
        if (combatUIManager.bnotifyPanelOpen == false)
        {
            combatUIManager.closePanels();
            combatUIManager.Panel_notify.SetActive(true);
            combatUIManager.bnotifyPanelOpen = true;
        }
        // increment player's number of spell pieces
        // this can be changed to other kinds of spell pieces later
        switch(spellPieceType)
        {
            case "Arcane":
                localPlayer.Spellcaster.iArcanePieces++;
                // setting text of notification panel
                combatUIManager.Text_notify.text = "You found a " + spellPieceType + " spell piece!\n\nYou now have " +
                                    localPlayer.Spellcaster.iArcanePieces + " spell pieces.";
                break;
            case "Alchemy":
                localPlayer.Spellcaster.iAlchemyPieces++;
                combatUIManager.Text_notify.text = "You found a " + spellPieceType + " spell piece!\n\nYou now have " +
                                    localPlayer.Spellcaster.iAlchemyPieces + " spell pieces.";
                break;
            case "Chronomancy":
                localPlayer.Spellcaster.iChronomancyPieces++;
                combatUIManager.Text_notify.text = "You found a " + spellPieceType + " spell piece!\n\nYou now have " +
                                    localPlayer.Spellcaster.iChronomancyPieces + " spell pieces.";
                break;
            case "Elemental":
                localPlayer.Spellcaster.iElementalPieces++;
                combatUIManager.Text_notify.text = "You found a " + spellPieceType + " spell piece!\n\nYou now have " +
                                    localPlayer.Spellcaster.iElementalPieces + " spell pieces.";
                break;
            case "Summoning":
                localPlayer.Spellcaster.iSummoningPieces++;
                combatUIManager.Text_notify.text = "You found a " + spellPieceType + " spell piece!\n\nYou now have " +
                                    localPlayer.Spellcaster.iSummoningPieces + " spell pieces.";
                break;
            case "Trickster":
                localPlayer.Spellcaster.iTricksterPieces++;
                combatUIManager.Text_notify.text = "You found a " + spellPieceType + " spell piece!\n\nYou now have " +
                                    localPlayer.Spellcaster.iTricksterPieces + " spell pieces.";
                break;
            default:
                break;
        }
    }
}
