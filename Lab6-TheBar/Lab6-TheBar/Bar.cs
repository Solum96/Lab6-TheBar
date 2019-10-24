using System.Collections.Concurrent;

namespace Lab6_TheBar
{
    internal class Bar
    {
        ConcurrentStack<Glass> numberOfGlass;
        ConcurrentBag<Table> numberOfTables;
        public bool isOpen { get; set; }

        public Bar(MainWindow mainWindow)
        {
        }
    }
}