using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EGOET.Scripts.Items.Inventory
{
    class Inventory
    {
        internal ImageBrush[] TileMap;

        public Inventory()
        {
            int TTX = 16, TTY = 16, TileSet = 32;
            BitmapImage src = new BitmapImage();
            TileMap = new ImageBrush[TTX * TTY];

            src.BeginInit();
            src.UriSource = new Uri(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Resources\PathAndObjects.png", UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();

            for (int i = 0; i < TTX; i++)
                for (int j = 0; j < TTY; j++)
                    TileMap[(i * TTX) + j] = new ImageBrush(new CroppedBitmap(src, new Int32Rect(j * TileSet, i * TileSet, TileSet, TileSet)));
        }
    }

    public class Item
    {
        public string Type { get; set; }
        public int Rare { get; set; }
        public int IdInv { get; set; }
        public int IdSprite { get; set; }
    }
}
