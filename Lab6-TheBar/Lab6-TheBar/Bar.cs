using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Lab6_TheBar
{
    internal class Bar
    {
        int numberOfGlass = 8;
        int numberOfChairs = 9;
        ConcurrentStack<Glass> glasses;
        ConcurrentQueue<Chair> chairs;
        public bool isOpen { get; set; }

        public Bar(MainWindow mainWindow)
        {
            for (int i = 0; i < numberOfGlass; i++)
            {
                glasses.Push(new Glass());
            }
            for (int i = 0; i < numberOfChairs; i++)
            {
                chairs.Enqueue(new Chair());
            }
        }

        public void OpenBar()
        {
            this.isOpen = true;
        }
        public void CloseBar()
        {
            this.isOpen = false;
        }
    }
}