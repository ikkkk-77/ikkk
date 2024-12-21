using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using Wpf页面设计_Prism_WebAPI_.ViewModel;

namespace Wpf页面设计_Prism_WebAPI_.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 鼠标放大放小关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        // 点击最小化事件
        private void btn_Min(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // 点击最大化最小化事件
        private void btn_MinMax(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Maximized;
            }
            else WindowState = WindowState.Minimized;
        }

        // 点击关闭事件
        private void btn_close(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // 鼠标双击事件
        private void ColorZone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Maximized;
            }
            else WindowState = WindowState.Minimized;
        }

        // 左侧菜单点击事件
        private void lbMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 菜单栏关闭
            drawerhost.IsLeftDrawerOpen = false;
        }
    }
}
