using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EGOET.Scripts
{
    class Fight
    {
        internal View view = new View(new Vector2f(430, 238), new Vector2f(1718, 949));
        private Font font;
        private RectangleShape Prostokat;
        private RectangleShape LogRectangle;
        private Sprite battleground;

        internal bool DisableFight = false;
        public Fight()
        {
            font = new Font(@"..\..\Resources\Fonts\Font.ttf");
            Texture LoadBattleground = new Texture(@"..\..\Resources\BattlegroundForest.png");
            battleground = new Sprite(LoadBattleground);

            Prostokat = new RectangleShape()
            {
                Size = new Vector2f(1200, 200),
                FillColor = Color.Red,
                Position = new Vector2f(10000, 10000)
            };

            LogRectangle = new RectangleShape()
            {
                Size = new Vector2f(500, 180),
                FillColor = Color.Red,
                Position = new Vector2f(10000, 10000)
            };
        }

        public void Update(float posX, float posY)
        {
            Prostokat.Position = new Vector2f(posX, posY + 200);
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
            window.Draw(drawable: LogRectangle);
        }
    }
}
