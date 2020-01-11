using EGOET.AdminConsole;
using EGOET.Informations;
using EGOET.Scripts.Conversations;
using EGOET.Scripts.Items.Inventory;
using EGOET.Maps;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace EGOET.Scripts
{
    class GameManager
    {
        private static readonly int NumberOfNPC = 2;
        private bool ConvUp = false;

        internal Map Mapa;
        internal Player player;
        internal AdminConsoleCommands ACC;
        internal PlayerClass PlayerControler;
        internal Inventory inventory;
        internal Action action;
        internal Button button;
        internal Conversation conversation;
        internal NPC[] kip = new NPC[NumberOfNPC];

        public enum Towns { Grudziadz, Torun, PatusowoPomorskie};
        public GameManager()
        {
            var temp = JsonConvert.DeserializeObject<List<NPCclass>>(File.ReadAllText("..\\..\\Resources\\Profiles\\NPC.json"));
            for (int i=0; i<NumberOfNPC; i++)
                kip[i] = new NPC(temp.Find(x => x.Id == i+1).SpritePath, temp.Find(x => x.Id == i+1));
            Mapa = new Map();
            inventory = new Inventory();
            ACC = new AdminConsoleCommands();
            player = new Player(Mapa.mapInfo);
            action = new Action(inventory.ActionIcon);

            //Nie potrzebne, wyczyść z pamięci
            inventory.ActionIcon = null;
            inventory.UnderButton = null;

            PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("C:\\Users\\Dorthion\\Desktop\\Admin.json"));
            SpawnPointPlayer();
        }

        internal void UpdateScreen(RenderWindow _renderWindow)
        {
            //Always render:
            this.Mapa.Draw(_renderWindow, (int)(player.Xpos / 32), (int)(player.Ypos / 32));
            this.player.Draw(_renderWindow);

            //Dont render if player is out of view
            //if (this.Mapa.playerView[(int)(this.kip.Xpos + 32) / 32, (int)(this.kip.Ypos + 32) / 32] == true)
            //    this.kip.Draw(_renderWindow);

            foreach(var hehe in kip)
            {
                if(this.Mapa.playerView[(int)(hehe.Xpos + 32) / 32, (int)(hehe.Ypos + 32) / 32] == true)
                    hehe.Draw(_renderWindow);
            }

            if (player.ShowActionIcon == true)
                this.action.Draw(_renderWindow);

            //Status Conversation
            if (Keyboard.IsKeyPressed(Keyboard.Key.E) && this.player.ShowActionIcon == true)
            {
                CreateConversation();
            }

            if (ConvUp == true)
            {   
                this.conversation.Draw(_renderWindow);
                this.conversation.Update(this.player.Xpos, this.player.Ypos);
                if(this.conversation.DisableConv == true)
                {
                    this.conversation = null;
                    ConvUp = false;
                }
            }

        }

        private void CreateConversation()
        {
            conversation = new Conversation(1, 29);
            ConvUp = true;
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
            using (StreamWriter file = File.CreateText("C:\\Users\\Dorthion\\Desktop\\Admin3.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, PlayerControler);
            }
        }
    }
}