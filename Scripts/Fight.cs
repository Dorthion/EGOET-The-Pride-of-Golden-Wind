using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EGOET.Scripts
{
    class Fight
    {
        internal View view = new View(new Vector2f(859, 457), new Vector2f(1718, 949));
        private Font font;
        private RectangleShape Prostokat;
        private RectangleShape LogRectangle;
        private Sprite battleground;
        private Player player;
        private Monster monster;

        internal bool DisableFight = false;
        public Fight(Player player, Monster monster)
        {
            font = new Font(@"..\..\Resources\Fonts\Font.ttf");
            Texture LoadBattleground = new Texture(@"..\..\Resources\BattlegroundForest.png");
            Texture ButtonBackground = new Texture(@"..\..\Resources\Fight.png");
            battleground = new Sprite(LoadBattleground);
            battleground.Position = new Vector2f(400.0f, 150.0f);
            Prostokat = new RectangleShape()
            {
                Size = new Vector2f(860, 170),
                Texture = ButtonBackground,
                Position = new Vector2f(10000, 10000)
            };

            /*LogRectangle = new RectangleShape()
            {
                Size = new Vector2f(500, 180),
                FillColor = Color.Red,
                Position = new Vector2f(10000, 10000)
            };*/

            this.player = player;
            this.monster = monster;
        }

        public void Update(float posX, float posY)
        {
            Prostokat.Position = new Vector2f(posX+140, posY + 404);
            this.player.Xpos = posX;
            this.player.Ypos = posY;
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                DisableFight = true;
            }
        }

        public void Draw(RenderWindow window)
        {
            window.SetView(view);
            window.Draw(drawable: battleground);
            window.Draw(drawable: Prostokat);
            //window.Draw(drawable: LogRectangle);
            this.player.Draw(window);

        }
    }
}
