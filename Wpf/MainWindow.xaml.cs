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
            Painter painter = new Painter(ImGraph.Width);
            double[] arrayInput = painter.GetArray(TxBStart.Text, TxBEnd.Text, TxBStep.Text, TxBScale.Text);
            WriteableBitmap bitmap = painter.Paint(TxBInput.Text, arrayInput); 
            ImGraph.Source = bitmap;
        }
    }
 }
