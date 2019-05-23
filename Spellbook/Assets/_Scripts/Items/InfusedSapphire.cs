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
        SoundManager.instance.PlaySingle(SoundManager.infusedSapphire);
        player.RemoveFromInventory(this);

        player.locationItemUsed = true;
        string[] locations = new string[9] { "Forest", "Swamp", "Crystal Mines", "Regulus", "Zandria", "Meridea", "Sarissa", "Parados", "Andromeda" };
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
            case "Regulus":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_alchemist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to " + randLocation + "!", locationSprite, "AlchemyTownScene");
                break;
            case "Zandria":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_arcanist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ArcaneTownScene");
                break;
            case "Meridea":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_chronomancer");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ChronomancyTownScene");
                break;
            case "Sarissa":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_elementalist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ElementalTownScene");
                break;
            case "Parados":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_illusionist");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "IllusionTownScene");
                break;
            case "Andromeda":
                locationSprite = Resources.Load<Sprite>("Art Assets/Backgrounds/town_summoner");
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "SummonerTownScene");
                break;
            default:
                break;
        }
    }
}
