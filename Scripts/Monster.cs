using EGOET.Informations;

namespace EGOET.Scripts
{
    class Monster : AnimatedCharacterWithAI
    {
        internal Monsterclass monsterclass;
        public Monster(string path, Monsterclass _monsterclass) : base(path, 32)
        {
            Anim_Down = new Animation(0, 0, 4);
            Anim_Left = new Animation(32, 0, 4);
            Anim_Right = new Animation(64, 0, 4);
            Anim_Up = new Animation(96, 0, 4);
            monsterclass = _monsterclass;
        }
    }
}