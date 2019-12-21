namespace EGOET.Informations
{
    /// <summary>
    ///     World information zawiera podstawowe informacje o danym otoczeniu
    ///     Zawiera obiekty: Nazwa, Coordy XY Spawna
    ///     Identyfikatory: Trigger, Spawn, Door
    /// </summary>

    [System.Serializable]
    class WorldInformation
    {
        internal int NameWorld { get; set; }
        internal int SpawnPointX { get; set; }
        internal int SpawnPointY { get; set; }
        internal int TriggerID { get; set; }
        internal int SpawnID { get; set; }
        internal int DoorID { get; set; }
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
        public HeroesClass[] Heroes { get; set; }
    }

    /// <summary>
    ///     Klasa o danum bohaterze
    ///     Zbiór informacji o aktualnym ID, nazwie, 
    ///     kasie, kryształach danego użytkownika
    /// </summary>

    [System.Serializable]
    public class HeroesClass
    {
        //Podstawowe informacje
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Poziom { get; set; } = 0;

        //Statystyki Bohatera
        public int ExpNow { get; set; } = 0;
        public int ExpToNextLvl { get; set; } = 0;
        public int Hp { get; set; } = 0;
        public int HpMax { get; set; } = 0;
        public int Mp { get; set; } = 0;
        public int MpMax { get; set; } = 0;

        //Statystyki Umiejętności
        public int Sila { get; set; } = 0;
        public int Magia { get; set; } = 0;
        public int Obrona { get; set; } = 0;

        //Dodatkowe Statystyki (Reszta)
        public int PunktyUmiejetnosci { get; set; } = 0;
        public int IdMiasta { get; set; } = 0;
        public int Money { get; set; } = 0;
        public int Crystals { get; set; } = 0;
    }
}