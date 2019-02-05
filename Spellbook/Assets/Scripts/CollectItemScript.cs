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

        // localPlayer.Spellcaster.spellPieces.Add(spellPieceType);
        int oldValue = (int)localPlayer.Spellcaster.spellPieces[spellPieceType];
        localPlayer.Spellcaster.spellPieces[spellPieceType] = oldValue + 1;

        // setting text of notification panel
        combatUIManager.Text_notify.text = "You found a " + spellPieceType + "!\n\nYou now have " +
                            (int)localPlayer.Spellcaster.spellPieces[spellPieceType] + " " + spellPieceType + ".";
    }
}
