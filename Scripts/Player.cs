using SFML.Window;

namespace EGOET
{
    class Player : AnimationCharacter
    {
        private bool[,] _tab;
        public Player(bool[,] tab) : base(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Sprites\Ruda_Dlugie.png", 32) {
            Anim_Down = new Animation(0, 0, 4);
            Anim_Left = new Animation(32, 0, 4);
            Anim_Right = new Animation(64, 0, 4);
            Anim_Up = new Animation(96, 0, 4);

            moveSpeed = 70;
            animationSpeed = 0.08f;
            _tab = tab;
        }

        public override void Update (float deltatime)
        {
            this.CurrentState = CharacterState.None;

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                this.CurrentState = CharacterState.MovingUp;
                if (_tab[(int)(Xpos + 16) / 32, (int)Ypos / 32] == true) return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                this.CurrentState = CharacterState.MovingDown;
                if (_tab[(int)(Xpos + 16) / 32, (int)(Ypos + 32)/ 32] == true) return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.CurrentState = CharacterState.MovingLeft;
                if (_tab[(int)Xpos / 32, (int)(Ypos + 16)/ 32] == true) return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                this.CurrentState = CharacterState.MovingRight;
                if (_tab[(int)(Xpos + 32) / 32, (int)(Ypos + 16)/ 32] == true) return;
            }
            base.Update(deltatime);
        }
    }
}
