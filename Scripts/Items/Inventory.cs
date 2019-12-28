using System;
using SFML.Graphics;

namespace EGOET.Scripts.Items.Inventory
{
    class Inventory
    {
        //private List<Item> Ekwipunek;

        public Inventory()
        {
            int TTX, TTY, TileSet = 32;
            Texture TextureMap = new Texture(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Resources\trees-and-bushes.png");
            TTX = Convert.ToInt32(TextureMap.Size.X / 32);
            TTY = Convert.ToInt32(TextureMap.Size.Y / 32);
            Sprite[] TileMap = new Sprite[TTX * TTY];


            for (int i = 0; i < TTY; i++)
            {
                for (int j = 0; j < TTX; j++)
                {
                    IntRect rect = new IntRect(j * TileSet, i * TileSet, TileSet, TileSet);
                    TileMap[(i * TTX) + j] = new Sprite(TextureMap, rect);
                }
            }
        }
    }

    public abstract class Item
    {
        private int Id = 0;
        private int Rare = 0;

        public Item(int id, int rare)
        {
            Id = id;
            Rare = rare;
        }
    }
}
