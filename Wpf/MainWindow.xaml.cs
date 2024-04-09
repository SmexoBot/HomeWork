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
            int scale = Convert.ToInt32(TxBScale.Text)/10;
            int step =  Convert.ToInt32(TxBStep.Text);
            var bitmap = BitmapFactory.New(height, width);
            for (int i = 0; i < width; i++)
            {
                bitmap.SetPixel(i, width / 2, Colors.Black);
                bitmap.SetPixel(height / 2, i, Colors.Black);
            }
            ImGraph.Source = bitmap;
            RpnCulculator calcul = new RpnCulculator();
            for (int i = scale*Convert.ToInt32(TxBStart.Text); i < scale*Convert.ToInt32(TxBEnd.Text); i+= step)
            {
                int y = -1*(int)calcul.RpnCulculate(TxBInput.Text, i);
                bitmap.SetPixel(xZero+ i,yZero +y,Colors.Red);
            }
            ImGraph.Source = bitmap;
        }

        private void txbInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}