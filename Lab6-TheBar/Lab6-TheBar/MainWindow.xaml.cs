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
        public MainWindow()
        {
            InitializeComponent();
            bar = new Bar(this);

            runButton.Click += RunButtonClick;
        }

        public void UpdateTimeLabel(DateTime closeTime)
        {
            Dispatcher.Invoke(() => 
            {
                timerLabel.Content = closeTime - DateTime.Now;
            });
        }

        internal void UpdateInfoLabels()
        {
            Dispatcher.Invoke(() => 
            {
                patronLabel.Content = $"Guests {bar.waitingGuests.Count + bar.servedPatrons.Count}";
                numberOfGlassLabel.Content = $"Glasses: {bar.glasses.Count}";
                numberOfChairsLabel.Content = $"Chairs: {bar.chairs.Count}";
            });
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            runButton.Content = "Close Bar";
            bar.OpenBar();
            runButton.Click -= RunButtonClick;
            runButton.Click += CloseBarClick;
        }

        private void CloseBarClick(object sender, RoutedEventArgs e)
        {
            runButton.Content = "Open Bar";
            bar.CloseBar();
            runButton.Click -= CloseBarClick;
            runButton.Click += RunButtonClick;
        }

        public void BartenderLog(string message)
        {
            Dispatcher.Invoke(() =>
            {
                bartenderLog.Items.Insert(0, DateTime.Now + message);
            });
        }
        public void WaiterLog(string message)
        {
            Dispatcher.Invoke(() =>
            {
                waiterLog.Items.Insert(0, DateTime.Now + message);
            });
        }
        public void PatronLog(string message)
        {
            Dispatcher.Invoke(() =>
            {
                patronLog.Items.Insert(0, DateTime.Now + message);
            });
        }
    }
}

