using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SFML.Graphics;

namespace EGOET.Scripts.Items.Inventory
{
    class Inventory
    {
        internal ImageBrush[] TileMap;
        internal CroppedBitmap UnderButton;
        internal Sprite ActionIcon;

        public Inventory()
        {
            int TTX = 16, TTY = 16, TileSet = 32;
            BitmapImage src = new BitmapImage();
            TileMap = new ImageBrush[TTX * TTY];

            src.BeginInit();
            src.UriSource = new Uri(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Resources\Items4.png", UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();

            for (int i = 0; i < TTX; i++)
                for (int j = 0; j < TTY; j++)
                    TileMap[(i * TTX) + j] = new ImageBrush(new CroppedBitmap(src, new Int32Rect(j * TileSet, i * TileSet, TileSet, TileSet)));
            UnderButton = new CroppedBitmap(src, new Int32Rect(11 * TileSet, 0 , TileSet, TileSet));
            ActionIcon = new Sprite(new Texture(src.UriSource.ToString()), new IntRect(13 * TileSet, 0 , TileSet, TileSet));
        }
    }
}
