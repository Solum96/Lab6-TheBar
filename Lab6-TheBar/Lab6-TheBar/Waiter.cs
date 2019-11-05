using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Waiter
    {
        Bar bar;
        MainWindow mainWindow;
        Glass[] dirtyGlasses = new Glass[6];
        public bool waiterWorking { get; set; }
        public Waiter(Bar bar, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
        }


        private void CollectGlass()
        {
            mainWindow.WaiterLog("The Waiter starts collecting glasses.");
            for (int i = 0; i < dirtyGlasses.Length; i++)
            {
                bar.dirtyGlasses.TryTake(out dirtyGlasses[i]);
            }
            Thread.Sleep(10000);
        }

        private void CleanGlass()
        {
            mainWindow.WaiterLog("Waiterboii starts washing up.");
            for(int i = 0; i < dirtyGlasses.Length; i++)
            {
                if(dirtyGlasses[i] != null)
                {
                    bar.glasses.Push(dirtyGlasses[i]);
                    dirtyGlasses[i] = null;
                    Thread.Sleep(15000);
                }
            }
            mainWindow.WaiterLog("All done!");
        }

        internal void Work()
        {
            Task.Run(() =>
            {
                while (bar.IsOpen)
                {
                    if (!bar.dirtyGlasses.IsEmpty)
                    {
                        CollectGlass();
                        CleanGlass();
                    }
                    Thread.Sleep(100);
                }
                while (!bar.IsOpen)
                {
                    if (bar.waitingGuests.IsEmpty && bar.chairs.IsEmpty)
                    {
                        waiterWorking = false;
                    }
                    else
                    {
                        CollectGlass();
                        CleanGlass();
                    }
                }
            });
        }
    }
}