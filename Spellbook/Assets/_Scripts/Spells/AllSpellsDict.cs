using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSpellsDict
{
    public static readonly Dictionary<string, Spell> AllSpells = new Dictionary<string, Spell> {

        //Alchemist
        { "Potion of Luck", new PotionofLuck() },
        { "Potion of Blessing", new PotionofBlessing() },
        { "Distilled Potion", new DistilledPotion() },
        { "Potion of Charm", new CharmingNegotiator() },
        { "Collector's Drink", new CollectorsDrink() },
        { "Toxic Potion", new ToxicPotion() },
        { "Crystal Scent Potion", new CrystalScent() },

        //Arcanist
        { "Transcribe", new Transcribe() },
        { "Marcellas Blessing", new MarcellasBlessing() },
        { "Archive", new Archive() },
        { "Combined Knowledge", new CombinedKnowledge() },
        { "Runic Darts", new RunicDarts() },
        { "Rune Conversion", new RuneConversion() },
        { "Arcana Harvest", new ArcanaHarvest() },

        //Elementalist
        { "Natural Disaster", new NaturalDisaster() },
        { "Tailwind", new Tailwind() },
        { "Eye of the Storm", new EyeOfTheStorm() },
        { "Terraforming Earthquake", new TerraformingEarthquake() },
        { "Frozen Terrain", new FrozenTerrain() },
        { "Fireball", new Fireball() },
        { "Growth", new Growth() },

        //Chronomancer
        { "Delay Time", new DelayTime() },
        { "Deja Vu", new DejaVu() },
        { "Manipulate", new Manipulate() },
        { "Forecast", new Forecast() },
        { "Reverse Wounds", new ReverseWounds() },
        { "Echo", new Echo() },
        { "Chronoblast", new Chronoblast() },

        //Illusionist/Trickster
        { "Playwright", new Playwright() },
        { "Cheat Out", new CheatOut() },
        { "Playback", new Playback() },
        { "Catastrophe", new Catastrophe() },
        { "Catharsis", new Catharsis() },
        { "Tragedy", new Tragedy() },
        { "Allegro", new Allegro() },

        //Summoner
        { "Leon's Shining", new CoSLeon() },
        { "Raven's Song", new Ravenssong() },
        { "Bear's Fury", new Bearsfury() },
        { "Umbra's Eclipse", new CoMUmbra() },
        { "Corpse Taker", new CoDCorpse() },
        { "Skeletons", new Skeletons() },
        { "Rigel's Ascension", new CoSRigel() },

        //Black Magic
        { "Agenda", new Agenda() },
        { "Dark Revelation", new DarkRevelation() },
        { "Fortune", new Fortune() },
        { "Hollow Creation", new HollowCreation() },
        { "Special Warp", new SpacialWarp() },
    };
}
