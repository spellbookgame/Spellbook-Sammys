using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spell for all clases
public class HollowCreation : Spell
{
    public HollowCreation()
    {
        iTier = 1;
        iManaCost = 2500;

        combatSpell = false;

        sSpellName = "Hollow Creation";
        sSpellClass = "";
        sSpellInfo = "Create 3 random Tier 1 or 2 items.";
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            ItemList itemList = GameObject.Find("ItemList").GetComponent<ItemList>();
            List<ItemObject> highTiers = new List<ItemObject>();
            highTiers.AddRange(itemList.tier1Items);
            highTiers.AddRange(itemList.tier2Items);

            ItemObject item1 = highTiers[Random.Range(0, highTiers.Count)];            
            ItemObject item2 = highTiers[Random.Range(0, highTiers.Count)];            
            ItemObject item3 = highTiers[Random.Range(0, highTiers.Count)];

            player.AddToInventory(item1);
            player.AddToInventory(item2);
            player.AddToInventory(item3);

            PanelHolder.instance.displayNotify("Hollow Creation", "You created " + item1.name + ", " + item2.name + ", and " + item3.name + ".", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            ItemList itemList = GameObject.Find("ItemList").GetComponent<ItemList>();
            List<ItemObject> highTiers = new List<ItemObject>();
            highTiers.AddRange(itemList.tier1Items);
            highTiers.AddRange(itemList.tier2Items);

            ItemObject item1 = highTiers[Random.Range(0, highTiers.Count)];
            ItemObject item2 = highTiers[Random.Range(0, highTiers.Count)];
            ItemObject item3 = highTiers[Random.Range(0, highTiers.Count)];

            player.AddToInventory(item1);
            player.AddToInventory(item2);
            player.AddToInventory(item3);

            PanelHolder.instance.displayNotify("Hollow Creation", "You created " + item1.name + ", " + item2.name + ", and " + item3.name + ".", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
        }
    }
}
