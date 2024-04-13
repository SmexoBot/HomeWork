using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf
{
    internal class Painter
    {
        public WriteableBitmap Paint(string input, int[] array)
        {
            int xStart = array[0];
            int xEnd = array[1];
            int step = array[2];
            int scale = array[3];
            int width = array[4];
            int height = array[5];
            int xZero = width / 2;
            int yZero = height / 2;
            WriteableBitmap image = BitmapFactory.New(width, height);
            for (int i = 0; i < width; i++)
            {
                image.SetPixel(i, width / 2, Colors.Black);
                image.SetPixel(height / 2, i, Colors.Black);
            }
            RpnCulculator calcul = new RpnCulculator();
            int yPrevious = -1 * (int)calcul.RpnCulculate(input, xStart) / scale;
            for (int i = xStart; i < xEnd; i++)
            {
                int x = i * scale;
                int y = -1 * (int)calcul.RpnCulculate(input, i) * scale;
                image = Setter(image, x, y, xZero, yZero);
                image = DrowLine(image, (i-1)*scale, yPrevious,x,y, xZero,yZero);
                yPrevious = y;
            }
            
                return image;
        }


        public int[] GetArray(string start, string end, string step, string scale, double width, double height)
        {
                int[] array = new int[6];
                array[0] = Convert.ToInt32(start);
                array[1] = Convert.ToInt32(end);
                array[2] = Convert.ToInt32(step);
                array[3] = Convert.ToInt32(scale);
                array[4] = Convert.ToInt32(width);
                array[5] = Convert.ToInt32(height);
                return array;
        }

        private WriteableBitmap Setter(WriteableBitmap image, int x, int y, int xZero, int yZero)
        {
            if (Math.Abs(y) < yZero - 1 && Math.Abs(x) < xZero)
            {
                    image.SetPixel(xZero + x, yZero + y, Colors.Red);
            }
            return image;
        }

        private WriteableBitmap DrowLine(WriteableBitmap image, int x1, int y1, int x2, int y2, int xZero, int yZero)
        {
            RpnCulculator calcul = new RpnCulculator();
            double k = (double)(y2- y1)/(x2-x1);
            string line = $"(x - {x1})*{k} + {y2}";
            for (int i = x1;  i < x2; i++)
            {
                int y = (int)calcul.RpnCulculate(line, i);
                Setter(image, i, y, xZero, yZero);
            }
            return image;
        }
    }
}

    

