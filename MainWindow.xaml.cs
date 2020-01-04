using EGOET.Informations;
using MahApps.Metro.Controls;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EGOET.Scripts;
using System.Windows.Media.Imaging;

namespace EGOET
{
    public partial class MainWindow : MetroWindow
    {
        internal GameManager gM;
        internal RenderWindow _renderWindow = null;

        private readonly Clock clock;
        private readonly View view = new View(new Vector2f(0, 0), new Vector2f(1718, 949));
        private readonly RectangleShape ClearRect = new RectangleShape()
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
            gM.PlayerControler = _player;

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
            //if(this.gM.Mapa.playerView[(int)(this.gM.kip.Xpos)/32, (int)(this.gM.kip.Xpos)/32] == true)
            this.gM.kip.Update(deltatime);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz wyjść i zapisać stan gry?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.None) != MessageBoxResult.No)
            {
                gM.SaveEq();
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

                string tempTooltip = (sender as Button).ToolTip.ToString();
                tempTooltip = tempTooltip.Replace("System.Windows.Controls.ToolTip: ","");
                (sender as Button).ToolTip = Btn1.ToolTip;
                Btn1.ToolTip = tempTooltip;

                SelectedButton.Visibility = Visibility.Hidden;

                gM.PlayerControler.Items[IdButtonSender - 1] = gM.PlayerControler.Items[IdButtonZamiennik - 1];
                gM.PlayerControler.Items[IdButtonZamiennik - 1] = itemSender;

                gM.button = null;
                return;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            gM.SaveEq();
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