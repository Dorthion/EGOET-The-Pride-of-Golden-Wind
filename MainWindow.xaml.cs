﻿using EGOET.Informations;
using EGOET.Options;
using EGOET.Scripts;
using EGOET.Skills;
using EGOET.Stats;
using MahApps.Metro.Controls;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EGOET
{
    public partial class MainWindow : MetroWindow
    {
        internal GameManager gM;
        internal RenderWindow _renderWindow = null;

        private bool dynamiccamera;
        private bool gamerunning = true;

        private int fpspersecond;
        private int counter = 0;
        private int[] iloscfps = new int[10];

        private readonly Clock clock;
        private readonly Clock clocklog;
        private readonly View view = new View(new Vector2f(0, 0), new Vector2f(1718, 949));
        private readonly RectangleShape ClearRect = new RectangleShape()
        {
            Position = new Vector2f(0, 0),
            Size = new Vector2f(2000,1000),
            FillColor = SFML.Graphics.Color.Black
        };

        #region OnLoad
        public MainWindow()
        {
            Directory.CreateDirectory("Logs");
            using (StreamWriter file = File.AppendText(@"Logs\LogStartUp.txt"))
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                file.WriteLine(DateTime.Now + ": [Loading Game]");
                InitializeComponent();
                file.WriteLine(DateTime.Now + ": Loading Main Window... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                Loaded += OnLoaded;

                ContextSettings context = new ContextSettings()
                {
                    AttributeFlags = ContextSettings.Attribute.Default,
                    MajorVersion = 4,
                    MinorVersion = 6,
                    DepthBits = 0,
                    StencilBits = 0,
                    AntialiasingLevel = 0
                };

                file.WriteLine(DateTime.Now + ": Loading OnLoaded... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                this._renderWindow = new RenderWindow(this.DrawSurface.Handle, context);
                this._renderWindow.SetMouseCursorVisible(false);

                file.WriteLine(DateTime.Now + ": Loading SFML Render Window... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                DrawSurface.Cursor = new System.Windows.Forms.Cursor("..\\..\\Sprites\\Cursor3.cur");

                file.WriteLine(DateTime.Now + ": Set Custom Cursor... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                CompositionTargetEx.Rendering += Timer_Tick;
                CompositionTargetEx.Rendering += TestWydajnosci;

                file.WriteLine(DateTime.Now + ": Set Rendering Components... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                gM = new GameManager();
                UpdateStatistics();
                clock = new Clock();
                clocklog = new Clock();
                
                file.WriteLine(DateTime.Now + ": Loading Other Components... (" + stopWatch.ElapsedMilliseconds + " ms)");
                file.WriteLine(DateTime.Now + ": [Done Loading]\n");
                stopWatch.Stop();
            };

            using (StreamWriter file = File.AppendText(@"Logs\Log SFML " + DateTime.Today.ToShortDateString() + ".txt"))
            {
                file.WriteLine(DateTime.Now + "[Game Started]");
            };
        }
        public MainWindow(PlayerClass _player)
        {
            Directory.CreateDirectory("Logs");
            using (StreamWriter file = File.AppendText(@"Logs\LogStartUp.txt"))
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                file.WriteLine(DateTime.Now + ": [Loading Game]");
                InitializeComponent();
                file.WriteLine(DateTime.Now + ": Loading Main Window... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                Loaded += OnLoaded;

                ContextSettings context = new ContextSettings()
                {
                    AttributeFlags = ContextSettings.Attribute.Default,
                    MajorVersion = 4,
                    MinorVersion = 6,
                    DepthBits = 0,
                    StencilBits = 0,
                    AntialiasingLevel = 0
                };

                file.WriteLine(DateTime.Now + ": Loading OnLoaded... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                this._renderWindow = new RenderWindow(this.DrawSurface.Handle, context);
                this._renderWindow.SetMouseCursorVisible(false);

                file.WriteLine(DateTime.Now + ": Loading SFML Render Window... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                DrawSurface.Cursor = new System.Windows.Forms.Cursor("..\\..\\Sprites\\Cursor3.cur");

                file.WriteLine(DateTime.Now + ": Set Custom Cursor... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                CompositionTargetEx.Rendering += Timer_Tick;
                CompositionTargetEx.Rendering += TestWydajnosci;

                file.WriteLine(DateTime.Now + ": Set Rendering Components... (" + stopWatch.ElapsedMilliseconds + " ms)");
                stopWatch.Restart();
                gM = new GameManager();
                gM.PlayerControler = _player;
                UpdateStatistics();
                clock = new Clock();
                clocklog = new Clock();

                file.WriteLine(DateTime.Now + ": Loading Other Components... (" + stopWatch.ElapsedMilliseconds + " ms)");
                file.WriteLine(DateTime.Now + ": [Done Loading]\n");
                stopWatch.Stop();
            };

            using (StreamWriter file = File.AppendText(@"Logs\Log SFML " + DateTime.Today.ToShortDateString() + ".txt"))
            {
                file.WriteLine(DateTime.Now + "[Game Started]");
            };
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            ShowMaxRestoreButton = false;
            ShowMinButton = false;
            
            Loaded -= OnLoaded;

            gM.LoadTown(this);
            gM.LoadInventory(this);
            UpdateRenderScreenSettings();

            NicknameLabel.Content = gM.PlayerControler.Hero.Name.ToString() + " (" + gM.PlayerControler.Hero.Poziom.ToString() + "lvl)";
            MoneyLabel.Content = gM.PlayerControler.Hero.Money.ToString();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._renderWindow.SetFramerateLimit(0);
            this._renderWindow.SetVerticalSyncEnabled(false);
            this._renderWindow.DispatchEvents();

            this.view.Zoom(0.7f);
            foreach(var t in gM.kip)
            {
                switch (t.npc.CurrentMovement)
                {
                    case 0:
                        t.CurrentState = CharacterState.None;
                        break;
                    case 1:
                        t.CurrentState = CharacterState.MovingUp;
                        break;
                    case 2:
                        t.CurrentState = CharacterState.MovingLeft;
                        break;
                    case 3:
                        t.CurrentState = CharacterState.MovingDown;
                        break;
                    case 4:
                        t.CurrentState = CharacterState.MovingRight;
                        break;
                }
            }
        }
        #endregion

        private void Timer_Tick(object sender, EventArgs e)
        {
            float deltatime = clock.Restart().AsSeconds();

            if (gM.player.IsDead)
            {
                if(3.0f - this.gM.deathClock.ElapsedTime.AsSeconds() > 0.0f)
                {
                    this.gM.UpdateDeathScreen(_renderWindow);
                    this._renderWindow.Display();
                    return;
                } else
                {
                    this.gM.player.IsDead = false;
                    this.gM.SpawnPointPlayer();
                }
                
            }

            //Center View
            if (!gM.IsFighting)
            {
                this._renderWindow.SetView(view);
                if (dynamiccamera)
                    DynamicCamera(deltatime);
                else StaticCamera();
            }

            //Clear Screen
            this.ClearRect.Position = new Vector2f(gM.player.Xpos - 1000.0f, gM.player.Ypos - 500.0f);
            this._renderWindow.Draw(ClearRect);

            if (gamerunning)
            {
                if(!this.gM.IsPaused)
                    this.gM.player.Update(deltatime);
                this.gM.action.Update(this.gM.player.Xpos, this.gM.player.Ypos);
                this.gM.UpdateScreen(_renderWindow);
                foreach (var t in gM.kip)
                    t.Update(deltatime);

                foreach (var m in gM.monsters)
                    m.Update(deltatime);
            }

            //Display
            this._renderWindow.Display();
        }


        #region PrivateMethods
        private void TestWydajnosci(object sender, EventArgs e)
        {
            fpspersecond++;

            if (clocklog.ElapsedTime.AsSeconds() > counter + 1)
            {
                iloscfps[counter] = fpspersecond;
                fpspersecond = 0;
                counter++;
            }

            if (clocklog.ElapsedTime.AsSeconds() > 10)
            {
                using (StreamWriter file = File.AppendText(@"Logs\Log SFML " + DateTime.Today.ToShortDateString() + ".txt"))
                    {
                    int temp = 0;
                    for(int i = 0; i<10; i++)
                    {
                        temp += iloscfps[i];
                    }
                    temp /= 10;
                    file.WriteLine(DateTime.Now + ": FPS: " + temp + " fps; Render Distance: " + gM.Mapa.GlebokoscOdswiezania);
                };
                fpspersecond = 0;
                counter = 0;
                clocklog.Restart();
            }
        }

        internal void UpdateRenderScreenSettings()
        {
            dynamiccamera = Properties.Settings.Default.DynamicCamera;
            gM.Mapa.GlebokoscOdswiezania = Properties.Settings.Default.RenderDistance;

            gamerunning = true;
        }

        internal void UpdateStats(PlayerClass playerClass)
        {
            this.gM.PlayerControler.Hero.Hp = playerClass.Hero.Hp;
            this.gM.PlayerControler.Hero.Sila = playerClass.Hero.Sila;
            this.gM.PlayerControler.Hero.Magia = playerClass.Hero.Magia;
            this.gM.PlayerControler.Hero.Obrona = playerClass.Hero.Obrona;
            this.gM.PlayerControler.Hero.PunktyUmiejetnosci = playerClass.Hero.PunktyUmiejetnosci;

            UpdateStatistics();
        }

        private void DynamicCamera(float dt)
        {
            Vector2f movement = new Vector2f(gM.player.Xpos - view.Center.X, gM.player.Ypos - view.Center.Y);
            this.view.Move(movement * dt * 2);
        }

        private void StaticCamera()
        {
            this.view.Center = new Vector2f(gM.player.Xpos, gM.player.Ypos);
        }

        private void UpdateStatistics()
        {
            this.Strength.Content = (this.gM.PlayerControler.Hero.Sila + this.gM.PlayerControler.Hero.Magia).ToString();
            this.Defense.Content = this.gM.PlayerControler.Hero.Obrona;
            this.MaxHP.Content = this.gM.PlayerControler.Hero.HpMax;

            this.HpCounter.Height = (float)((float)this.gM.PlayerControler.Hero.Hp / (float)this.gM.PlayerControler.Hero.HpMax) * 150;
            this.ExpCounter.Height = (float)((float)this.gM.PlayerControler.Hero.ExpNow / (float)this.gM.PlayerControler.Hero.ExpToNextLvl) * 150;
        }
        #endregion

        #region Buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz wyjść i zapisać stan gry?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.None) != MessageBoxResult.No)
            {
                gM.SaveEq();
                using (StreamWriter file = File.AppendText(@"Logs\Log SFML " + DateTime.Today.ToShortDateString() + ".txt"))
                {
                    file.WriteLine(DateTime.Now + "[Game Closed]\n");
                };
                this.Close();
            }
        }

        private void RunAdminCommand(object sender, RoutedEventArgs e)
        {
            string[] TakeString = AdminTextBox.Text.Split(' ');
            switch (TakeString[0])
            {
                case "GetMove":
                    AdminConsole.Text += "\nTo działa XD";
                    gM.ACC.GetMove(1, AdminConsole, AdminScroll, this);
                    break;

                case "Clear":
                    switch (TakeString[1])
                    {
                        case "Console":
                            AdminConsole.Text = "";
                            break;
                    }
                    break;
                case "Stop":
                    if(TakeString[1] == "Now")
                        gM.ACC.GetMoveDzialaj = true;
                    else
                    {
                        int IloscCzasuDoZatrzymania = Convert.ToInt32(TakeString[1]);
                        gM.ACC.ZatrzymajWCzasie(IloscCzasuDoZatrzymania);
                    }
                    break;
                default:                   
                    if(TakeString.Length == 2)
                        AdminConsole.Text += "\nBłąd komendy: " + TakeString[0] + "\n Czy na pewno chodziło Ci o obiekt:\n" + TakeString[1];
                    else AdminConsole.Text += "\nSyntax Error: Brak komendy lub obiektu docelowego";
                    break;
            }
        }

        private void AdminTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "TextBox...")
                tb.Text = string.Empty;
            tb.GotFocus -= AdminTextBox_GotFocus;
        }

        private void AdminTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == string.Empty)
                tb.Text = "TextBox...";
        }

        private void OpenAdminConsole_Click(object sender, RoutedEventArgs e)
        {
            AdminScroll.Visibility = Visibility.Visible;
            AdminButton.Visibility = Visibility.Visible;
            AdminTextBox.Visibility = Visibility.Visible;
        }

        private void ActiveInvButton(object sender, RoutedEventArgs e)
        {
            if(gM.button == null)
            {
                (sender as Button).BorderBrush = BorderBrush;
                gM.button = (sender as Button);
                SelectedButton.Visibility = Visibility.Visible;
                SelectedButton.Margin = (sender as Button).Margin;
                return;
            }

            if(gM.button == (sender as Button))
            {
                gM.button = null;
                SelectedButton.Visibility = Visibility.Hidden;
                return;
            } else
            {
                Button Btn1 = this.FindName(gM.button.Name) as Button;
                int IdButtonSender = Convert.ToInt32((sender as Button).Name.Remove(0,1));
                int IdButtonZamiennik = Convert.ToInt32(Btn1.Name.Remove(0, 1));
                int Temp = gM.PlayerControler.Items[IdButtonSender - 1].IdInv;
                Btn1.BorderThickness = new Thickness(0.1);

                Item itemSender = new Item()
                {
                    Type = gM.PlayerControler.Items[IdButtonSender - 1].Type,
                    ItemName = gM.PlayerControler.Items[IdButtonSender - 1].ItemName,
                    Rare = gM.PlayerControler.Items[IdButtonSender - 1].Rare,
                    Cost = gM.PlayerControler.Items[IdButtonSender - 1].Cost,
                    //IdInv = gM.PlayerControler.Items[IdButtonSender - 1].IdInv,
                    IdInv = gM.PlayerControler.Items[IdButtonZamiennik - 1].IdInv,
                    IdSprite = gM.PlayerControler.Items[IdButtonSender - 1].IdSprite
                };

                Brush tempBackground = (sender as Button).Background;
                (sender as Button).Background = Btn1.Background;
                Btn1.Background = tempBackground;

                string tempTooltip = (sender as Button).ToolTip.ToString();
                tempTooltip = tempTooltip.Replace("System.Windows.Controls.ToolTip: ","");
                (sender as Button).ToolTip = Btn1.ToolTip;
                Btn1.ToolTip = tempTooltip;

                SelectedButton.Visibility = Visibility.Hidden;

                gM.PlayerControler.Items[IdButtonSender - 1] = gM.PlayerControler.Items[IdButtonZamiennik - 1];
                gM.PlayerControler.Items[IdButtonSender - 1].IdInv = Temp;
                gM.PlayerControler.Items[IdButtonZamiennik - 1] = itemSender;

                gM.button = null;
                return;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            gM.SaveEq();
        }

        private void OpenOptions_Click(object sender, RoutedEventArgs e)
        {
            gamerunning = false;
            OptionWindow option = new OptionWindow(this);
            option.Show();
        }
        private void SpawnButton_Click(object sender, RoutedEventArgs e)
        {
            this.gM.SpawnPointPlayer();
        }

        private void SkillButton_Click(object sender, RoutedEventArgs e)
        {
            gamerunning = false;
            SkillWindow skill = new SkillWindow(this.gM.PlayerControler, this);
            skill.Show();
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            gamerunning = false;
            StatsWindow stats = new StatsWindow(this.gM.PlayerControler, this);
            stats.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.gM = null;
            GC.Collect();
            gM = new GameManager();
            UpdateStatistics();
        }
        #endregion
    }

    public static class CompositionTargetEx
    {
        private static TimeSpan _last = TimeSpan.Zero;
        private static event EventHandler<RenderingEventArgs> FrameUpdating;
        public static event EventHandler<RenderingEventArgs> Rendering
        {
            add
            {
                if (FrameUpdating == null)
                    CompositionTarget.Rendering += CompositionTarget_Rendering;
                FrameUpdating += value;
            }
            remove
            {
                FrameUpdating -= value;
                if (FrameUpdating == null)
                    CompositionTarget.Rendering -= CompositionTarget_Rendering;
            }
        }

        static void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            RenderingEventArgs args = (RenderingEventArgs)e;
            if (args.RenderingTime == _last)
                return;
            _last = args.RenderingTime; FrameUpdating(sender, args);
        }
    }
}