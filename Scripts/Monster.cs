using EGOET.Informations;

namespace EGOET.Scripts
{
    class Monster : AnimationCharacter
    {
        internal Monsterclass monsterclass;
        public Monster(string path, Monsterclass _monsterclass) : base(path, 32)
        {
            Anim_Down = new Animation(0, 0, 4);
            Anim_Left = new Animation(32, 0, 4);
            Anim_Right = new Animation(64, 0, 4);
            Anim_Up = new Animation(96, 0, 4);

            Xpos = _monsterclass.XPos * 32;
            Ypos = _monsterclass.YPos * 32;

            monsterclass = _monsterclass;
        }
    }
}