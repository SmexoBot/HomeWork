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
        public WriteableBitmap Paint(string input, double[] array)
        {
            double xStart = array[0];
            double xEnd = array[1];
            double step = array[2];
            double scale = array[3];
            int width = (int)array[4];
            int height = (int)array[5];
            int xZero = (int)width / 2;
            int yZero = (int)height / 2;
            int yMax = -width;
            int yMin = height;
            WriteableBitmap image = BitmapFactory.New(width, height);
            for (double a = xStart*scale; a <= xEnd*scale; a += step*scale)
            {
                for(int k = -3; k <= 3; k++)
                {
                    image = Setter(image, (int)a, k, xZero, yZero, Colors.Black);
                }
            }
            for (int i = 0; i < width; i++)
            {
                image.SetPixel(i, width / 2, Colors.Black);
                image.SetPixel(height / 2, i, Colors.Black);
            }
            RpnCulculator calcul = new RpnCulculator(input);
            for (double i = xStart; i < xEnd; i+= step)
            {
                double x = i * scale;
                int y = -1 * (int)Math.Round(calcul.RpnCulculate(i) * scale,MidpointRounding.ToNegativeInfinity);
                image = Setter(image, (int)x, y, xZero, yZero , Colors.Red);
                if (y > yMax)
                {
                    yMax = y;
                }
                else if (y < yMin)
                {
                    yMin = y;
                }
            }

            for(double a = yMin; a < yMax; a+= step * scale)
            {
                for(int k = -3; k <= 3; k++)
                {
                    image = Setter(image, (int)k, (int)a, xZero, yZero, Colors.Black);
                }
            }

            return image;
        }


        public double[] GetArray(string start, string end, string step, string scale, double width, double height)
        {
            double[] array = new double[6];
            array[0] = Convert.ToDouble(start);
            array[1] = Convert.ToDouble(end);
            array[2] = Convert.ToDouble(step);
            array[3] = Convert.ToDouble(scale);
            array[4] = Convert.ToDouble(width);
            array[5] = Convert.ToDouble(height);
            return array;
        }

        private WriteableBitmap Setter(WriteableBitmap image, int x, int y, int xZero, int yZero, Color color)
        {
            if (Math.Abs(y) < yZero - 1 && Math.Abs(x) < xZero)
            {
                image.SetPixel(xZero + x, yZero + y, color);
            }
            return image;
        }

        
    }
}

    

