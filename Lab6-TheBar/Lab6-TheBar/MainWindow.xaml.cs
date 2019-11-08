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
        public enum Presets { Default, LotsOfGlass, LotsOfTables, GuestsStayLong, SuperWaiter, AllNightBar, CouplesNight, BusLoad };
        public Presets SelectedOption = Presets.Default;

        int speed = 1;
        public int Speed { get { return speed; } private set { speed = value; } }

        public MainWindow()
        {
            InitializeComponent();
            optionsMenu.ItemsSource = Enum.GetValues(typeof(Presets)).Cast<Presets>();

            bar = new Bar(this);

            runButton.Click += RunButtonClick;
            speedSlider.ValueChanged += SpeedSliderValueChanged;
        }

        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => 
            {
                bartenderLog.Items.Clear();
                waiterLog.Items.Clear();
                patronLog.Items.Clear();
            });
        }

        private void SpeedSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speed = Convert.ToInt32(speedSlider.Value);
            speedLabel.Content = $"Speed: {Convert.ToInt32(speedSlider.Value)}";
        }

        public void UpdateTimeLabel(int closeTime)
        {
            Dispatcher.Invoke(() => 
            {
                timerLabel.Content = closeTime;
            });
        }

        internal void UpdateInfoLabels()
        {
            Dispatcher.Invoke(() => 
            {
                numberOfPatronLabel.Content = $"Guests {bar.waitingGuests.Count + bar.servedPatrons.Count}";
                numberOfGlassLabel.Content = $"Glasses: {bar.glasses.Count}";
                numberOfChairsLabel.Content = $"Chairs: {bar.chairs.Count}";
            });
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            runButton.Content = "Close Bar";
            optionsMenu.IsEnabled = false;
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

        private void OptionsMenuSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (optionsMenu.SelectedItem)
            {
                case Presets.Default:
                    SelectedOption = Presets.Default;
                    break;

                case Presets.LotsOfGlass:
                    SelectedOption = Presets.LotsOfGlass;
                    break;

                case Presets.LotsOfTables:
                    SelectedOption = Presets.LotsOfTables;
                    break;

                case Presets.GuestsStayLong:
                    SelectedOption = Presets.GuestsStayLong;
                    break;

                case Presets.SuperWaiter:
                    SelectedOption = Presets.SuperWaiter;
                    break;

                case Presets.AllNightBar:
                    SelectedOption = Presets.AllNightBar;
                    break;

                case Presets.CouplesNight:
                    SelectedOption = Presets.CouplesNight;
                    break;

                case Presets.BusLoad:
                    SelectedOption = Presets.BusLoad;
                    break;

                default:
                    break;
            }
        }

    }
}

