using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Grace Ko
/// singleton used to track player's spells
/// instantiated in MainPageHandler.setUpMainPage();
/// </summary>
public class SpellTracker : MonoBehaviour
{
    public static SpellTracker instance = null;

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
    public void UpdateActiveSpells(string spellName)
    {
        // if spell is active
        if (spellCaster.activeSpells.Any(x => x.sSpellName.Equals(spellName)))
        {
            // removing D8 after it is used
            if (spellName.Equals("Brew - Potion of Luck"))
            {
                spellCaster.dice["D8"] -= 1;
            }
            // remove spell from active spells list once it wears off
            foreach (Spell entry in spellCaster.chapter.spellsCollected)
            {
                if (entry.sSpellName.Equals(spellName))
                {
                    spellCaster.activeSpells.Remove(entry);
                    Debug.Log(entry.sSpellName + " was removed from active spells list");
                }
            }
        }
    }

    // call this after D8 is used up
    public void EndPotionofLuck()
    {
        // if potion of luck is an active spell, remove the dice and the spell from active spells list
        if(spellCaster.activeSpells.Any(x => x.sSpellName.Equals("Brew - Potion of Luck")))
        {
            spellCaster.dice["D8"] -= 1;

            foreach(Spell entry in spellCaster.chapter.spellsCollected)
            {
                if(entry.sSpellName.Equals("Brew - Potion of Luck"))
                    spellCaster.activeSpells.Remove(entry);
            }
        }
    }

    // call this after player rolls
    public void EndEcho()
    {
        // if echo is an active spell, remove spell from active spells list
        if (spellCaster.activeSpells.Any(x => x.sSpellName.Equals("Echo")))
        {
            foreach (Spell entry in spellCaster.chapter.spellsCollected)
            {
                if (entry.sSpellName.Equals("Echo"))
                {
                    spellCaster.activeSpells.Remove(entry);
                    Debug.Log(entry.sSpellName + " was removed from active spells list");
                }
            }
        }
    }

    // call this to end tailwind spell (after player rolls)
    public void EndTailwind()
    {
        // if tailwind is an active spell, remove the spell from active spells list
        if (spellCaster.activeSpells.Any(x => x.sSpellName.Equals("Tailwind")))
        {
            foreach (Spell entry in spellCaster.chapter.spellsCollected)
            {
                if (entry.sSpellName.Equals("Tailwind"))
                    spellCaster.activeSpells.Remove(entry);
            }
        }
    }
}
