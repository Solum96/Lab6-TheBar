using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Bouncer
    {
        MainWindow mainWindow;
        Bar bar;
        Random rng = new Random();

        public Bouncer(Bar bar, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
        }

        public void LetInPatron()
        {
            if(!bar.chairs.IsEmpty)
            {
                Thread.Sleep(rng.Next(3000, 10001));
                if (bar.IsOpen)
                {
                    bar.waitingGuests.Enqueue(new Patron(bar, mainWindow));
                }
            }
        }

        internal void Work()
        {
            Task.Run(() =>
            {
                while (bar.IsOpen)
                {
                    LetInPatron();
                    if (!bar.IsOpen) { break; }
                }
                mainWindow.PatronLog("Bouncern ragequitta");
            });
        }
    }
}