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
        MainWindow.Presets SelectedOption;

        public Bouncer(Bar bar, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
            this.SelectedOption = mainWindow.SelectedOption;
        }

        public void LetInPatron()
        {
            if(!bar.chairs.IsEmpty)
            {
                if (bar.IsOpen)
                {
                    if (SelectedOption == MainWindow.Presets.BusLoad) Thread.Sleep(rng.Next(6000, 20000));
                    else Thread.Sleep(rng.Next(3000, 10000));

                    bar.waitingGuests.Enqueue(new Patron(bar, mainWindow));
                    if(SelectedOption == MainWindow.Presets.CouplesNight)
                    {
                        bar.waitingGuests.Enqueue(new Patron(bar, mainWindow));
                    }
                }
            }
        }

        internal void Work()
        {
            Task.Run(() =>
            {
                if(SelectedOption == MainWindow.Presets.BusLoad)
                {
                    Task.Run(() => 
                    {
                        Thread.Sleep(20000);
                        for (int i = 0; i < 15; i++)
                        {
                            bar.waitingGuests.Enqueue(new Patron(bar, mainWindow));
                        }
                        mainWindow.PatronLog("Oh shit, en busslast med idioter!");
                    });
                }
                while (bar.IsOpen)
                {
                    LetInPatron();
                    if (!bar.IsOpen) { break; }
                }
                mainWindow.PatronLog(" Bouncern ragequitta");
            });
        }
    }
}