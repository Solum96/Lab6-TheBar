using System.Collections.ObjectModel;

namespace Lab6_TheBar
{
    internal class Logger
    {
        MainWindow mainWindow;

        public static ObservableCollection<string> bartenderList = new ObservableCollection<string>();
        public static ObservableCollection<string> waiterList = new ObservableCollection<string>();
        public static ObservableCollection<string> patronList = new ObservableCollection<string>();

        public Logger(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            mainWindow.bartenderLog.ItemsSource = bartenderList;
            mainWindow.waiterLog.ItemsSource = waiterList;
            mainWindow.patronLog.ItemsSource = patronList;
        }

    }
}