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
            if (!bartender.bartenderWorking) return;
            mainWindow.WaiterLog(" The Waiter starts collecting glasses.");
            Thread.Sleep(10000);
            for (int i = 0; i < dirtyGlasses.Length; i++)
            {
                bar.dirtyGlasses.TryTake(out dirtyGlasses[i]);
            }
        }

        private void CleanGlass()
        {
            if (!bartender.bartenderWorking) return;
            mainWindow.WaiterLog(" Waiterboii starts washing up.");
            Thread.Sleep(15000);
            for(int i = 0; i < dirtyGlasses.Length; i++)
            {
                if (dirtyGlasses[i] != null)
                {
                    bar.glasses.Push(dirtyGlasses[i]);
                    dirtyGlasses[i] = null;
                }
            }
            mainWindow.WaiterLog(" All done!");
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
                        mainWindow.WaiterLog(" Waiter hoppar ut genom fönstret. Överlevde. Tyvärr.");
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