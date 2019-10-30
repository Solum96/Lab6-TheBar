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

        public Bartender(Bar bar)
        {
            this.bar = bar;
            Task.Run(() => 
            {
                while(bar.IsOpen)
                {
                    WaitForPatron();
                    Thread.Sleep(3000);
                    WaitForGlass();
                    Thread.Sleep(3000);
                    ServePatron();
                }
            });
        }

        private void ServePatron()
        {
            currentPatron.drinkingGlass = currentGlass;
            currentGlass = null;
            bar.servedPatrons.TryAdd(currentPatron.name, currentPatron);
        }

        private void WaitForGlass()
        {
            while (bar.glasses.IsEmpty)
            {
                Thread.Sleep(1000);
            }
            bar.glasses.TryPop(out currentGlass);
        }

        private void WaitForPatron()
        {
            while(bar.waitingGuests.IsEmpty) { Thread.Sleep(1000); }
            bar.waitingGuests.TryDequeue(out currentPatron);
        }
    }
}