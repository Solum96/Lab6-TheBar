using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lab6_TheBar
{
    internal class Patron
    {
        Bar bar;
        MainWindow mainWindow;
        public Glass drinkingGlass;
        public Chair seat;
        public string name { get; set; }
        string[] nameArray = 
        {
            "Bengt",
            "Bertil",
            "Bosse",
            "Benjamin",
            "Benny",
            "Bodil",
            "Brosef",
            "Banders",
            "Bemil",
            "Bils"
        };
        Random nameRandomizer = new Random();

        public Patron(Bar bar, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
            Task.Run(() => 
            {
                name = nameArray[nameRandomizer.Next(0, nameArray.Length)];
                mainWindow.PatronLog($"{this.name} entered Ye Ol' Crusty Sock");
                while (bar.IsOpen)
                {
                    Thread.Sleep(1000);
                    LookForChair();
                    while (drinkingGlass == null) { Thread.Sleep(100); }
                    DrinkBeer();
                    LeaveBar();
                }
            });
        }

        private void LeaveBar()
        {
            bar.dirtyGlasses.Add(drinkingGlass);
            drinkingGlass = null;
            bar.chairs.Enqueue(seat);
            seat = null;
            mainWindow.PatronLog($"{this.name} leaves the Bar.");
            bar.servedPatrons.TryRemove(this.name, out Patron patron);
        }

        private void DrinkBeer()
        {
            mainWindow.PatronLog($"{this.name} buys a drink and starts chugging.");
            Random rng = new Random();
            Thread.Sleep(rng.Next(10000, 30000));
        }

        private void LookForChair()
        {
            while (bar.chairs.IsEmpty) { Thread.Sleep(100); }
            Thread.Sleep(4000);
            bar.chairs.TryDequeue(out seat);
            mainWindow.PatronLog($"{this.name} found a chair!");
        }
    }
}