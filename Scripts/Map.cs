using System;
using System.IO;
using SFML.Graphics;

namespace EGOET.Maps
{
    class Map
    {
        public Sprite[,] Tiles;

        internal int[,] mapInfo;
        internal int GlebokoscOdswiezania;
        internal bool[,] playerView = new bool [100, 100];

        private readonly int TileX = 54, TileY = 31, TileSet = 32;

        public Map()
        {
            Texture TextureMap = new Texture(@"..\..\Resources\trees-and-bushes.png");
            int TTX = Convert.ToInt32(TextureMap.Size.X/32);
            int TTY = Convert.ToInt32(TextureMap.Size.Y/32);
            Sprite[] TileMap = new Sprite[TTX * TTY];
            GlebokoscOdswiezania = Properties.Settings.Default.RenderDistance;

            for (int i = 0; i < TTY; i++)
            {
                for (int j = 0; j < TTX; j++)
                {
                    IntRect rect = new IntRect(j * TileSet, i * TileSet, TileSet, TileSet);
                    TileMap[(i * TTX) + j] = new Sprite(TextureMap, rect);
                }
            }

            Tiles = new Sprite[TileX, TileY];
            StreamReader reader = new StreamReader(@"..\..\GameProbs.csv");
            int[,] mapinfo = new int[100, 100];
            
            for (int y = 0; y < TileY; y++)
            {
                string line = reader.ReadLine();
                string[] items = line.Split(',');
                for (int x = 0; x < TileX; x++)
                {
                    int id = Convert.ToInt32(items[x]);
                    Tiles[x, y] = new Sprite(TileMap[id])
                    {
                        Position = new SFML.System.Vector2f(TileSet * x, TileSet * y)
                    };
                    if (id != 36)
                        mapinfo[x, y] = 1;
                    if(id == 27)
                        mapinfo[x, y] = 2;
                }
            }
            mapInfo = mapinfo;
        }

        public void Draw(RenderWindow window, int TileX, int TileY)
        {
            Array.Clear(playerView, 0, playerView.Length);
            for (int i = TileX - GlebokoscOdswiezania; i < TileX + GlebokoscOdswiezania; i++)
            {
                for (int j = TileY - GlebokoscOdswiezania; j < TileY + GlebokoscOdswiezania; j++)
                {
                    if (i >= 0 && j >= 0 && i<this.TileX && j < this.TileY)
                    {
                        window.Draw(drawable: Tiles[i, j]);
                        playerView[i, j] = true;
                    }
                }
            }
        }
    }
}