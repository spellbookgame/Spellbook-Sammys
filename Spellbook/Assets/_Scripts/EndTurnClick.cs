using UnityEngine;

public class EndTurnClick : MonoBehaviour
{
    public void OnEndTurnClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Player localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        bool endSuccessful = localPlayer.onEndTurnClick();
        if (endSuccessful)
        {
            // reset player attribute values
            localPlayer.Spellcaster.hasAttacked = false;
            localPlayer.Spellcaster.hasRolled = false;
            localPlayer.Spellcaster.scannedSpaceThisTurn = false;

            // collect end of turn mana
            int manaCollected = localPlayer.Spellcaster.CollectManaEndTurn();
            GameObject.Find("ScriptContainer").GetComponent<MainPageHandler>().DisplayMana(manaCollected);

            // disable buttons
            UICanvasHandler.instance.ActivateEndTurnButton(false);
            UICanvasHandler.instance.EnableDiceButton(false);
        }
    }
}
