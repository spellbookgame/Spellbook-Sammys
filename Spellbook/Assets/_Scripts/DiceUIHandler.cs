using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject diceSlot;
    [SerializeField] private GameObject dice;
    [SerializeField] private GameObject diceScrollContent;
    [SerializeField] private Button rollButton;
    [SerializeField] private Button diceButton;

    public bool hasRolled;
    private bool diceTrayOpen;

    Player localPlayer;

    private void Update()
    {
        // prevents player from closing dice tray if they rolled (hasRolled set in DiceRoll.cs)
        if(hasRolled && diceTrayOpen)
        {
            diceButton.interactable = false;
        }
    }

    // call this function from onclick event on button
    public void OpenDiceTray()
    {
        SoundManager.instance.PlaySingle(SoundManager.dicetrayopen);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        rollButton.transform.GetChild(0).GetComponent<Text>().text = "Roll!";

        if (!diceTrayOpen)
        {
            gameObject.SetActive(true);

            // if player has tailwind active, add a D6 to movement
            if(localPlayer.Spellcaster.activeSpells.Any(x => x.sSpellName.Equals("Tailwind")))
            {
                GameObject newDice = Instantiate(dice, transform.Find("slot1"));
                newDice.transform.GetChild(0).GetComponent<Image>().sprite = newDice.GetComponent<DiceRoll>().pipsSix;
                newDice.GetComponent<DiceRoll>()._rollMaximum = 6;
                // disable drag on dice
                newDice.GetComponent<DiceDragHandler>().enabled = false;
                // enable roll
                newDice.GetComponent<DiceRoll>().rollEnabled = true;
            }

            // populate dice inventory with player's dice
            foreach (KeyValuePair<string, int> kvp in localPlayer.Spellcaster.dice)
            {
                if (kvp.Value > 0)
                {
                    for(int i = 0; i < kvp.Value; ++i)
                    {
                        // instantiate a prefab of dice slot and set its parent to the inventory tray
                        GameObject clone = Instantiate(diceSlot, diceScrollContent.transform);
                        // disable roll if dice is still in inventory
                        clone.transform.GetChild(0).GetComponent<DiceRoll>().rollEnabled = false;
                        // set dice max values
                        if (kvp.Key.Equals("D4"))
                        {
                            clone.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = clone.transform.GetChild(0).GetComponent<DiceRoll>().pipsFour;
                            clone.transform.GetChild(0).GetComponent<DiceRoll>()._rollMaximum = 4;
                        }
                        else if(kvp.Key.Equals("D6"))
                        {
                            clone.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = clone.transform.GetChild(0).GetComponent<DiceRoll>().pipsSix;
                            clone.transform.GetChild(0).GetComponent<DiceRoll>()._rollMaximum = 6;
                        }
                        else if(kvp.Key.Equals("D8"))
                        {
                            clone.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = clone.transform.GetChild(0).GetComponent<DiceRoll>().pipsEight;
                            clone.transform.GetChild(0).GetComponent<DiceRoll>()._rollMaximum = 8;
                        }
                    }
                }
            }
            diceTrayOpen = true;
        }
        else
        {
            // destroy all dice in panel
            foreach (Transform child in diceScrollContent.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Slot"))
            {
                if (g.transform.childCount > 0)
                {
                    Destroy(g.transform.GetChild(0).gameObject);
                }
            }
            gameObject.SetActive(false);
            diceTrayOpen = false;
            hasRolled = false;
        }
    }
}
