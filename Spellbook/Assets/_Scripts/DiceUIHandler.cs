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

    [SerializeField] private Button spellBookButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button endTurnButton;

    public bool diceTrayOpen;
    private int numDice;

    public Button rollButton;
    public Button scanButton;
    public bool diceLocked;

    Player localPlayer;

    private void Start()
    {
        scanButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            OpenCloseDiceTray();
            SceneManager.LoadScene("VuforiaScene");
        });
    }

    // call this function from onclick event on button
    public void OpenCloseDiceTray()
    {
        SoundManager.instance.PlaySingle(SoundManager.dicetrayopen);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // set dice tray position to 0
        transform.localPosition = new Vector3(0, 0, 0);

        // if player hasn't rolled yet, enable roll button
        if (!localPlayer.Spellcaster.hasRolled)
            rollButton.interactable = true;

        // if dice are not locked, reset dice when opening tray
        if (!diceTrayOpen && !localPlayer.Spellcaster.hasRolled)
        {
            gameObject.SetActive(true);

            // populate the scroll rect with player's dice
            PopulateScrollRect();

            // if player has Tailwind active, add a D6 to movement slot
            if (localPlayer.Spellcaster.activeSpells.Any(x => x.sSpellName.Equals("Tailwind")))
            {
                D6ToMovement();
            }
            // if player has Allegro active, add a D6 to movement slot
            if (localPlayer.Spellcaster.activeSpells.Any(x => x.sSpellName.Equals("Allegro")))
            {
                D6ToMovement();
            }
            
            // disable spellbook/inventory buttons while dice tray is open
            spellBookButton.interactable = false;
            spellBookButton.transform.GetChild(0).gameObject.SetActive(false);
            inventoryButton.interactable = false;
            inventoryButton.transform.GetChild(0).gameObject.SetActive(false);

            diceTrayOpen = true;
        }
        // if dice tray is closed and dice are locked, open tray the way the dice were placed
        else if(!diceTrayOpen && localPlayer.Spellcaster.hasRolled)
        {
            gameObject.SetActive(true);

            // disable spellbook/inventory buttons while dice tray is open
            spellBookButton.interactable = false;
            spellBookButton.transform.GetChild(0).gameObject.SetActive(false);
            inventoryButton.interactable = false;
            inventoryButton.transform.GetChild(0).gameObject.SetActive(false);

            diceTrayOpen = true;
        }
        // if dice are not locked, reset dice when panel is closed
        else if(diceTrayOpen && !localPlayer.Spellcaster.hasRolled)
        {
            UICanvasHandler.instance.ActivateEndTurnButton(localPlayer.Spellcaster.hasRolled);

            RemoveDiceFromSlots();

            gameObject.SetActive(false);

            // enable spellbook/inventory buttons while dice tray is closed
            spellBookButton.interactable = true;
            spellBookButton.transform.GetChild(0).gameObject.SetActive(true);
            inventoryButton.interactable = true;
            inventoryButton.transform.GetChild(0).gameObject.SetActive(true);

            diceTrayOpen = false;
        }
        // if dice are locked, keep dice the same when panel is closed
        else if(diceTrayOpen && localPlayer.Spellcaster.hasRolled)
        {
            UICanvasHandler.instance.ActivateEndTurnButton(localPlayer.Spellcaster.hasRolled);

            gameObject.SetActive(false);

            // enable spellbook/inventory buttons while dice tray is closed
            spellBookButton.interactable = true;
            spellBookButton.transform.GetChild(0).gameObject.SetActive(true);
            inventoryButton.interactable = true;
            inventoryButton.transform.GetChild(0).gameObject.SetActive(true);

            diceTrayOpen = false;
        }
    }

    private void PopulateScrollRect()
    {
        // just in case there are some dice that didn't get reset
        RemoveDiceFromSlots();

        // populate dice inventory with player's dice
        foreach (KeyValuePair<string, int> kvp in localPlayer.Spellcaster.dice)
        {
            if (kvp.Value > 0)
            {
                for (int i = 0; i < kvp.Value; ++i)
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
                    else if (kvp.Key.Equals("D6"))
                    {
                        clone.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = clone.transform.GetChild(0).GetComponent<DiceRoll>().pipsSix;
                        clone.transform.GetChild(0).GetComponent<DiceRoll>()._rollMaximum = 6;
                    }
                    else if (kvp.Key.Equals("D8"))
                    {
                        clone.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = clone.transform.GetChild(0).GetComponent<DiceRoll>().pipsEight;
                        clone.transform.GetChild(0).GetComponent<DiceRoll>()._rollMaximum = 8;
                    }
                    // track num of dice to expand scroll rect
                    ++numDice;
                }
            }
        }
        // expand scroll rect for each die that exceeds 4
        if (numDice > 4)
        {
            RectTransform rect = diceScrollContent.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2((float)rect.sizeDelta.x + (260 * (numDice - 4)), rect.sizeDelta.y);
        }
    }

    private void RemoveDiceFromSlots()
    {
        // destroy all dice in scroll rect and tray slots
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
    }

    // add a D6 into a movement slot if given a temporary dice
    private void D6ToMovement()
    {
        Debug.Log("Adding D6 to movement...");
        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot"))
        {
            if (slot.name.Equals("slot1") && slot.transform.childCount == 0)
            {
                Debug.Log("slot found!");
                GameObject newDice = Instantiate(dice, slot.transform);
                newDice.transform.GetChild(0).GetComponent<Image>().sprite = newDice.GetComponent<DiceRoll>().pipsSix;
                newDice.GetComponent<DiceRoll>()._rollMaximum = 6;
                // disable drag on dice
                newDice.GetComponent<DiceDragHandler>().enabled = false;
                // enable roll
                newDice.GetComponent<DiceRoll>().rollEnabled = true;
                break;
            }
        }
    }
}
