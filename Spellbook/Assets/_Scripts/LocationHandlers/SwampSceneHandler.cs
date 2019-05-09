using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwampSceneHandler : MonoBehaviour
{
    [SerializeField] private Button buffLandButton;
    [SerializeField] private Button blackMagicSpellButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Text dialogueText;

    private bool requested;
    private bool requestCompleted;

    private Player localPlayer;
    private ItemList itemList;
    private ItemObject requiredItem;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        itemList = GameObject.Find("ItemList").GetComponent<ItemList>();

        // for black magic spell button
        List<ItemObject> highTiers = new List<ItemObject>();
        highTiers.AddRange(itemList.tier1Items);
        highTiers.AddRange(itemList.tier2Items);
        requiredItem = highTiers[Random.Range(0, highTiers.Count)];

        // add onclick listeners
        buffLandButton.onClick.AddListener(BuffLand);
        blackMagicSpellButton.onClick.AddListener(BlackMagicSpell);
        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("location_swamp");
        QuestTracker.instance.TrackErrandQuest("location_swamp");
    }

    private void BuffLand()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        if (!requested)
        {
            dialogueText.text = "I require an item...";
            buffLandButton.transform.GetChild(0).GetComponent<Text>().text = "Give an item!";
            requested = true;
        }
        else if(requested)
        {
            if(localPlayer.Spellcaster.inventory.Count <= 0)
            {
                dialogueText.text = "Fool! You have no items to give. Leave my swamp!";
            }
            else
            {
                if(!requestCompleted)
                {
                    int randIndex = Random.Range(0, localPlayer.Spellcaster.inventory.Count - 1);
                    string itemName = localPlayer.Spellcaster.inventory[randIndex].name;
                    localPlayer.Spellcaster.RemoveFromInventory(localPlayer.Spellcaster.inventory[randIndex]);
                    dialogueText.text = "I'll take your " + itemName + ". You will receive double mana from the land next round.";
                    requestCompleted = true;
                }
                else
                {
                    dialogueText.text = "I only accept one request per visit.";
                }
            }
        }
    }

    private void BlackMagicSpell()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        // if player hasn't requested for something yet
        if (!requested)
        {
            
            dialogueText.text = "If you want a forbidden spell, I require a " + requiredItem.name + ".";

            blackMagicSpellButton.transform.GetChild(0).GetComponent<Text>().text = "Give " + requiredItem.name + "!";
            requested = true;
        }
        else if(requested)
        {
            // if player does not have item
            if(!localPlayer.Spellcaster.inventory.Any(x => x.name.Equals(requiredItem.name)))
            {
                dialogueText.text = "Fool! You do not have a " + requiredItem.name + ". Leave my swamp!";
            }
            else
            {
                // if player hasn't had a request completed yet
                if(!requestCompleted)
                {
                    // remove item from player's inventory
                    foreach(ItemObject i in localPlayer.Spellcaster.inventory)
                    {
                        if(i.name.Equals(requiredItem.name))
                        {
                            localPlayer.Spellcaster.RemoveFromInventory(i);
                            break;
                        }
                    }

                    List<Spell> blackMagicSpells = new List<Spell>()
                    {
                        new HollowCreation(),
                        new DarkRevelation(),
                        new SpacialWarp(),
                        new Fortune(),
                        new Agenda()
                    };

                    Spell randomSpell = blackMagicSpells[Random.Range(0, blackMagicSpells.Count)];

                    localPlayer.Spellcaster.chapter.spellsCollected.Add(randomSpell);
                    PanelHolder.instance.displayNotify(randomSpell.sSpellName, "The Witch took your " + requiredItem.name + " and granted you the " + randomSpell.sSpellName + " spell!", "MainPlayerScene");

                    requestCompleted = true;
                }
                else
                {
                    dialogueText.text = "I only accept one request per visit.";
                }
            }
        }
    }
}
