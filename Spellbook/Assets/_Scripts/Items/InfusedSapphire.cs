using UnityEngine;

public class InfusedSapphire : ItemObject
{
    public InfusedSapphire()
    {
        name = "Infused Sapphire";
        sprite = GameObject.Find("ItemContainer").transform.Find(name).GetComponent<SpriteRenderer>().sprite;
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

        GameObject locationContainer = GameObject.Find("LocationContainer");

        player.locationItemUsed = true;
        string[] locations = new string[9] { "Forest", "Swamp", "Crystal Mines", "Regulus", "Zandria", "Meridea", "Sarissa", "Parados", "Andromeda" };
        string randLocation = locations[Random.Range(0, 9)];

        Sprite locationSprite;
        switch(randLocation)
        {
            case "Forest":
                locationSprite = locationContainer.transform.Find("Forest").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ForestScene");
                break;
            case "Swamp":
                locationSprite = locationContainer.transform.Find("Swamp").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "SwampScene");
                break;
            case "Crystal Mines":
                locationSprite = locationContainer.transform.Find("Mines").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "MineScene");
                break;
            case "Regulus":
                locationSprite = locationContainer.transform.Find("Regulus").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to " + randLocation + "!", locationSprite, "AlchemyTownScene");
                break;
            case "Zandria":
                locationSprite = locationContainer.transform.Find("Zandria").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ArcaneTownScene");
                break;
            case "Meridea":
                locationSprite = locationContainer.transform.Find("Meridea").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ChronomancyTownScene");
                break;
            case "Sarissa":
                locationSprite = locationContainer.transform.Find("Sarissa").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "ElementalTownScene");
                break;
            case "Parados":
                locationSprite = locationContainer.transform.Find("Parados").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "IllusionTownScene");
                break;
            case "Andromeda":
                locationSprite = locationContainer.transform.Find("Andromeda").GetComponent<SpriteRenderer>().sprite;
                PanelHolder.instance.displayBoardScan("Infused Sapphire", "The Infused Sapphire teleported you to the " + randLocation + "!", locationSprite, "SummonerTownScene");
                break;
            default:
                break;
        }
    }
}
