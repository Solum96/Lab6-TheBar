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
        Bartender bartender;
        public Waiter(Bar bar, MainWindow mainWindow, Bartender bartender)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
            this.bartender = bartender;
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
                    Thread.Sleep(15000);
                    bar.glasses.Push(dirtyGlasses[i]);
                    dirtyGlasses[i] = null;
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
                    if (bar.IsOpen == false) { break; }

                }
                while (!bar.IsOpen)
                {
                    if (!bartender.bartenderWorking)
                    {
                        waiterWorking = false;
                        mainWindow.WaiterLog("Waiter hoppar ut genom fönstret. Överlevde. Tyvärr.");
                        break;
                    }
                    else if (!bar.dirtyGlasses.IsEmpty)
                    {    
                        CollectGlass();
                        CleanGlass();   
                    }
                }
            });
        }
    }
}