using SFML.Window;

namespace EGOET
{
    class Player : AnimationCharacter
    {
        private int[,] _tab;

        internal bool IsDead = false;
        internal bool IsActionNear = false;
        internal bool IsActionWithChest = false;
        internal bool IsActionWithNPC = false;
        internal bool IsFighting = false;

        internal int XPosAction { get; set; }
        internal int YPosAction { get; set; }

        public Player(int[,] tab, string path) : base(path, 32) {
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
                if (_tab[(int)(Xpos + 16) / 32, (int)Ypos / 32] != 1)
                {
                    if (!PlayerAction((int)(Xpos + 16) / 32, (int)Ypos / 32, (int)Xpos, (int)Ypos))
                        return;
                }
                else return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                this.CurrentState = CharacterState.MovingDown;
                if (_tab[(int)(Xpos + 16) / 32, (int)(Ypos + 32)/ 32] != 1)
                {
                    if(!PlayerAction((int)(Xpos + 16) / 32, (int)(Ypos + 32) / 32, (int)Xpos, (int)Ypos))
                        return;
                }
                else return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.CurrentState = CharacterState.MovingLeft;
                if (_tab[(int)Xpos / 32, (int)(Ypos + 16)/ 32] != 1)
                {
                    if(!PlayerAction((int)Xpos / 32, (int)(Ypos + 16) / 32, (int)Xpos, (int)Ypos))
                        return;
                }
                else return;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                this.CurrentState = CharacterState.MovingRight;
                if (_tab[(int)(Xpos + 32) / 32, (int)(Ypos + 16) / 32] != 1)
                {
                    if(!PlayerAction((int)(Xpos + 32) / 32, (int)(Ypos + 16) / 32, (int)Xpos, (int)Ypos))
                        return;
                }
                else return;
                }
            base.Update(deltatime);
        }

        private bool PlayerAction(int x, int y, int XPos, int YPos)
        {
            switch (_tab[x, y])
            {
                case 0: //Zwykła ścieżka

                    IsActionWithNPC = false;
                    IsActionNear = false;
                    IsFighting = false;

                    XPosAction = 0;
                    YPosAction = 0;

                    return true;

                case 1:
                    IsActionWithNPC = false;
                    IsActionNear = false;
                    IsFighting = false;

                    return false;

                case 2:
                    //Akcja rozmowa
                    IsActionWithNPC = true;
                    IsActionNear = true;

                    XPosAction = XPos;
                    YPosAction = YPos;

                    return false;

                case 3:
                    //Akcja ze skrzynką

                    break;

                case 4:
                    //Walka

                    IsFighting = true;
                    IsActionNear = true;

                    XPosAction = XPos;
                    YPosAction = YPos;

                    return false;

                default:
                    return true;
            }
            return true;
        }
    }
}