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
        questTask = "Bring this item to the Alchemist Town. You received [Infused Sapphire].";

        startTurn = turnStarted;
        turnLimit = 5;

        spaceName = "town_alchemist";

        // give player item to deliver
        item = new ItemObject("Infused Sapphire", Resources.Load<Sprite>("Art Assets/Items and Currency/InfusedSapphire"), 2000, 1000, 
            "This sapphire is embued with pure arcane energy. When shattered, it gives its user a temporary power boost.", 
            "Add +6 to your next damage spell (one time use).");
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.AddToInventory(item);

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