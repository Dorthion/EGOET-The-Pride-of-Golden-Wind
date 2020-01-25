using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace EGOET.Scripts
{
    class Fight
    {
        internal View view = new View(new Vector2f(859, 457), new Vector2f(1718, 949));
        private Font font;
        private RectangleShape mainRactangle;
        //private RectangleShape LogRectangle;
        private Sprite battleground;
        private Sprite tempplayer;
        private Sprite tempmonster;

        internal bool DisableFight = false;
        public Fight(string playersprite, string monstersprite)
        {
            Texture LoadBattleground = new Texture(@"..\..\Resources\BattlegroundForest.png");
            Texture ButtonBackground = new Texture(@"..\..\Resources\Fight.png");
            Texture texture = new Texture(playersprite);
            Texture texture2 = new Texture(monstersprite);
            var spriteRect = new IntRect(32, 64, 32, 32);
            var spriteRect2 = new IntRect(32, 32, 32, 32);

            Random random = new Random();
            float tempX = random.Next(900, 1000);
            float tempY = random.Next(350, 450);

            font = new Font(@"..\..\Resources\Fonts\Font.ttf");
            tempplayer = new Sprite(texture, spriteRect);
            tempmonster = new Sprite(texture2, spriteRect2);
            battleground = new Sprite(LoadBattleground);
            battleground.Position = new Vector2f(400.0f, 150.0f);
            tempplayer.Position = new Vector2f(660, 400);
            tempmonster.Position = new Vector2f(tempX, tempY);

            mainRactangle = new RectangleShape()
            {
                Texture = ButtonBackground,
                Size = new Vector2f(860, 170),
                Position = new Vector2f(430, 526)
            };

            /*LogRectangle = new RectangleShape()
            {
                Size = new Vector2f(500, 180),
                FillColor = Color.Red,
                Position = new Vector2f(10000, 10000)
            };*/
        }

        public void Update()
        {
            
            //mainRactangle.Position = new Vector2f(430, 526);
            

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                DisableFight = true;
            }
        }

        public void Draw(RenderWindow window)
        {
            window.SetView(view);
            window.Draw(drawable: battleground);
            window.Draw(drawable: mainRactangle);
            window.Draw(drawable: tempplayer);
            window.Draw(drawable: tempmonster);
            //window.Draw(drawable: LogRectangle);

        }
    }
}
