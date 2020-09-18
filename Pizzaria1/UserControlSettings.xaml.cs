using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlSettings.xaml
    /// </summary>
    public partial class UserControlSettings : UserControl
    {
        public UserControlSettings()
        {
            InitializeComponent();

        }

        MainWindow a = new MainWindow();
        UserControlEscolha e = new UserControlEscolha();

        private void _one_Click(object sender, RoutedEventArgs e)
        {
            a.Hide();
            a.GridMenu.Background = new SolidColorBrush(Color.FromRgb(80, 113, 123));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(142, 204, 204));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(142, 204, 204));
            a.Topmenu.Background = new SolidColorBrush(Color.FromRgb(80, 113, 123));
            a.ShowDialog();
        }

        private void _two_Click(object sender, RoutedEventArgs e)
        {
            a.GridMenu.Background = new SolidColorBrush(Color.FromRgb(66, 122, 91));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(180, 205, 147));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(180, 205, 147));
            a.Topmenu.Background = new SolidColorBrush(Color.FromRgb(66, 122, 91));
            a.ShowDialog();
        }

        private void _three_Click(object sender, RoutedEventArgs e)
        {
            a.GridMenu.Background = new SolidColorBrush(Color.FromRgb(226, 67, 75));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(249, 191, 143));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(249, 191, 143));
            a.Topmenu.Background = new SolidColorBrush(Color.FromRgb(226, 67, 75));
            a.ShowDialog();
        }

        private void _four_Click(object sender, RoutedEventArgs e)
        {
            a.GridMenu.Background = new SolidColorBrush(Color.FromRgb(230, 122, 122));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(254, 206, 168));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(254, 206, 168));
            a.Topmenu.Background = new SolidColorBrush(Color.FromRgb(230, 122, 122));

            a.ShowDialog();
        }

        private void _five_Click(object sender, RoutedEventArgs e)
        {
            a.GridMenu.Background = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            a.ShowDialog();
        }
    }
}
