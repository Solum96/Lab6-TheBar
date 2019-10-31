using System.Collections.ObjectModel;

namespace Lab6_TheBar
{
    internal class Logger
    {
        MainWindow mainWindow;

        public static ObservableCollection<string> bartenderList;
        public static ObservableCollection<string> waiterList;
        public static ObservableCollection<string> patronList;

        public Logger(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            mainWindow.bartenderLog.ItemsSource = bartenderList;
            mainWindow.waiterLog.ItemsSource = waiterList;
            mainWindow.patronLog.ItemsSource = patronList;
        }

    }
}