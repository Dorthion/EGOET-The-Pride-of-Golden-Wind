using EGOET.AdminConsole;
using EGOET.Informations;
using EGOET.Maps;
using Newtonsoft.Json;
using SFML.Graphics;
using System.IO;

namespace EGOET.Scripts
{
    class GameManager
    {
        internal int actualCharacter = 0;
        internal Map Mapa;
        internal NPC kip;
        internal Player player;
        internal AdminConsoleCommands ACC;
        internal PlayerClass PlayerControler;
        //internal Sprite JakisSprite;
        public enum Towns { Grudziadz, Torun};
        public GameManager()
        {
            Mapa = new Map();
            kip = new NPC();
            ACC = new AdminConsoleCommands();
            player = new Player();
            PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("C:\\Users\\Dorthion\\Desktop\\Admin.json"));
        }

        public void UpdateScreen(RenderWindow _renderWindow)
        {
            //Draw Methods
            this.Mapa.Draw(_renderWindow, (int)(player.Xpos / 32), (int)(player.Ypos / 32), 10);
            this.kip.Draw(_renderWindow);
            this.player.Draw(_renderWindow);
        }
    }
}
