using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellCollectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject spellButtonPrefab;
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private GameObject UIScrollable;
    [SerializeField] private Sprite combatIcon;
    [SerializeField] private Sprite nonCombatIcon;
    [SerializeField] private Button castButton;
    [SerializeField] private Text noSpellsText;
    [SerializeField] private Text spellPanelTitle;
    [SerializeField] private Text spellPanelInfo;
    [SerializeField] private Text spellDescription;

    [SerializeField] private Color tier3Color;
    [SerializeField] private Color tier2Color;
    [SerializeField] private Color tier1Color;

    private UIScrollableController scrollController;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        scrollController = UIScrollable.GetComponent<UIScrollableController>();

        if (localPlayer.Spellcaster.chapter.spellsCollected.Count > 0)
            noSpellsText.text = "";

        // disable spell cast/charge if not player's turn
        if (!localPlayer.bIsMyTurn)
        {
            castButton.interactable = false;
        }

        foreach (Spell s in localPlayer.Spellcaster.chapter.spellsCollected)
        {
            GameObject spellButton = Instantiate(spellButtonPrefab);
            UISpellButtonController buttonController = spellButton.GetComponent<UISpellButtonController>();
            buttonController.SetTitle(s.sSpellName);
            buttonController.SetText(s.sSpellInfo);

            // set button icons
            if (s.combatSpell)
                buttonController.SetIcon(combatIcon);
            else
                buttonController.SetIcon(nonCombatIcon);

            // set button colors
            if (s.iTier == 3)
                buttonController.SetColor(tier3Color);
            else if (s.iTier == 2)
                buttonController.SetColor(tier2Color);
            else
                buttonController.SetColor(tier1Color);

            scrollController.AddElement(spellButton);
            spellButton.GetComponent<Button>().onClick.AddListener(() => OpenSpellPanel(s));
        }

        // set panel holder as last sibling
        PanelHolder.instance.SetPanelHolderLast();
    }

    private void OpenSpellPanel(Spell spell)
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        // reset cast button
        castButton.onClick.RemoveAllListeners();

        string combat = "Non-Combat";
        if (spell.combatSpell)
        {
            combat = "Combat";
            castButton.transform.GetChild(0).GetComponent<Text>().text = "Charge!";
        }
        else
            castButton.transform.GetChild(0).GetComponent<Text>().text = "Cast!";

        // set spell name and info
        spellPanelTitle.text = spell.sSpellName;
        spellPanelInfo.text = "Cost: " + spell.iManaCost + "  |  " + combat;
        if (spell.combatSpell)
            spellPanelInfo.text = spellPanelInfo.text + "  |  Charges: " + spell.iCharges.ToString();
        spellDescription.text = spell.sSpellInfo;

        // add onclick listener to cast button
        castButton.onClick.AddListener(() => OnCastClick(spell));

        spellPanel.SetActive(true);
    }
    private void OnCastClick(Spell spell)
    {
        if(localPlayer.Spellcaster.fCurrentHealth <= 0)
        {
            PanelHolder.instance.displayNotify("No Health", "You cannot cast or charge spells if you do not have health.", "OK");
        }
        // if plague was failed, player cant cast spells
        else if(localPlayer.Spellcaster.plagueConsequence)
        {
            PanelHolder.instance.displayNotify("Plague Consequence", "You're too sick to cast or charge any spells right now!", "OK");
        }
        // if player has already cast 2 spells this turn and Agenda is not active
        else if (localPlayer.Spellcaster.numSpellsCastThisTurn >= 2 && !SpellTracker.instance.agendaActive)
        {
            PanelHolder.instance.displayNotify("Too Many Spells", "You already cast 2 spells this turn.", "OK");
        }
        // don't let player cast repeat spells
        else if (SpellTracker.instance.SpellIsActive(spell))
        {
            PanelHolder.instance.displayNotify("Already Active", spell.sSpellName + " is already active.", "OK");
        }
        // if it's a combat spell, add a charge
        else if (spell.combatSpell)
        {
            spell.Charge(localPlayer.Spellcaster);
            spellPanelInfo.text = "You now have " + spell.iCharges + " charges to this spell.";
        }
        else
        {
            spell.SpellCast(localPlayer.Spellcaster);
        }
    }
}
