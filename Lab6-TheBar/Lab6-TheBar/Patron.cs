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
        MainWindow.Presets SelectedOption;
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
            "Banders",
            "Bemil",
            "Bils",
            "Batlan",
            "Brosef"
        };
        Random nameRandomizer = new Random();

        public Patron(Bar bar, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.bar = bar;
            this.SelectedOption = mainWindow.SelectedOption;
            EnterBar(bar, mainWindow);
        }

        private void EnterBar(Bar bar, MainWindow mainWindow)
        {
            Task.Run(() =>
            {
                name = nameArray[nameRandomizer.Next(0, nameArray.Length)];
                mainWindow.PatronLog($" {this.name} entered Ye Ol' Crusty Sock");
                
             
                Thread.Sleep(1000 / mainWindow.Speed);
                LookForChair();
                while (drinkingGlass == null) { Thread.Sleep(100); }
                DrinkBeer();
                LeaveBar();
             
            });
        }

        private void LeaveBar()
        {
            bar.dirtyGlasses.Add(drinkingGlass);
            drinkingGlass = null;
            if(seat != null) bar.chairs.Enqueue(seat);
            seat = null;
            mainWindow.PatronLog($" {this.name} leaves the Bar.");
            bar.servedPatrons.TryRemove(this, out Patron patron);
        }

        private void DrinkBeer()
        {
            mainWindow.PatronLog($" {this.name} buys a drink and starts chugging.");
            Random rng = new Random();
            if (SelectedOption == MainWindow.Presets.GuestsStayLong)
            {
                Thread.Sleep(rng.Next(20000 / mainWindow.Speed, 60000 / mainWindow.Speed));
            }
            else
            {
                Thread.Sleep(rng.Next(10000 / mainWindow.Speed, 30000 / mainWindow.Speed));
            }
        }

        private void LookForChair()
        {
            Thread.Sleep(4000 / mainWindow.Speed);
            while (bar.chairs.IsEmpty) { Thread.Sleep(100); }
            bar.chairs.TryDequeue(out seat);
            mainWindow.PatronLog($" {this.name} found a chair!");
        }
    }
}