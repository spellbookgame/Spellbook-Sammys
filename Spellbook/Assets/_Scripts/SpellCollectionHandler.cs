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

    private bool spellPanelOpen;
    private UIScrollableController scrollController;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        scrollController = UIScrollable.GetComponent<UIScrollableController>();

        if (localPlayer.Spellcaster.chapter.spellsCollected.Count > 0)
            noSpellsText.text = "";

        foreach(Spell s in localPlayer.Spellcaster.chapter.spellsCollected)
        {
            GameObject spellButton = Instantiate(spellButtonPrefab);
            UISpellButtonController buttonController = spellButton.GetComponent<UISpellButtonController>();
            buttonController.SetTitle(s.sSpellName);
            buttonController.SetText(s.sSpellInfo);
            if (s.combatSpell)
                buttonController.SetIcon(combatIcon);
            else
                buttonController.SetIcon(nonCombatIcon);

            scrollController.AddElement(spellButton);
            spellButton.GetComponent<Button>().onClick.AddListener(() => OpenSpellPanel(s));
        }

        // make spell panel last in hierarchy so it will appear over buttons
        spellPanel.transform.SetAsLastSibling();

        // set panel holder as last sibling
        PanelHolder.instance.SetPanelHolderLast();
    }

    private void Update()
    {
        // disable spell casting if it's not player's turn
        if(!localPlayer.bIsMyTurn)
        {
            castButton.interactable = false;
        }
    }

    private void OpenSpellPanel(Spell spell)
    {
        SoundManager.instance.PlaySingle(SoundManager.spellbookopen);

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
        spellPanelInfo.text = "Cost: " + spell.iManaCost  + "  |  " + combat + "\n\n" + spell.sSpellInfo;

        // add onclick listener to close button
        spellPanel.transform.Find("button_exit").GetComponent<Button>().onClick.AddListener(CloseSpellPanel);

        // add onclick listener to cast button
        castButton.onClick.AddListener(() => OnCastClick(spell));

        spellPanel.SetActive(true);
        spellPanelOpen = true;
    }
    
    private void CloseSpellPanel()
    {
        if(spellPanelOpen == true)
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            castButton.onClick.RemoveAllListeners();
            spellPanel.SetActive(false);
            spellPanelOpen = false;
        }
    }

    private void OnCastClick(Spell spell)
    {
        // if player has already cast 2 spells this turn and Agenda is not active
        if (localPlayer.Spellcaster.numSpellsCastThisTurn >= 2 && !SpellTracker.instance.agendaActive)
        {
            PanelHolder.instance.displayNotify("Too Many Spells", "You already cast 2 spells this turn.", "OK");
            CloseSpellPanel();
        }
        // don't let player cast repeat spells
        else if (SpellTracker.instance.SpellIsActive(spell.sSpellName))
        {
            PanelHolder.instance.displayNotify("Already Active", spell.sSpellName + " is already active.", "OK");
            CloseSpellPanel();
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
            CloseSpellPanel();
        }
    }
}
