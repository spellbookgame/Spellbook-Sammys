using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummonerTownHandler : MonoBehaviour
{
    [SerializeField] private Button findQuestButton;
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button pickupItemButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Button crisisButton;
    [SerializeField] private Text dialogueText;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        if (CrisisHandler.instance.currentCrisis.Equals("Intervention"))
        {
            crisisButton.gameObject.SetActive(true);
            crisisButton.onClick.AddListener(CheckCrisis);
        }

        findQuestButton.onClick.AddListener(FindQuest);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Quest summonManaQuest = new SummoningManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
        if (QuestTracker.instance.HasQuest(summonManaQuest))
        {
            PanelHolder.instance.displayNotify("Summoner Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            PanelHolder.instance.displayQuest(summonManaQuest);
        }
    }

    private void CheckCrisis()
    {
        if (!localPlayer.Spellcaster.classType.Equals("Summoner"))
        {
            dialogueText.text = "You are not the Summoner, you cannot resolve this crisis.";
        }
        else
        {
            if(localPlayer.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 2))
            {
                // look for the tier 2 spell (find a better way to do this)
                foreach (Spell s in localPlayer.Spellcaster.chapter.spellsCollected)
                {
                    if (s.iTier == 2)
                    {
                        dialogueText.text = "Will you cast " + s.sSpellName + " to resolve this Divine Intervention?";
                        crisisButton.transform.GetChild(0).GetComponent<Text>().text = "Cast the spell!";
                        // remove current onclick listener and add new one
                        crisisButton.onClick.RemoveAllListeners();
                        crisisButton.onClick.AddListener(() => CastCrisisSpell(s));
                        break;
                    }
                }
            }
            else
            {
                dialogueText.text = "You do not have any tier 2 spells created. Come back when you are stronger, Summoner.";
            }
        }
    }

    private void CastCrisisSpell(Spell spell)
    {
        spell.SpellCast(localPlayer.Spellcaster);
        CrisisHandler.instance.CheckCrisis(localPlayer, CrisisHandler.instance.currentCrisis, "town_summoner");
    }
}