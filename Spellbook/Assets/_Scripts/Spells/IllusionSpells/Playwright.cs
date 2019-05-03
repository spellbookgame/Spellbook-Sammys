using System.Collections.Generic;
using UnityEngine;

// spell for Illusionist class
public class Playwright : Spell
{
    public Playwright()
    {
        iTier = 1;
        iManaCost = 2000;

        combatSpell = false;

        sSpellName = "Playwright";
        sSpellClass = "Illusionist";
        sSpellInfo = "Destroy a random item to change two of your runes into any runes of your choice.";

        requiredRunes.Add("Illusionist A Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Arcanist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        if (player.inventory.Count <= 0)
        {
            PanelHolder.instance.displayNotify("No Items!", "You have no items to sacrifice to cast this spell.", "OK");
        }
        // cast spell for free if Umbra's Eclipse is active
        else if (SpellTracker.instance.CheckUmbra())
        {
            string itemName;
            // take a random index from the inventory of items and remove it 
            List<ItemObject> playerInventory = new List<ItemObject>(player.inventory);
            if (playerInventory.Count <= 1)
            {
                itemName = playerInventory[0].name;
                player.RemoveFromInventory(playerInventory[0]);
            }
            else
            {
                int rIndex = (int)UnityEngine.Random.Range(0, playerInventory.Count);
                itemName = playerInventory[rIndex].name;
                player.RemoveFromInventory(playerInventory[rIndex]);
            }

            PanelHolder.instance.displayNotify(sSpellName, "You destroyed " + itemName + ". Discard 2 of your runes and choose 2 runes of your choice from the deck.", "OK");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            string itemName;
            // take a random index from the inventory of items and remove it 
            List<ItemObject> playerInventory = new List<ItemObject>(player.inventory);
            if(playerInventory.Count <= 1)
            {
                itemName = playerInventory[0].name;
                player.RemoveFromInventory(playerInventory[0]);
            }
            else
            {
                int rIndex = (int)UnityEngine.Random.Range(0, playerInventory.Count);
                itemName = playerInventory[rIndex].name;
                player.RemoveFromInventory(playerInventory[rIndex]);
            }

            PanelHolder.instance.displayNotify(sSpellName, "You destroyed " +  itemName + ". Discard 2 of your runes and choose 2 runes of your choice from the deck.", "OK");
        }
    }
}
