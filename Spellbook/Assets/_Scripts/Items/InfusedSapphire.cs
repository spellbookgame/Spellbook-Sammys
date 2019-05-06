using UnityEngine;

public class InfusedSapphire : ItemObject
{
    public InfusedSapphire()
    {
        name = "Infused Sapphire";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/InfusedSapphire");
        tier = 2;
        buyPrice = 1400;
        sellPrice = 420;
        flavorDescription = "This sapphire is embued with pure arcane energy.";
        mechanicsDescription = "When shattered, teleports the user to a random location.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);
        string[] locations = new string[9] { "Forest", "Swamp", "Crystal Mines", "Alchemist Town", "Arcanist Town", "Chronomancer Town", "Elementalist Town", "Illusionist Town", "Summoner Town" };
        string randLocation = locations[Random.Range(0, 9)];

        Sprite locationSprite;
        switch(randLocation)
        {
            case "Forest":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/Forest");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Swamp":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/Swamp");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Crystal Mines":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/Mines");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Alchemist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_alchemist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Arcanist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_arcanist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Chronomancer Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_chronomancer");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Elementalist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_elementalist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            case "Illusionist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_summoner");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite);
                break;
            default:
                break;
        }
    }
}
