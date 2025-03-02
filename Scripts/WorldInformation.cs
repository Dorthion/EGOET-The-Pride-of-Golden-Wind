﻿using EGOET.Scripts;
using System.Collections.Generic;
namespace EGOET.Informations
{
    /// <summary>
    ///     World information zawiera podstawowe informacje
    ///     o danym otoczeniu
    ///     Zawiera obiekty: Nazwa, Coordy XY Spawna
    ///     Identyfikatory: Trigger, Spawn, Door
    /// </summary>

    [System.Serializable]
    public class WorldInformation
    {
        public string NameWorld { get; set; }
        public int SpawnPointX { get; set; }
        public int SpawnPointY { get; set; }
    }

    /// <summary>
    ///     Zbiór informacji o danych użytkownika,
    ///     Zawiera informacje o loginie, haśle(?), bohaterach.
    /// </summary>

    [System.Serializable]
    public class PlayerClass
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public HeroClass Hero { get; set; } = new HeroClass();
        public List<Item> Items { get; set; } = new List<Item>();
    }

    /// <summary>
    ///     Klasa o danym bohaterze
    ///     Zbiór informacji o danych użytkownika
    /// </summary>

    [System.Serializable]
    public class HeroClass
    {
        //Podstawowe informacje
        public string Name { get; set; } = "";
        public int Poziom { get; set; } = 1;

        //Statystyki Bohatera
        public int ExpNow { get; set; } = 0;
        public int ExpToNextLvl { get; set; } = 100;
        public int Hp { get; set; } = 100;
        public int HpMax { get; set; } = 100;

        //Statystyki Umiejętności
        public int Sila { get; set; } = 5;
        public int Magia { get; set; } = 5;
        public int Obrona { get; set; } = 10;

        //Dodatkowe Statystyki (Reszta)
        public string SpritePath { get; set; } = "..\\..\\Sprites\\Ruda_Dlugie.png";
        public int PunktyUmiejetnosci { get; set; } = 0;
        public int IdMiasta { get; set; } = 0;
        public int Money { get; set; } = 10;
    }

    /// <summary>
    /// Dane o przedmiocie
    /// </summary>
    [System.Serializable]
    public class Item
    {
        public int Type { get; set; } = 0;
        public int ItemName { get; set; } = 0;
        public int Rare { get; set; } = 0;
        public int Cost { get; set; } = 0;
        public int IdInv { get; set; } = 0;
        public int IdSprite { get; set; } = 14;
    }

    /// <summary>
    /// Zbiór informacji o NPC
    /// </summary>
    [System.Serializable]
    public class NPCclass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpritePath { get; set; }
        public int QuestNumber { get; set; }
        public int CurrentMovement { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public List<Waypoint> Waypoints { get; set; }
    }

    /// <summary>
    /// Dane o przeciwniku
    /// </summary>
    [System.Serializable]
    public class MonsterClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Sila { get; set; }
        public int Obrona { get; set; }
        public int Hp { get; set; }
        public string SpritePath { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}