using SFML.Graphics;
using SFML.System;

namespace EGOET.Scripts
{
    abstract class SingleAnimationMonster
    {
        public float Xpos { get; set; }
        public float Ypos { get; set; }

        private Sprite sprite;
        private IntRect spriteRect;
        private int frameSize;

        public CharacterState CurrentState { get; set; }

        protected Animation Anim_Down;

        private Clock AnimationClock;
        protected float animationSpeed = 0.1f;

        public SingleAnimationMonster(string Filename, int FrameSize)
        {
            this.frameSize = FrameSize;
            Texture texture = new Texture(Filename);

            spriteRect = new IntRect(0, 0, frameSize, frameSize);
            sprite = new Sprite(texture, spriteRect);

            AnimationClock = new Clock();
        }

        public virtual void Update(float deltaTime)
        {
            Animation currentAnimation = null;
            switch (CurrentState)
            {
                case CharacterState.MovingDown:
                    currentAnimation = Anim_Down;
                    break;
            }

            sprite.Position = new Vector2f(Xpos, Ypos);

            if (AnimationClock.ElapsedTime.AsSeconds() > animationSpeed)
            {
                if (currentAnimation != null)
                {
                    spriteRect.Top = currentAnimation.offsetTop;
                    if (spriteRect.Left == (currentAnimation.numFrames - 2) * frameSize)
                        spriteRect.Left = 0;
                    else
                        spriteRect.Left += frameSize;
                }
                AnimationClock.Restart();
            }
            sprite.TextureRect = spriteRect;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(sprite);
        }
        
    }
}
