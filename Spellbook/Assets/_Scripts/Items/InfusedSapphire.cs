using UnityEngine;

public class InfusedSapphire : ItemObject
{
    public InfusedSapphire()
    {
        name = "Infused Sapphire";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Infused Sapphire");
        tier = 2;
        buyPrice = 1400;
        sellPrice = 420;
        flavorDescription = "This sapphire is embued with pure arcane energy.";
        mechanicsDescription = "When shattered, teleports the user to a random location.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);

        player.locationItemUsed = true;
        string[] locations = new string[9] { "Forest", "Swamp", "Crystal Mines", "Alchemist Town", "Arcanist Town", "Chronomancer Town", "Elementalist Town", "Illusionist Town", "Summoner Town" };
        string randLocation = locations[Random.Range(0, 9)];

        Sprite locationSprite;
        switch(randLocation)
        {
            case "Forest":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/Forest");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ForestScene");
                break;
            case "Swamp":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/Swamp");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "SwampScene");
                break;
            case "Crystal Mines":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/Mine");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "MineScene");
                break;
            case "Alchemist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_alchemist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "AlchemyTownScene");
                break;
            case "Arcanist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_arcanist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ArcaneTownScene");
                break;
            case "Chronomancer Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_chronomancer");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ChronomancyTownScene");
                break;
            case "Elementalist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_elementalist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ElementalTownScene");
                break;
            case "Illusionist Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_illusionist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "IllusionTownScene");
                break;
            case "Summoner Town":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_summoner");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "SummonerTownScene");
                break;
            default:
                break;
        }
    }
}
