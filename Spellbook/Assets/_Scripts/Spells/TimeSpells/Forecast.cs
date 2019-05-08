using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class Forecast : Spell
{
    public Forecast()
    {
        iTier = 2;
        iManaCost = 1900;

        combatSpell = false;

        sSpellName = "Forecast";
        sSpellClass = "Chronomancer";
        sSpellInfo = "See into the future and learn what the next item in the Forest will be. Gain double of that next time you enter. Can cast on an ally.";

        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Elementalist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            List<ItemObject> itemList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;
            ItemObject item = itemList[Random.Range(0, itemList.Count)];
            SpellTracker.instance.forecastItem = item;
            PanelHolder.instance.displayBoardScan("Forecast", "The next time you enter the Forest, you will gain 2 " + item.name + "s.", item.sprite, "MainPlayerScene");
            player.activeSpells.Add(this);
            SpellTracker.instance.forecastItem = item;

            SpellTracker.instance.lastSpellCasted = this;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You don't have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            List<ItemObject> itemList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;
            ItemObject item = itemList[Random.Range(0, itemList.Count)];
            SpellTracker.instance.forecastItem = item;
            PanelHolder.instance.displayBoardScan("Forecast", "The next time you enter the Forest, you will gain 2 " + item.name + "s.", item.sprite, "MainPlayerScene");
            player.activeSpells.Add(this);

            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
