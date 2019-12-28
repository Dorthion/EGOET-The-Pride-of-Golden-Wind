using System;
using System.IO;
using SFML.Graphics;

namespace EGOET.Maps
{
    class Map
    {
        public Sprite[,] Tiles;
        internal int TTX = 0, TTY = 0;
        internal bool[,] mapInfo;
        private readonly int TileX = 54, TileY = 31, TileSet = 32;
        //Sprite TextureMap2 = new Sprite(new Texture(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Resources\zad.png"));
        //private readonly int MapSizeX = 1824, MapSizeY = 896;

        public Map()
        {
            //Texture TextureMap = new Texture("../../Resources/PathAndObjects.png");
            Texture TextureMap = new Texture(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Resources\trees-and-bushes.png");
            TTX = Convert.ToInt32(TextureMap.Size.X/32);
            TTY = Convert.ToInt32(TextureMap.Size.Y/32);
            Sprite[] TileMap = new Sprite[TTX * TTY];


            for (int i = 0; i < TTY; i++)
            {
                for (int j = 0; j < TTX; j++)
                {
                    IntRect rect = new IntRect(j * TileSet, i * TileSet, TileSet, TileSet);
                    TileMap[(i * TTX) + j] = new Sprite(TextureMap, rect);
                }
            }

            Tiles = new Sprite[TileX, TileY];
            StreamReader reader = new StreamReader(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\GameProbs.csv");
            bool[,] mapinfo = new bool[1000,1000];
            for (int y = 0; y < TileY; y++)
            {
                string line = reader.ReadLine();
                string[] items = line.Split(',');
                for (int x = 0; x < TileX; x++)
                {
                    int id = Convert.ToInt32(items[x]);
                    Tiles[x, y] = new Sprite(TileMap[id]);
                    Tiles[x, y].Position = new SFML.System.Vector2f(TileSet * x, TileSet * y);
                    if(id != 36)
                        mapinfo[x, y] = true;
                }
            }
            mapInfo = mapinfo;
        }

        public void Draw(RenderWindow window, int TileX, int TileY, int GlebokoscOdswiezania)
        {
            for (int i = TileX - GlebokoscOdswiezania; i < TileX + GlebokoscOdswiezania; i++)
            {
                for (int j = TileY - GlebokoscOdswiezania; j < TileY + GlebokoscOdswiezania; j++)
                {
                    if (i >= 0 && j >= 0 && i<this.TileX && j<this.TileY)
                        window.Draw(drawable: Tiles[i, j]);
                }
            }
        }
    }
}