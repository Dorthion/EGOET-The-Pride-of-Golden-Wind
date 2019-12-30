﻿using EGOET.Informations;
using MahApps.Metro.Controls;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EGOET.Scripts;

namespace EGOET
{
    public partial class MainWindow : MetroWindow
    {
        internal GameManager gM;
        internal RenderWindow _renderWindow = null;

        private Clock clock;
        private View view = new View(new Vector2f(0, 0), new Vector2f(1718, 949));
        private RectangleShape ClearRect = new RectangleShape()
        {
            Position = new Vector2f(0, 0),
            Size = new Vector2f(2000,1000),
            FillColor = SFML.Graphics.Color.Black
        };

        public MainWindow()
        {
            InitializeComponent();
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

            this._renderWindow = new RenderWindow(this.DrawSurface.Handle, context);
            CompositionTargetEx.Rendering += Timer_Tick;
            gM = new GameManager();
            clock = new Clock();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            ShowMaxRestoreButton = false;
            ShowMinButton = false;
            Loaded -= OnLoaded;

            NicknameLabel.Content = gM.PlayerControler.Hero.Name.ToString();
            TownLabel.Content = (GameManager.Towns)gM.PlayerControler.Hero.IdMiasta;
            MoneyLabel.Content = gM.PlayerControler.Hero.Money.ToString();
            LvlLabel.Content = gM.PlayerControler.Hero.Poziom.ToString();

            gM.LoadInventory(this);
        }

        public MainWindow(PlayerClass _player)
        {
            InitializeComponent();
            clock = new Clock();
            //PlayerControler = _player;

           // CreateRenderWindow();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //this._renderWindow.DispatchEvents();
            float deltatime = clock.Restart().AsSeconds();
            this._renderWindow.SetView(view);

            //Clear Screen
            this.ClearRect.Position = new Vector2f(gM.player.Xpos - 1000.0f, gM.player.Ypos - 500.0f);
            this._renderWindow.Draw(ClearRect);

            //Update State
            this.gM.kip.Update(deltatime);
           if(gM.Mapa.mapInfo[(int)gM.player.Xpos/32 + 1, (int)gM.player.Ypos/32 + 1] == false)
                this.gM.player.Update(deltatime);
            this.gM.UpdateScreen(_renderWindow);

            //Center View
            this.view.Center = new Vector2f(gM.player.Xpos, gM.player.Ypos);
            
            //Display
            this._renderWindow.Display();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._renderWindow.SetFramerateLimit(0);
            this._renderWindow.SetVerticalSyncEnabled(false);
            this._renderWindow.DispatchEvents();

            this.view.Zoom(0.7f);
            gM.kip.CurrentState = CharacterState.MovingDown;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

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
            //Zamienić to potem na switcha
            //Jeżeli nic nie jest zaznaczone
            if(gM.button == null)
            {
                (sender as Button).BorderBrush = BorderBrush;
                gM.button = (sender as Button);
                return;
            }

            //Jeżeli chcemy odznaczyć item, w przeciwnym wypadku zamienić miejsce
            if(gM.button == (sender as Button))
            {
                //(sender as Button).BorderThickness = new Thickness(0.1);
                gM.button = null;
                return;
            } else
            {
                //(sender as Button).BorderThickness = new Thickness(0.1);
                int IdButtonSender = Convert.ToInt32((sender as Button).Name.Remove(0,1));

                Button Btn1 = this.FindName(gM.button.Name) as Button;
                int IdButtonZamiennik = Convert.ToInt32(Btn1.Name.Remove(0, 1));
                Btn1.BorderThickness = new Thickness(0.1);

                Item itemSender = new Item()
                {
                    Type = gM.PlayerControler.Items[IdButtonSender - 1].Type,
                    Rare = gM.PlayerControler.Items[IdButtonSender - 1].Rare,
                    IdInv = gM.PlayerControler.Items[IdButtonSender - 1].IdInv,
                    IdSprite = gM.PlayerControler.Items[IdButtonSender - 1].IdSprite
                };

                Brush tempBackground = (sender as Button).Background;
                (sender as Button).Background = Btn1.Background;
                Btn1.Background = tempBackground;

                ToolTip tempTooltip = new ToolTip();
                tempTooltip.Content = (sender as Button).ToolTip.ToString();
                (sender as Button).ToolTip = Btn1.ToolTip;
                Btn1.ToolTip = tempTooltip;

                gM.PlayerControler.Items[IdButtonSender - 1] = gM.PlayerControler.Items[IdButtonZamiennik - 1];
                gM.PlayerControler.Items[IdButtonZamiennik - 1] = itemSender;

                string tempName = Btn1.Name;
                Btn1.Name = (sender as Button).Name;
                (sender as Button).Name = tempName;

                gM.button = null;
                return;
            }
            
        }
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