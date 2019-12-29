using EGOET.AdminConsole;
using EGOET.Informations;
using EGOET.Scripts.Items.Inventory;
using EGOET.Maps;
using Newtonsoft.Json;
using SFML.Graphics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

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
        internal Inventory inventory;

        public enum Towns { Grudziadz, Torun, MaruszaMnieNieRusza};
        public GameManager()
        {
            Mapa = new Map();
            kip = new NPC();
            ACC = new AdminConsoleCommands();
            player = new Player();
            inventory = new Inventory();
            PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("C:\\Users\\Dorthion\\Desktop\\Admin.json"));
            SpawnPointPlayer();
        }

        public void UpdateScreen(RenderWindow _renderWindow)
        {
            //Draw Methods
            this.Mapa.Draw(_renderWindow, (int)(player.Xpos / 32), (int)(player.Ypos / 32), 10);
            this.kip.Draw(_renderWindow);
            this.player.Draw(_renderWindow);
        }

        private void SpawnPointPlayer()
        {
            player.Xpos = PlayerControler.Hero.LastPositionX;
            player.Ypos = PlayerControler.Hero.LastPositionY;
        }

        public void LoadInventory(MainWindow window)
        {
            int i = 1;
            string LoadItem;
            Button temp;
            ImageBrush brush;
            foreach (Item item in window.gM.PlayerControler.Items)
            {
                LoadItem = "i" + item.IdInv;
                temp = window.FindName(LoadItem) as Button;
                brush = inventory.TileMap[i] as ImageBrush;
                temp.Background = brush;
            }
        }
    }
}