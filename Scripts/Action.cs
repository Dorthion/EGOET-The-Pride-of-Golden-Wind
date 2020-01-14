using SFML.Graphics;
using SFML.System;

namespace EGOET.Scripts
{
    class Action
    {
        private Sprite ActionSprite { get; set; }

        public Action(Sprite sprite)
        {
            ActionSprite = sprite;
        }

        public void Update(float Xplayer, float Yplayer)
        {
            ActionSprite.Position = new Vector2f(Xplayer, Yplayer - 32);
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(drawable: ActionSprite);
        }
    }
}