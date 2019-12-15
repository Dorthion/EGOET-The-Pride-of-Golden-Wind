using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace EGOET
{
    class Statistic
    {
        internal int X;
        internal int Y;
        internal int[,] Mapa;
        internal Bitmap[] PathAndObjects;
        internal int IloscObiektowX = 0;
        internal int IloscObiektowY = 0;
        internal BitmapSource TempMap;

        public Statistic(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Mapa = new int[X,Y];
            Bitmap img = Image.FromFile(@"Resources\PathAndObjects.png") as Bitmap;
            IloscObiektowX = img.Width / 32;
            IloscObiektowY = img.Height / 32;
            var imgarray = new Bitmap[IloscObiektowX * IloscObiektowY];
            //var img2 = Image.FromFile(@"Resources\TempMap");

            for (int i = 0; i < IloscObiektowX; i++)
            {
                for (int j = 0; j < IloscObiektowY; j++)
                {
                    var index = i * IloscObiektowX + j;
                    imgarray[index] = new Bitmap(32, 32);
                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, 32, 32), new Rectangle(i * 32, j * 32, 32, 32), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }
            PathAndObjects = imgarray;
            TempMap = MergeImagess();
        }

        public BitmapSource MergeImagess()
        {
            Bitmap result = new Bitmap(32 * IloscObiektowX, 32 * IloscObiektowY);
            
            using (Graphics g = Graphics.FromImage(result))
            {
                for (int i = 0; i < IloscObiektowX; i++)
                {
                    for (int j = 0; j < IloscObiektowY; j++)
                    {
                        var index = i * IloscObiektowX + j;
                        g.DrawImage(PathAndObjects[index], 32 * i, 32 * j);
                    }
                }
            }
            return Bitmap2BitmapImage(result);
        }

        private BitmapSource Bitmap2BitmapImage(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(
               bitmap.GetHbitmap(),
               IntPtr.Zero,
               Int32Rect.Empty,
               BitmapSizeOptions.FromWidthAndHeight(32 * IloscObiektowX, 32 * IloscObiektowY));
        }
    }
}
