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
        internal int NameWorld { get; set; }
        internal int SpawnPointX { get; set; }
        internal int SpawnPointY { get; set; }
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
        public HeroClass Hero { get; set; }
        public List<Item> Items { get; set; }
    }

    /// <summary>
    ///     Klasa o danum bohaterze
    ///     Zbiór informacji o aktualnym ID, nazwie, 
    ///     kasie, kryształach danego użytkownika
    /// </summary>

    [System.Serializable]
    public class HeroClass
    {
        //Podstawowe informacje
        public string Name { get; set; } = "";
        public int Poziom { get; set; } = 0;

        //Statystyki Bohatera
        public int ExpNow { get; set; } = 0;
        public int ExpToNextLvl { get; set; } = 0;
        public int Hp { get; set; } = 0;
        public int HpMax { get; set; } = 0;

        //Statystyki Umiejętności
        public int Sila { get; set; } = 0;
        public int Magia { get; set; } = 0;
        public int Obrona { get; set; } = 0;

        //Dodatkowe Statystyki (Reszta)
        public int PunktyUmiejetnosci { get; set; } = 0;
        public int IdMiasta { get; set; } = 0;
        public int Money { get; set; } = 0;
        public int LastPositionX { get; set; } = 0;
        public int LastPositionY { get; set; } = 0;
    }

    [System.Serializable]
    public class Item
    {
        public string Type { get; set; }
        public int Rare { get; set; }
        public int IdInv { get; set; }
        public int IdSprite { get; set; }
    }

    [System.Serializable]
    public class NPCclass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpritePath { get; set; }
        public int QuestNumber { get; set; }
        public int CurrentMovement { get; set; }
    }
}