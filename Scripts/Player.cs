using System;
using SFML.Window;

namespace EGOET
{
    class Player : AnimationCharacter
    {
        private int[,] _tab;
        internal bool ShowActionIcon = false;
        public Player(int[,] tab) : base(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Sprites\Ruda_Dlugie.png", 32) {
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
                if (_tab[(int)(Xpos + 16) / 32, (int)Ypos / 32] != 1) PlayerAction((int)(Xpos + 16) / 32, (int)Ypos / 32); else return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                this.CurrentState = CharacterState.MovingDown;
                if (_tab[(int)(Xpos + 16) / 32, (int)(Ypos + 32)/ 32] != 1) PlayerAction((int)(Xpos + 16) / 32, (int)(Ypos + 32) / 32); else return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.CurrentState = CharacterState.MovingLeft;
                if (_tab[(int)Xpos / 32, (int)(Ypos + 16)/ 32] != 1) PlayerAction((int)Xpos / 32, (int)(Ypos + 16) / 32); else return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                this.CurrentState = CharacterState.MovingRight;
                if (_tab[(int)(Xpos + 32) / 32, (int)(Ypos + 16) / 32] != 1) PlayerAction((int)(Xpos + 32) / 32, (int)(Ypos + 16) / 32); else return;
            }

            base.Update(deltatime);
        }

        private void PlayerAction(int x, int y)
        {
            switch (_tab[x, y])
            {
                case 0: //Zwykła ścieżka
                    ShowActionIcon = false;
                    return;
                case 2:
                    //Akcja ze skrzynią
                    ShowActionIcon = true;
                    break;
                case 3:
                    //Akcja rozmowa
                    break;
                case 4:
                    //Walka
                    break;
                default:
                    return;
            }
        }
    }
}
