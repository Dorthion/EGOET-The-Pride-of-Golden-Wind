﻿using EGOET.AdminConsole;
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

namespace EGOET.Scripts
{
    class GameManager
    {
        private static readonly int NumberOfNPC = 3;
        private static readonly int NumberOfMonsters = 2;
        private bool IsConvUp = false;

        internal bool IsFighting = false;

        internal Map Mapa;
        internal Player player;
        internal AdminConsoleCommands ACC;
        internal PlayerClass PlayerControler;
        internal Inventory inventory;
        internal Action action;
        internal Button button;
        internal Conversation conversation;
        internal Fight fight;
        internal WorldInformation worldinformation;
        internal NPC[] kip = new NPC[NumberOfNPC];
        internal Monster[] monsters = new Monster[NumberOfMonsters];

        public GameManager()
        {
            PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("C:\\Users\\Dorthion\\Desktop\\Admin.json"));

            var temp = JsonConvert.DeserializeObject<List<NPCclass>>(File.ReadAllText("..\\..\\Resources\\Profiles\\NPC.json"));
            for (int i=0; i<NumberOfNPC; i++)
                kip[i] = new NPC(temp.Find(x => x.Id == i+1).SpritePath, temp.Find(x => x.Id == i+1));

            var temp2 = JsonConvert.DeserializeObject<List<Monsterclass>>(File.ReadAllText("..\\..\\Resources\\Towns\\Monsters"+ PlayerControler.Hero.IdMiasta + ".json"));
            for (int i = 0; i < NumberOfMonsters; i++)
                monsters[i] = new Monster(temp2.Find(x => x.Id == i + 1).SpritePath, temp2.Find(x => x.Id == i + 1));

            Mapa = new Map();

            foreach (var mon in monsters)
                Mapa.SetMonsters(mon);

            inventory = new Inventory();
            ACC = new AdminConsoleCommands();
            player = new Player(Mapa.mapInfo);
            action = new Action(inventory.ActionIcon);

            //Nie potrzebne, wyczyść z pamięci
            inventory.ActionIcon = null;

            SpawnPointPlayer();
        }

        internal void UpdateScreen(RenderWindow _renderWindow)
        {
            //Always render:
            this.Mapa.Draw(_renderWindow, (int)(player.Xpos / 32), (int)(player.Ypos / 32));

            foreach(var npc in kip)
            {
                if(this.Mapa.playerView[(int)(npc.Xpos + 32) / 32, (int)(npc.Ypos + 32) / 32] == true && this.Mapa.playerView[(int)(npc.Xpos) / 32, (int)(npc.Ypos) / 32] == true)
                    npc.Draw(_renderWindow);
            }

            foreach (var mon in monsters)
            {
                mon.Draw(_renderWindow);
            }

            this.player.Draw(_renderWindow);

            if (player.IsActionNear == true)
                this.action.Draw(_renderWindow);

            //Status Conversation
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                if (this.player.IsActionWithChest == true)
                    CreateConversation();

                if (this.player.IsFighting == true)
                {
                    CreateBattleground();
                    fight.view.Zoom(0.5f);
                }
            }

            if (IsConvUp == true)
            {   
                this.conversation.Draw(_renderWindow);
                this.conversation.Update(this.player.Xpos, this.player.Ypos);
                if(this.conversation.DisableConv == true)
                {
                    this.conversation = null;
                    IsConvUp = false;
                }
            }

            if (IsFighting == true)
            {
                this.fight.Draw(_renderWindow);
                this.fight.Update(this.player.Xpos, this.player.Ypos);
                if (this.fight.DisableFight == true)
                {
                    this.conversation = null;
                    IsFighting = false;
                }
            }
        }

        internal void LoadTown(MainWindow mainWindow)
        {
            worldinformation = JsonConvert.DeserializeObject<WorldInformation>(File.ReadAllText("..\\..\\Resources\\Towns\\Town"+ PlayerControler.Hero.IdMiasta + ".json"));
            mainWindow.TownLabel.Content = worldinformation.NameWorld;
            player.Xpos = worldinformation.SpawnPointX * 32;
            player.Ypos = worldinformation.SpawnPointY * 32;
        }

        private void CreateConversation()
        {
            conversation = new Conversation(1, 29);
            IsConvUp = true;
        }

        private void CreateBattleground()
        {
            fight = new Fight();
            IsFighting = true;
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