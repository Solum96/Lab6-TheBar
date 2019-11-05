using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Bartender
    {
        Patron currentPatron;
        Glass currentGlass;
        Bar bar;
        MainWindow mainWindow;
        public bool bartenderWorking { get; set; }

        public Bartender(Bar bar, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
        }

        public void Work()
        {
            Task.Run(() =>
            {
                while (bar.IsOpen)
                {
                    WaitForPatron();
                    Thread.Sleep(3000);
                    WaitForGlass();
                    Thread.Sleep(3000);
                    ServePatron();
                }
                while (!bar.IsOpen)
                {
                    if (bar.waitingGuests.IsEmpty && bar.chairs.IsEmpty)
                    {
                        bartenderWorking = false;
                    }
                    else
                    {
                        WaitForPatron();
                        Thread.Sleep(3000);
                        WaitForGlass();
                        Thread.Sleep(3000);
                        ServePatron();
                    }
                }
            });
        }

        private void ServePatron()
        {
            currentPatron.drinkingGlass = currentGlass;
            currentGlass = null;
            bar.servedPatrons.TryAdd(currentPatron.name, currentPatron);
            mainWindow.BartenderLog("The bartender poured a beer and served the patron.");

        }

        private void WaitForGlass()
        {
            while (bar.glasses.IsEmpty)
            {
                Thread.Sleep(1000);
            }
            bar.glasses.TryPop(out currentGlass);
            mainWindow.BartenderLog("The bartender took a glass from the shelf.");
        }

        private void WaitForPatron()
        {
            while(bar.waitingGuests.IsEmpty) { Thread.Sleep(1000); }
            bar.waitingGuests.TryDequeue(out currentPatron);
            mainWindow.BartenderLog("The bartender is now serving a patron.");
        }
    }
}