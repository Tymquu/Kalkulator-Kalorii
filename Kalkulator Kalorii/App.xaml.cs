using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kalkulator_Kalorii
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AdjustWindowSize(sender);
        //}
        //private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    (sender as Window).WindowState = WindowState.Minimized;
        //}

        //private void AdjustWindowSize(object sender)
        //{
        //    //(sender as App).Con
        //    if ((sender as Window).WindowState == WindowState.Maximized)
        //    {
        //        (sender as Window).WindowState = WindowState.Normal;
        //        (sender as Button).Content = "◰";
        //    }
        //    else
        //    {
        //        (sender as Window).WindowState = WindowState.Maximized;
        //        (sender as Button).Content = "□";
        //    }

        //}


    }
}
