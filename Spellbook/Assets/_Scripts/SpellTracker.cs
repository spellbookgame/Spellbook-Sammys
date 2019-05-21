using System.Linq;
using UnityEngine;

/// <summary>
/// Grace Ko
/// singleton used to track player's spells
/// instantiated in MainPageHandler.setUpMainPage();
/// </summary>
public class SpellTracker : MonoBehaviour
{
    public static SpellTracker instance = null;

    // public bools to track specific spell attributes
    public ItemObject forecastItem;
    public Spell lastSpellCasted;
    public bool agendaActive;

    private SpellCaster spellCaster;

    void Awake()
    {
        //Check if there is already an instance of SpellTracker
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of QuestTracker.
            Destroy(gameObject);

        //Set QuestTracker to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        spellCaster = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster;
    }

    // making sure player is notified if a spell wears off
    public void RemoveFromActiveSpells(string spellName)
    {
        // if spell is active
        if (spellCaster.activeSpells.Any(x => x.sSpellName.Equals(spellName)))
        {
            // remove spell from active spells list once it wears off
            foreach (Spell entry in spellCaster.activeSpells)
            {
                if (entry.sSpellName.Equals(spellName))
                {
                    spellCaster.activeSpells.Remove(entry);
                    Debug.Log(entry.sSpellName + " was removed from active spells list");
                    break;
                }
            }
        }
    }

    // checks to see if spell is in player's list of active spells
    public bool SpellIsActive(Spell s)
    {
        if (spellCaster.activeSpells.Any(x => x.sSpellName.Equals(s.sSpellName)))
        {
            return true;
        }
        else
            return false;
    }

    public bool CheckUmbra()
    {
        if (spellCaster.activeSpells.Any(x => x.sSpellName.Equals("Umbra's Eclipse")))
        {
            RemoveFromActiveSpells("Umbra's Eclipse");
            return true;
        }
        else
            return false;
    }

    // called from ForestSceneHandler.cs
    public void DoForecast()
    {
        if(forecastItem != null)
        {
            // add 2 of the items into inventory
            spellCaster.AddToInventory(forecastItem);
            spellCaster.AddToInventory(forecastItem);
            PanelHolder.instance.displayBoardScan("Forecast Active", "Because of Forecast, you found 2 " + forecastItem.name + "!", forecastItem.sprite, "MainPlayerScene");

            // reset forecast item and remove forecast from active spells list
            forecastItem = null;
        }
        RemoveFromActiveSpells("Forecast");
    }
}