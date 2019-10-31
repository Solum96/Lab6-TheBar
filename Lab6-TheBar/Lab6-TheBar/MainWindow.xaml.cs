using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab6_TheBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bar bar;
        Logger log;
        public MainWindow()
        {
            InitializeComponent();
            bar = new Bar(this);
            log = new Logger(this);

            runButton.Click += RunButtonClick;
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            runButton.IsEnabled = false;
            bar.OpenBar();
        }
    }
}
