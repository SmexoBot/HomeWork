﻿using RpnLogic;
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
            RpnCulculator calcul = new RpnCulculator();
            double result = calcul.RpnCulculate(txbInput.Text, txbVariable.Text);
            lblOutput.Content = result;
        }

        private void txbInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}