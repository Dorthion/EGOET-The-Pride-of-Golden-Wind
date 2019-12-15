using System;
using System.Windows;
using System.Windows.Threading;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using EGOET.Maps;
using EGOET.Informations;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;

namespace EGOET
{
    public partial class MainWindow : MetroWindow
    {
        private AdminConsoleCommands ACC = new AdminConsoleCommands();
        internal RenderWindow _renderWindow = null;
        private Map Mapa;
        private NPC kip;
        internal Player player { get; private set; }
        public Sprite JakisSprite;
        private PlayerClass PlayerControler;
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

            ContextSettings context = new ContextSettings()
            {
                AttributeFlags = ContextSettings.Attribute.Default,
                MajorVersion = 4,
                MinorVersion = 6,
                DepthBits = 0,
                StencilBits = 0,
                AntialiasingLevel = 0
            };

            Mapa = new Map();
            kip = new NPC();
            player = new Player();
            this._renderWindow = new RenderWindow(this.DrawSurface.Handle, context);
            CompositionTargetEx.Rendering += Timer_Tick;
            clock = new Clock();
        }

        public MainWindow(PlayerClass _player)
        {
            InitializeComponent();
            clock = new Clock();
            PlayerControler = _player;

           // CreateRenderWindow();
        }

        private void CreateRenderWindow()
        {
            if (_renderWindow != null)
            {
                this._renderWindow.SetActive(false);
                this._renderWindow.Dispose();
            }

            _renderWindow.SetFramerateLimit(0);
            _renderWindow.SetVerticalSyncEnabled(false);
            //this._renderWindow.MouseButtonPressed += RenderWindow_MouseButtonPressed;
            //this._renderWindow.KeyPressed += RenderWindow_KeyPressed;

            _renderWindow.SetActive(true);
        }

        //Na razie "dla zwiększenia wydajności"
        public static class CompositionTargetEx
        {
            private static TimeSpan _last = TimeSpan.Zero;
            private static event EventHandler<RenderingEventArgs> _FrameUpdating;
            public static event EventHandler<RenderingEventArgs> Rendering
            {
                add
                {
                    if (_FrameUpdating == null)
                        CompositionTarget.Rendering += CompositionTarget_Rendering;
                    _FrameUpdating += value;
                }
                remove
                {
                    _FrameUpdating -= value;
                    if (_FrameUpdating == null)
                        CompositionTarget.Rendering -= CompositionTarget_Rendering;
                }
            }

            static void CompositionTarget_Rendering(object sender, EventArgs e)
            {
                RenderingEventArgs args = (RenderingEventArgs)e;
                if (args.RenderingTime == _last)
                    return;
                _last = args.RenderingTime; _FrameUpdating(sender, args);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //this._renderWindow.DispatchEvents();
            float deltatime = clock.Restart().AsSeconds();

            this._renderWindow.SetView(view);

            //Clear Screen
            this.ClearRect.Position = new Vector2f(player.Xpos - 1000.0f, player.Ypos - 500.0f);
            this._renderWindow.Draw(ClearRect);

            //Update State
            this.kip.Update(deltatime);
            this.player.Update(deltatime);

            //Draw Methods
            this.Mapa.Draw(_renderWindow, (int)(player.Xpos/32), (int)(player.Ypos/32), 5);
            this.kip.Draw(_renderWindow);
            this.player.Draw(_renderWindow);

            //Center View
            this.view.Center = new Vector2f(player.Xpos, player.Ypos);
            
            this._renderWindow.Display();
            //Zakomentować wszystko, sprawdzić wydajność programu pod wzzględem ilości ticków aplikacji
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Na później
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            //Prawdopodobnie do skasowania (Wpf jest ograniczony)
            this._renderWindow.SetFramerateLimit(0);
            this._renderWindow.SetVerticalSyncEnabled(false);
            this._renderWindow.DispatchEvents();
            
            this.view.Zoom(0.7f);
            kip.CurrentState = CharacterState.MovingDown;
            //CreateRenderWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

        private void RunAdminCommand(object sender, RoutedEventArgs e)
        {
            string[] TakeString = AdminTextBox.Text.Split(' ');
            switch (TakeString[0])
            {
                case "GetMove":
                    AdminConsole.Text += "\nTo działa XD";
                    ACC.GetMove(1, AdminConsole, AdminScroll, this);
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
                        ACC.GetMoveDzialaj = true;
                    else
                    {
                        int IloscCzasuDoZatrzymania = Convert.ToInt32(TakeString[1]);
                        ACC.ZatrzymajWCzasie(IloscCzasuDoZatrzymania);
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
    }
}