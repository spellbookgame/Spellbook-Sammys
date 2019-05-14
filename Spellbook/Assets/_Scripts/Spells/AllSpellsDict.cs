using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSpellsDict
{
    public static readonly Dictionary<string, Spell> AllSpells = new Dictionary<string, Spell> {

        //Alchemist
        { "Brew - Potion of Luck", new PotionofLuck() },
        { "Brew - Potion of Blessing", new PotionofBlessing() },
        { "Brew - Distilled Potion", new DistilledPotion() },
        { "Brew - Charming Negotiator", new CharmingNegotiator() },
        { "Brew - Collector's Drink", new CollectorsDrink() },
        { "Brew - Toxic Potion", new ToxicPotion() },
        { "Brew - Crystal Scent", new CrystalScent() },

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
        { "Call of the Sun - Leon's Shining", new CoSLeon() },
        { "Call of the Wild - Raven's Song", new Ravenssong() },
        { "Call of the Wild - Bear's Fury", new Bearsfury() },
        { "Call of the Moon - Umbra's Eclipse", new CoMUmbra() },
        { "Call of the Dead - Corpse Taker", new CoDCorpse() },
        { "Call of the Wild - Skeletons", new Skeletons() },
        { "Call of the Stars - Rigel's Ascension", new CoSRigel() },

        //Black Magic
        { "Agenda", new Agenda() },
        { "Dark Revelation", new DarkRevelation() },
        { "Fortune", new Fortune() },
        { "Hollow Creation", new HollowCreation() },
        { "Special Warp", new SpacialWarp() },
    };
}
