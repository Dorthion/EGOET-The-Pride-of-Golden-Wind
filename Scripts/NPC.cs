using EGOET.Informations;

namespace EGOET
{
    class NPC : AnimationCharacter
    {
        internal NPCclass npc;
        public NPC(string path, NPCclass npcclass) : base(path, 32)
        {
            Anim_Down = new Animation(0, 0, 4);
            Anim_Left = new Animation(32, 0, 4);
            Anim_Right = new Animation(64, 0, 4);
            Anim_Up = new Animation(96, 0, 4);
            npc = npcclass;
        }
    }
}
