using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ElementalErrandQuest : Quest
{
    public ElementalErrandQuest(int turnStarted)
    {
        questName = "Errand Quest";
        questType = "Errand";
        questDescription = "Bring this item to the Alchemist Town. (You received [item name]).";

        startTurn = turnStarted;
        turnLimit = 5;

        spaceName = "town_alchemist";

        // Item item = new Item();
        // spellcaster.AddToInventory(item);

        List<string> rewardList = new List<string>();
        rewardList.Add("Elemental A Glyph");
        rewardList.Add("Elemental B Glyph");

        rewards.Add("Glyph", rewardList);

        questCompleted = false;
    }

    // return a string that contains the rewards of the quest
    public override string DisplayReward()
    {
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<string, List<string>> kvp in rewards)
        {
            foreach (string s in kvp.Value)
            {
                sb.Append(s);
                sb.Append("\n");
            }
        }

        return sb.ToString();
    }
}