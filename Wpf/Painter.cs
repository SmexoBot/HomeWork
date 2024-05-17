using RpnLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf
{
    internal class Painter
    {

        private readonly int widthAndHeight;

        public Painter(double input)
        {
            widthAndHeight = Convert.ToInt32(input);
        }

        public WriteableBitmap Paint(string input, double[] array)
        {
            double xStart = array[0];
            double xEnd = array[1];
            double step = array[2];
            double scale = array[3];
            int yMax = -widthAndHeight;
            int yMin = widthAndHeight;
            WriteableBitmap image = BitmapFactory.New(widthAndHeight, widthAndHeight);
            image = DrowSerifs(image, xStart*scale, xEnd*scale,step*scale,'x');
            for (int i = 0; i < widthAndHeight; i++)
            {
                image.SetPixel(i, widthAndHeight / 2, Colors.Black);
                image.SetPixel(widthAndHeight / 2, i, Colors.Black);
            }
            RpnCulculator calcul = new RpnCulculator(input);
            int yPrevious = 0;
            for (double i = xStart; i <= xEnd; i+= step)
            {
                double x = i * scale;
                int y = -1 * (int)Math.Round(calcul.RpnCulculate(i) * scale,MidpointRounding.ToNegativeInfinity);
                image = SetPixel(image, (int)x, y, Colors.Red);
                int xMath2 = ToUITransllate((int)x);
                int yMath2 = ToUITransllate(y);
                int xMath1 = ToUITransllate((int)((i - step) * scale));
                int yMath1 = ToUITransllate(yPrevious);
                if(i != xStart)
                {
                    image.DrawLineAa(xMath1, yMath1, xMath2, yMath2, Colors.Red);
                }
                yPrevious = y;
                yMax = (int)Math.Max(yMax, y);
                yMin = (int)Math.Min(yMin, y);
            }

            image = DrowSerifs(image, yMin, yMax, step*scale,'y');

            return image;
        }

        public double[] GetArray(string start, string end, string step, string scale)
        {
            double[] array = new double[4];
            array[0] = Convert.ToDouble(start);
            array[1] = Convert.ToDouble(end);
            array[2] = Convert.ToDouble(step);
            array[3] = Convert.ToDouble(scale);
            return array;
        }

        private int ToUITransllate(int varible)
        {
            return varible + widthAndHeight / 2;
        }

        private WriteableBitmap SetPixel(WriteableBitmap image, int x, int y, Color color)
        {
            int x1 = ToUITransllate(x);
            int y1 = ToUITransllate(y);
            int Zero = ToUITransllate(0);
            if (y != int.MaxValue && y != int.MinValue && x != int.MinValue && x != int.MaxValue)
            {
                if (Math.Abs(y) < Zero - 1 && Math.Abs(x) < Zero)
                {
                    image.SetPixel(x1, y1, color);
                }
            }
            return image;
        }

        private WriteableBitmap DrowSerifs(WriteableBitmap image, double start, double end, double step, char axis)
        {
            for (double a = start; a <= end; a += step)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (axis == 'x')
                    {
                        image = SetPixel(image, (int)a, k, Colors.Black);
                    }
                    else
                    {
                        image = SetPixel(image, k,(int)a, Colors.Black);
                    }
                }
            }
            return image;
        }
    }
}

    

