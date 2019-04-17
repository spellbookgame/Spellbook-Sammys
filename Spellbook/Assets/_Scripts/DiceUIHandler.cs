using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject diceSlot;
    [SerializeField] private GameObject diceScrollContent;
    private bool diceTrayOpen;

    Player localPlayer;

    // call this function from onclick event on button
    public void OpenDiceTray()
    {
        SoundManager.instance.PlaySingle(SoundManager.dicetrayopen);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        if (!diceTrayOpen)
        {
            gameObject.SetActive(true);
            // populate dice inventory with player's dice
            foreach (KeyValuePair<string, int> kvp in localPlayer.Spellcaster.dice)
            {
                if (kvp.Value > 0)
                {
                    // instantiate a prefab of dice slot and set its parent to the inventory tray
                    Instantiate(diceSlot, diceScrollContent.transform);
                }
            }
            diceTrayOpen = true;
        }
        else
        {
            // destroy all dice in panel
            foreach(Transform child in diceScrollContent.transform)
            {
                Destroy(child.gameObject);
            }
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Slot"))
            {
                if(g.transform.childCount > 0)
                {
                    Destroy(g.transform.GetChild(0).gameObject);
                }
            }
            gameObject.SetActive(false);
            diceTrayOpen = false;
        }
    }
}
