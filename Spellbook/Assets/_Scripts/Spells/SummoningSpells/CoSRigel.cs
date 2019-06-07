using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoSRigel : Spell
{
    public CoSRigel()
    {
        iTier = 3;
        iManaCost = 800;

        combatSpell = false;

        sSpellName = "Rigel's Ascension";
        sSpellClass = "Summoner";
        sSpellInfo = "Destroy 2 random items to summon a tier 1 item.";

        requiredRunes.Add("Summoner C Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        if(player.inventory.Count < 2)
        {
            PanelHolder.instance.displayNotify("Not enough Items!", "You need 2 items to cast this spell.", "OK");
        }
        // cast spell for free if Umbra's Eclipse is active
        else if (SpellTracker.instance.CheckUmbra())
        {
            // destroy 2 random items from inventory
            ItemObject item1 = player.inventory[Random.Range(0, player.inventory.Count)];
            player.RemoveFromInventory(item1);
            ItemObject item2 = player.inventory[Random.Range(0, player.inventory.Count)];
            player.RemoveFromInventory(item2);

            // get a random item
            string[] items = new string[] { "Mimetic Vellum", "Crystal Mirror", "Rift Talisman" };
            ItemObject newItem = GameObject.Find("ItemList").GetComponent<ItemList>().GetItemFromName(items[Random.Range(0, 3)]);

            player.AddToInventory(newItem);
            PanelHolder.instance.displayBoardScan("Rigel's Ascension", "You destroyed " + item1.name + " and " + item2.name + " and gained " + newItem.name + "!", newItem.sprite, "MainPlayerScene");

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

            // destroy 2 random items from inventory
            ItemObject item1 = player.inventory[Random.Range(0, player.inventory.Count)];
            player.RemoveFromInventory(item1);
            ItemObject item2 = player.inventory[Random.Range(0, player.inventory.Count)];
            player.RemoveFromInventory(item2);

            // get a random item
            string[] items = new string[] { "Mimetic Vellum", "Crystal Mirror", "Rift Talisman" };
            ItemObject newItem = GameObject.Find("ItemList").GetComponent<ItemList>().GetItemFromName(items[Random.Range(0, 3)]);

            player.AddToInventory(newItem);
            PanelHolder.instance.displayBoardScan("Rigel's Ascension", "You destroyed " + item1.name + " and " + item2.name + " and gained " + newItem.name + "!", newItem.sprite, "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
