using SFML.Graphics;
using SFML.System;

namespace EGOET.Scripts
{
    class Action
    {
        public float Xpos { get; set; }
        public float Ypos { get; set; }
        private Sprite ActionSprite;

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
