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
                    if (bar.IsOpen == false) { break; }
                }
                while (!bar.IsOpen)
                {
                    if (bar.waitingGuests.IsEmpty && bar.servedPatrons.IsEmpty)
                    {
                        bartenderWorking = false;
                        mainWindow.BartenderLog("Bartendern gick hem och grät sig till sömns.");
                        break;
                    }
                    else if (!bar.waitingGuests.IsEmpty)
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
            if (currentPatron == null) return;
            currentPatron.drinkingGlass = currentGlass;
            currentGlass = null;
            bar.servedPatrons.TryAdd(currentPatron, currentPatron);
            mainWindow.BartenderLog(" The bartender poured a beer and served the patron.");

        }

        private void WaitForGlass()
        {
            if (currentPatron == null) return;
            while (bar.glasses.IsEmpty)
            {
                Thread.Sleep(1000);
            }
            bar.glasses.TryPop(out currentGlass);
            mainWindow.BartenderLog(" The bartender took a glass from the shelf.");
        }

        private void WaitForPatron()
        {
            while(bar.waitingGuests.IsEmpty)
            {
                Thread.Sleep(1000);
                if (!bar.IsOpen) break;
            }
            bar.waitingGuests.TryDequeue(out currentPatron);
            if (currentPatron == null) { return; }
            mainWindow.BartenderLog(" The bartender is now serving a patron.");
        }
    }
}