using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellCollectionHandler : MonoBehaviour
{
    [SerializeField] private Button spellButton;
    [SerializeField] private Button castButton;
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private Text noSpellsText;
    [SerializeField] private Text spellPanelTitle;
    [SerializeField] private Text spellPanelInfo;

    private bool spellPanelOpen;
    private Color combatColor;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        if (localPlayer.Spellcaster.chapter.spellsCollected.Count > 0)
            noSpellsText.text = "";

        combatColor = new Color();
        ColorUtility.TryParseHtmlString("#253390", out combatColor);

        int yPos = 105;
        // add buttons for each spell the player has collected
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsCollected.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton, GameObject.Find("Canvas").transform);
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsCollected[i].sSpellName;
            newSpellButton.transform.localPosition = new Vector3(0, yPos, 0);

            // if it's a combat spell, change button/text color
            if (localPlayer.Spellcaster.chapter.spellsCollected[i].combatSpell)
            {
                newSpellButton.GetComponent<Image>().color = combatColor;
                newSpellButton.transform.GetChild(0).GetComponent<Text>().color = combatColor;
            }

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => OpenSpellPanel(localPlayer.Spellcaster.chapter.spellsCollected[i2]));

            // to position new button underneath prev button
            yPos -= 200;
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
