using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoDCorpse : Spell
{
    public CoDCorpse()
    {
        iTier = 2;
        iManaCost = 2100;

        combatSpell = false;

        sSpellName = "Corpse Taker";
        sSpellClass = "Summoner";
        sSpellInfo = "Find 2 random items.";

        requiredRunes.Add("Summoner B Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            // get 2 random items
            List<ItemObject> itemsList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;
            ItemObject item1 = itemsList[Random.Range(0, itemsList.Count)];
            ItemObject item2 = itemsList[Random.Range(0, itemsList.Count)];

            player.AddToInventory(item1);            
            player.AddToInventory(item2);

            PanelHolder.instance.displayNotify("Corpse Taker", "You found " + item1.name + " and " + item2.name + "!", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            // get 2 random items
            List<ItemObject> itemsList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;
            ItemObject item1 = itemsList[Random.Range(0, itemsList.Count)];
            ItemObject item2 = itemsList[Random.Range(0, itemsList.Count)];

            player.AddToInventory(item1);
            player.AddToInventory(item2);

            PanelHolder.instance.displayNotify("Corpse Taker", "You found " + item1.name + " and " + item2.name + "!", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
