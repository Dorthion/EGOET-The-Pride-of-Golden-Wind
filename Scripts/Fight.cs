using EGOET.Informations;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace EGOET.Scripts
{
    class Fight
    {
        private bool dead = false;
        private bool deadplayer = false;
        private bool deadmonster = false;
        private int attackPer = 1000;
        private int counter = 0;
        private float HpMonster;

        private Text quitText;
        private readonly Text attackText;
        private readonly Text moredmgText;
        private readonly Text moredefText;
        private readonly Text monsterText;
        private readonly Text playerattackText;

        private readonly Sprite playerSprite;
        private readonly Sprite monsterSprite;
        private readonly Sprite buttonSprite;
        private readonly Sprite logSprite;
        private readonly Sprite moredmgSprite;
        private readonly Sprite moredefSprite;
        private readonly Sprite graveSprite;
        private readonly Sprite quitSprite;
        private readonly Sprite battlegroundSprite;

        internal readonly PlayerClass player;
        internal readonly MonsterClass monster;

        private readonly Font font;
        private readonly RectangleShape mainRactangle;
        private readonly RectangleShape phpRactangle;
        private readonly RectangleShape mhpRactangle;

        private readonly FloatRect buttonBounds;
        private readonly FloatRect moredefBound;
        private readonly FloatRect moredmgBounds;
        private readonly FloatRect quitBounds;

        internal bool DisableFight = false;
        internal View view = new View(new Vector2f(859, 457), new Vector2f(1718, 949));

        public Fight(PlayerClass _player, MonsterClass _monster)
        {
            player = _player;
            monster = _monster;
            attackPer /= monster.Level;
            HpMonster = monster.Hp;

            Texture LoadBattleground = new Texture(@"..\..\Resources\BattlegroundForest.png");
            Texture ButtonBackground = new Texture(@"..\..\Resources\Fight.png");
            Texture ButtonTexture = new Texture(@"..\..\Resources\Button.png");
            Texture Grave = new Texture(@"..\..\Resources\Grave.png");
            Texture texture = new Texture(_player.Hero.SpritePath);
            Texture texture2 = new Texture(_monster.SpritePath);
            Random random = new Random();

            var spriteRect = new IntRect(32, 64, 32, 32);
            var spriteRect2 = new IntRect(32, 32, 32, 32);
            float tempX = random.Next(900, 1000);
            float tempY = random.Next(350, 450);

            font = new Font(@"..\..\Resources\Fonts\FantaisieArtistique.ttf");
            attackText = new Text("Atakuj", font);
            moredmgText = new Text("Zwieksz Sile", font);
            moredefText = new Text("Zwieksz Obrone", font);
            monsterText = new Text(monster.Name+ " " + monster.Level + "lvl", font);
            quitText = new Text("Uciekaj", font);

            playerSprite = new Sprite(texture, spriteRect);
            monsterSprite = new Sprite(texture2, spriteRect2);
            battlegroundSprite = new Sprite(LoadBattleground);
            buttonSprite = new Sprite(ButtonTexture);
            logSprite = new Sprite(ButtonTexture);
            moredefSprite = new Sprite(ButtonTexture);
            moredmgSprite = new Sprite(ButtonTexture);
            quitSprite = new Sprite(ButtonTexture);
            graveSprite = new Sprite(Grave);

            battlegroundSprite.Position = new Vector2f(400.0f, 150.0f);
            playerSprite.Position = new Vector2f(660.0f, 400.0f);
            monsterSprite.Position = new Vector2f(tempX, tempY);
            buttonSprite.Position = new Vector2f(540.0f, 555.0f);
            logSprite.Position = new Vector2f(870.0f, 555.0f);
            moredefSprite.Position = new Vector2f(700.0f, 555.0f);
            moredmgSprite.Position = new Vector2f(540.0f, 615.0f);
            quitSprite.Position = new Vector2f(700.0f, 615.0f);

            attackText.Position = new Vector2f(565.0f, 560.0f);
            monsterText.Position = new Vector2f(940.0f, 550.0f);
            moredefText.Position = new Vector2f(710.0f, 570.0f);
            moredmgText.Position = new Vector2f(565.0f, 630.0f);
            quitText.Position = new Vector2f(716.0f, 620.0f);

            moredmgText.CharacterSize = 15;
            moredefText.CharacterSize = 15;

            monsterText.FillColor = Color.Yellow;

            moredefSprite.Scale = new Vector2f(0.25f, 0.25f);
            moredmgSprite.Scale = new Vector2f(0.25f, 0.25f);
            buttonSprite.Scale = new Vector2f(0.25f, 0.25f);
            logSprite.Scale = new Vector2f(0.64f, 0.55f);
            quitSprite.Scale = new Vector2f(0.25f, 0.25f);
            graveSprite.Scale = new Vector2f(0.25f, 0.25f);

            buttonBounds = buttonSprite.GetGlobalBounds();
            moredefBound = moredefSprite.GetGlobalBounds();
            moredmgBounds = moredmgSprite.GetGlobalBounds();
            quitBounds = quitSprite.GetGlobalBounds();

            mainRactangle = new RectangleShape()
            {
                Texture = ButtonBackground,
                Size = new Vector2f(860.0f, 170.0f),
                Position = new Vector2f(430.0f, 526.0f)
            };
            
            phpRactangle = new RectangleShape()
            {
                FillColor = Color.Red,
                Size = new Vector2f((float)((float)player.Hero.Hp / (float)player.Hero.HpMax) * 200.0f, 15.0f),
                Position = new Vector2f(950.0f, 600.0f)
            };
            
            mhpRactangle = new RectangleShape()
            {
                FillColor = Color.Red,
                Size = new Vector2f((float)(HpMonster / (float)monster.Hp) * 200.0f, 15.0f),
                Position = new Vector2f(950.0f, 630.0f)
            };
        }

        public void Update(RenderWindow window)
        {
            Vector2f mousepos = window.MapPixelToCoords(Mouse.GetPosition(window));

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && buttonBounds.Contains(mousepos.X, mousepos.Y) && dead == false)
            {
                HpMonster -= (player.Hero.Sila / 2) + ((player.Hero.Sila / 2) / monster.Obrona);
                if(HpMonster > 0)
                {
                    mhpRactangle.Size = new Vector2f((float)(HpMonster / (float)monster.Hp) * 200.0f, mhpRactangle.Size.Y);
                } 
                else
                {
                    mhpRactangle.Size = new Vector2f(0.0f, 0.0f);
                    dead = true;
                    deadmonster = true;
                    UpdateQuitText();
                    graveSprite.Position = new Vector2f(monsterSprite.Position.X, monsterSprite.Position.Y);
                }
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && quitBounds.Contains(mousepos.X, mousepos.Y))
            {
                DisableFight = true;
            }

            if (counter > attackPer && dead == false)
            {
                player.Hero.Hp -= (monster.Sila / 2) + ((monster.Sila / 2) / player.Hero.Obrona);
                if(player.Hero.Hp > 0)
                {
                    phpRactangle.Size = new Vector2f((float)((float)player.Hero.Hp / (float)player.Hero.HpMax) * 200.0f, phpRactangle.Size.Y);
                    counter = 0;
                } else
                {
                    phpRactangle.Size = new Vector2f(0.0f, 0.0f);
                    dead = true;
                    deadplayer = true;
                    UpdateQuitText();
                    graveSprite.Position = new Vector2f(660.0f, 400.0f);
                }
            }
            else counter++;
        }

        private void UpdateQuitText()
        {
            quitText = new Text("Wyjdz", font);
            quitText.Position = new Vector2f(716.0f, 620.0f);
        }

        public void Draw(RenderWindow window)
        {
            window.SetView(view);

            window.Draw(drawable: battlegroundSprite);
            window.Draw(drawable: mainRactangle);
            window.Draw(drawable: buttonSprite);
            window.Draw(drawable: logSprite);
            window.Draw(drawable: moredmgSprite);
            window.Draw(drawable: moredefSprite);
            window.Draw(drawable: quitSprite);
            window.Draw(drawable: phpRactangle);
            window.Draw(drawable: mhpRactangle);

            if (!deadplayer)
                window.Draw(drawable: playerSprite);
            else window.Draw(drawable: graveSprite);

            if (!deadmonster)
                window.Draw(drawable: monsterSprite);
            else window.Draw(drawable: graveSprite);

            window.Draw(drawable: attackText);
            window.Draw(drawable: monsterText);
            window.Draw(drawable: moredefText);
            window.Draw(drawable: moredmgText);
            window.Draw(drawable: quitText);
        }
    }
}