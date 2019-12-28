using SFML.Window;

namespace EGOET
{
    class Player : AnimationCharacter
    {
        public Player() : base(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Sprites\Ruda_Dlugie.png", 32) {
            Anim_Down = new Animation(0, 0, 4);
            Anim_Left = new Animation(32, 0, 4);
            Anim_Right = new Animation(64, 0, 4);
            Anim_Up = new Animation(96, 0, 4);

            moveSpeed = 70;
            animationSpeed = 0.08f;
        }

        public override void Update (float deltatime)
        {
            this.CurrentState = CharacterState.None;

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                this.CurrentState = CharacterState.MovingUp;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                this.CurrentState = CharacterState.MovingDown;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.CurrentState = CharacterState.MovingLeft;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                this.CurrentState = CharacterState.MovingRight;
            }
            base.Update(deltatime);
        }
    }
}
