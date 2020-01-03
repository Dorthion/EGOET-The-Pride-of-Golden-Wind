﻿using EGOET.AdminConsole;
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
        internal Map Mapa;
        internal NPC kip;
        internal Player player;
        internal AdminConsoleCommands ACC;
        internal PlayerClass PlayerControler;
        internal Inventory inventory;
        internal Button button;

        public enum Towns { Grudziadz, Torun, PatusowoPomorskie};
        public GameManager()
        {
            Mapa = new Map();
            kip = new NPC();
            inventory = new Inventory();
            ACC = new AdminConsoleCommands();
            player = new Player(Mapa.mapInfo);
            PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("C:\\Users\\Dorthion\\Desktop\\Admin.json"));
            SpawnPointPlayer();
        }

        public void UpdateScreen(RenderWindow _renderWindow)
        {
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
            string LoadItem;
            Button temp;
            ImageBrush brush;
            ToolTip tooltip;
            foreach (Item item in window.gM.PlayerControler.Items)
            {
                tooltip = new ToolTip();
                LoadItem = "i" + item.IdInv;
                temp = window.FindName(LoadItem) as Button;
                brush = inventory.TileMap[item.IdSprite] as ImageBrush;
                tooltip.Content =
                    "Name: Wymysl" +
                    "\nRare:" + item.Rare +
                    "\nType: " + item.Type +
                    "\nIdInv: " + item.IdInv;
                temp.Background = brush;
                temp.ToolTip = tooltip;
            }
            window.SelectedButton.Source = inventory.UnderButton;
        }

        public void SaveEq()
        {
            //File.WriteAllText("C:\\Users\\Dorthion\\Desktop\\Admin2.json", JsonConvert.SerializeObject(PlayerControler));
            using (StreamWriter file = File.CreateText("C:\\Users\\Dorthion\\Desktop\\Admin3.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, PlayerControler);
            }
        }
    }
}