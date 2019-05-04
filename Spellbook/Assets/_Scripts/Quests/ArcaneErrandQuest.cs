using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArcaneErrandQuest : Quest
{
    public ArcaneErrandQuest(int turnStarted)
    {
        questName = "Arcane Errand Quest";
        questType = "Errand";
        questFlavor = "This Abyssal Ore needs to be delivered to the Illusion town right away.";
        questTask = "Take this Abyssal Ore to the Illusion Town.";

        startTurn = turnStarted;
        turnLimit = 4;

        spaceName = "town_illusionist";

        item = new ItemObject("Abyssal Ore", Resources.Load<Sprite>("Art Assets/Items and Currency/AbyssalOre"), 0, 0, 
                            "A rare metal given to you by the local Arcanist to give to the Illusion town.", "Does nothing.");

        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.AddToInventory(item);

        rewards.Add("Rune", "Illusionist A Rune");
        rewards.Add("Class Rune", "B Rune");

        consequenceMana = 500;

        questCompleted = false;
    }

    // return a string that contains the rewards of the quest
    public override string DisplayReward()
    {
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<string, string> kvp in rewards)
        {
            sb.Append(kvp.Value);
            sb.Append("\n");
        }

        return sb.ToString();
    }
}
