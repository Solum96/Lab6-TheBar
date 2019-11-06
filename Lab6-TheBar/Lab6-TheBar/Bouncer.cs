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
                if (bar.IsOpen)
                {
                    Thread.Sleep(rng.Next(3000, 10000));
                    bar.waitingGuests.Enqueue(new Patron(bar, mainWindow));
                }
            }
        }

        internal void Work()
        {
            Task.Run(() =>
            {
                // FOR TESTING:
                // Task.Run(() => 
                // {
                //     Thread.Sleep(20000);
                //     for (int i = 0; i < 15; i++)
                //     {
                //         bar.waitingGuests.Enqueue(new Patron(bar, mainWindow));
                //     }
                //     mainWindow.PatronLog("Oh shit, en busslast med idioter!");
                // });
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