using RpnLogic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int width = (int)ImGraph.Width;
            int height = (int)ImGraph.Height;
            int xZero = width / 2;
            int yZero = height / 2;
            int scale = Convert.ToInt32(TxBScale.Text);
            int step = Convert.ToInt32(TxBStep.Text);
            int xStart = Convert.ToInt32(TxBStart.Text);
            int xEnd = Convert.ToInt32(TxBEnd.Text);
            var bitmap = BitmapFactory.New(height, width);
            for (int i = 0; i < width; i++)
            {
                bitmap.SetPixel(i, width / 2, Colors.Black);
                bitmap.SetPixel(height / 2, i, Colors.Black);
            }
            RpnCulculator calcul = new RpnCulculator();
            int xPrevious = xStart * scale;
            int yPrevious =-1*scale* (int)calcul.RpnCulculate(TxBInput.Text, xPrevious);
            for (int i = xStart; i < xEnd; i++)
            {
                int x = i * scale;
                int y = -1 * scale * (int)calcul.RpnCulculate(TxBInput.Text, i);

                if (Math.Abs(y) < yZero - 1 && Math.Abs(x) < xZero)
                {
                    bitmap.SetPixel(xZero + x, yZero + y, Colors.Red);
                }
                else
                {
                    continue;
                }
                if (y == yPrevious)
                {
                    continue;
                }
                /*string lines = $"(x-{xPrevious})*({y}-{yPrevious})/({x}-{xPrevious}) +{yPrevious}";
                for (int l = xPrevious; l < x; l++)
                {
                    for (int j = 0; j < scale; j++)
                    {
                        double lD = l + (double)(j / scale);
                        int y1 =   scale * (int)Math.Round(calcul.RpnCulculate(lines, lD), MidpointRounding.ToNegativeInfinity);
                        if (Math.Abs(y1) < yZero && Math.Abs(l) < xZero)
                        {
                            bitmap.SetPixel(xZero + l, yZero + y1, Colors.Red);
                        }
                        else
                        {
                            break;
                        }
                    }
                */
                }
                xPrevious = x;
                yPrevious = y;
               
            }
            ImGraph.Source = bitmap;
        }
    

         private void txbInput_TextChanged(object sender, TextChangedEventArgs e)
         {

         }
    }
 }
