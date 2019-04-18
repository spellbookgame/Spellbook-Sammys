using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Grace Ko
// Populating dice tray when gameobject is set active
public class DiceUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject diceSlot;
    [SerializeField] private GameObject dice;
    [SerializeField] private GameObject diceScrollContent;

    [SerializeField] private Button diceButton;
    [SerializeField] private Button spellBookButton;
    [SerializeField] private Button inventoryButton;

    private bool diceTrayOpen;

    public Button rollButton;
    public Button scanButton;
    public bool diceLocked;

    Player localPlayer;

    private void Start()
    {
        scanButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("VuforiaScene");
        });
        // disable scan button until player has rolled
        scanButton.interactable = false;
    }

    // call this function from onclick event on button
    public void OpenDiceTray()
    {
        SoundManager.instance.PlaySingle(SoundManager.dicetrayopen);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // if dice are not locked, populate dice normally
        if (!diceTrayOpen && !diceLocked)
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
            // disable spellbook/inventory buttons while dice tray is open
            spellBookButton.interactable = false;
            inventoryButton.interactable = false;

            diceTrayOpen = true;
        }
        // if dice tray is closed and dice are locked, open tray the way the dice were originally
        else if(!diceTrayOpen && diceLocked)
        {
            gameObject.SetActive(true);

            // disable spellbook/inventory buttons while dice tray is open
            spellBookButton.interactable = false;
            inventoryButton.interactable = false;

            diceTrayOpen = true;
        }
        // if dice are not locked, reset dice when panel is closed
        else if(diceTrayOpen && !diceLocked)
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

            // enable spellbook/inventory buttons while dice tray is open
            spellBookButton.interactable = true;
            inventoryButton.interactable = true;

            diceTrayOpen = false;
        }
        // if dice are locked, keep dice the same when panel is closed
        else if(diceTrayOpen && diceLocked)
        {
            gameObject.SetActive(false);

            // enable spellbook/inventory buttons while dice tray is open
            spellBookButton.interactable = true;
            inventoryButton.interactable = true;

            diceTrayOpen = false;
        }
    }
}
