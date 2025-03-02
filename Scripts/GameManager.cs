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
using System;
using SFML.System;

namespace EGOET.Scripts
{
    public enum ItemTypes
    {
        Brak,
        Helm,
        Naszyjnik,
        Rekawiczki,
        Pierscien,
        Pancerz,
        Tarcza,
        Miecz,
        Buty
    }

    public enum ItemSpec
    {
        Brak,
        Zniszczony,
        Utwardzany,
        Twardy,
        Magiczny,
        Ukruszony,
        Zaczarowany
    }

    public enum ItemRare
    {
        Brak,
        Zwyczajny,
        Rzadki,
        Epicki,
        Legendarny
    }

    class GameManager
    {
        private static readonly int NumberOfNPC = 3;
        private static readonly int NumberOfMonsters = 2;

        internal bool IsConvUp = false;
        internal bool IsFighting = false;
        internal bool IsPaused = false;
        internal bool NeedToUpdate = false;

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

        internal Clock deathClock;
        private static Font font;
        private static Text deathText;
        private static Text deathText2;
        private static RectangleShape deathRectangle;


        public GameManager()
        {
            //Delete?
            //PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("..\\..\\Resources\\Profiles\\"+PlayerControler.Username+".json"));
            //In dev time:
            PlayerControler = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText("..\\..\\Resources\\Profiles\\Admin.json"));

            var temp = JsonConvert.DeserializeObject<List<NPCclass>>(File.ReadAllText("..\\..\\Resources\\Towns\\NPC" + PlayerControler.Hero.IdMiasta + ".json"));
            for (int i=0; i<NumberOfNPC; i++)
                kip[i] = new NPC(temp.Find(x => x.Id == i+1).SpritePath, temp.Find(x => x.Id == i+1));

            var temp2 = JsonConvert.DeserializeObject<List<MonsterClass>>(File.ReadAllText("..\\..\\Resources\\Towns\\Monsters" + PlayerControler.Hero.IdMiasta + ".json"));
            for (int i = 0; i < NumberOfMonsters; i++)
                monsters[i] = new Monster(temp2.Find(x => x.Id == i + 1).SpritePath, temp2.Find(x => x.Id == i + 1));

            Mapa = new Map();

            foreach (var npc in kip)
                Mapa.SetNPC(npc);

            foreach (var mon in monsters)
                Mapa.SetMonsters(mon);

            inventory = new Inventory();
            ACC = new AdminConsoleCommands();
            player = new Player(Mapa.mapInfo, PlayerControler.Hero.SpritePath);
            action = new Action(inventory.ActionIcon);

            //Nie potrzebne, wyczyść z pamięci
            inventory.ActionIcon = null;
        }

        internal void UpdateScreen(RenderWindow _renderWindow)
        {
            //Always render:
            if(this.IsFighting == false)
            {
                this.Mapa.Draw(_renderWindow, (int)(player.Xpos / 32), (int)(player.Ypos / 32));

                foreach(var npc in kip)
                {
                    if(this.Mapa.playerView[(int)(npc.Xpos + 32) / 32, (int)(npc.Ypos + 32) / 32] == true && this.Mapa.playerView[(int)(npc.Xpos) / 32, (int)(npc.Ypos) / 32] == true)
                        npc.Draw(_renderWindow);
                }

                foreach (var mon in monsters)
                {
                    if (this.Mapa.playerView[(int)(mon.Xpos + 32) / 32, (int)(mon.Ypos + 32) / 32] == true && this.Mapa.playerView[(int)(mon.Xpos) / 32, (int)(mon.Ypos) / 32] == true && mon.IsDead == false)
                        mon.Draw(_renderWindow);
                }

                this.player.Draw(_renderWindow);

                if (player.IsActionNear == true)
                    this.action.Draw(_renderWindow);

                //Status Conversation
                if (Keyboard.IsKeyPressed(Keyboard.Key.E) && !IsPaused)
                {
                    if (this.player.IsActionWithNPC == true)
                    {
                        CreateConversation();
                        IsPaused = true;
                    }

                    if (this.player.IsFighting == true)
                    {
                        CreateBattleground();
                        fight.view.Zoom(0.5f);
                        this.IsPaused = true;
                    }
                }
            }

            if (this.IsConvUp == true)
            {   
                this.conversation.Draw(_renderWindow);
                this.conversation.Update(this.player.Xpos, this.player.Ypos);

                if(this.conversation.DisableConv == true)
                {
                    this.conversation = null;
                    this.IsConvUp = false;
                    this.IsPaused = false;
                }
            }

            if (this.IsFighting == true)
            {
                this.fight.Draw(_renderWindow);
                this.fight.Update(_renderWindow);
                

                if (this.fight.DisableFight == true)
                {
                    if (this.fight.player.Hero.Hp < 0)
                    {
                        this.player.IsDead = true;
                        DeathScreen();
                    }
                    this.PlayerControler.Hero.Hp = this.fight.player.Hero.Hp;
                    this.PlayerControler.Hero.Money = this.fight.player.Hero.Money;
                    this.PlayerControler.Hero.ExpNow = this.fight.player.Hero.ExpNow;
                    this.PlayerControler.Hero.ExpToNextLvl = this.fight.player.Hero.ExpToNextLvl;
                    this.PlayerControler.Hero.PunktyUmiejetnosci = this.fight.player.Hero.PunktyUmiejetnosci;
                    this.fight = null;
                    this.IsFighting = false;
                    this.IsPaused = false;
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
            GC.Collect();
            conversation = new Conversation(1, 29);
            this.IsConvUp = true;
        }

        private void DeathScreen()
        {
            deathRectangle = new RectangleShape()
            {
                Size = new Vector2f(2000, 1500),
                Position = new Vector2f(player.Xpos, player.Ypos),
                FillColor = SFML.Graphics.Color.Black
            };

            deathClock = new Clock();
            font = new Font(@"..\..\Resources\Fonts\FantaisieArtistique.ttf");
            deathText = new Text("Umarles!", font);
            deathText.Position = new Vector2f(player.Xpos + 500.0f, player.Ypos + 200.0f);
        }

        internal void UpdateDeathScreen(RenderWindow window)
        {
            float deltatime = deathClock.ElapsedTime.AsSeconds();
            float temp = 3.0f - deltatime;

            deathText2 = new Text("Powstaniesz za: " + temp + "s", font);
            deathText2.Position = new Vector2f(player.Xpos + 400.0f, player.Ypos + 400.0f);
            PlayerControler.Hero.Hp = 1;

            window.Draw(drawable: deathRectangle);
            window.Draw(drawable: deathText);
            window.Draw(drawable: deathText2);
        }

        private void CreateBattleground()
        {
            GC.Collect();
            int i = 0;
            foreach(var mor in monsters)
            {
                if (mor.Xpos == player.XPosAction && mor.Ypos == player.YPosAction)
                    return;
                i++;
            }

            fight = new Fight(PlayerControler, monsters[i-1].monsterclass);
            this.IsFighting = true;
        }

        internal void SpawnPointPlayer()
        {
            player.Xpos = worldinformation.SpawnPointX * 32;
            player.Ypos = worldinformation.SpawnPointY * 32;
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
                    "Name: " + (ItemSpec)item.Type + " " +
                    (ItemTypes)item.ItemName +
                    "\nRare:" + (ItemRare)item.Rare +
                    "\nCost: " + item.Cost;
                temp.Background = brush;
                temp.ToolTip = tooltip;
            }
            window.SelectedButton.Source = inventory.UnderButton;
        }

        public void SaveEq()
        {
            using (StreamWriter file = File.CreateText("..\\..\\Resources\\Profiles\\"+ PlayerControler.Username +".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, PlayerControler);
            }
        }
    }
}