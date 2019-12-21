namespace EGOET
{
    class NPC : AnimationCharacter
    {
        public NPC() : base("../../Sprites/Ruda_Krotkie.png", 32)
        {
            Anim_Down = new Animation(0, 0, 4);
            Anim_Left = new Animation(32, 0, 4);
            Anim_Right = new Animation(64, 0, 4);
            Anim_Up = new Animation(96, 0, 4);
        }
    }
}
