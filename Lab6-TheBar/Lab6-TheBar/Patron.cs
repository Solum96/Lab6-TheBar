using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Patron
    {
        Bar bar;
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

        public Patron(Bar bar)
        {
            this.bar = bar;
            Task.Run(() => 
            {
                name = nameArray[nameRandomizer.Next(0, nameArray.Length)];
                while (bar.IsOpen)
                {
                    Thread.Sleep(1000);
                    LookForChair();
                    Thread.Sleep(4000);
                    while (drinkingGlass == null) { }
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
            bar.servedPatrons.TryRemove(this.name, out Patron patron);
        }

        private void DrinkBeer()
        {
            Random rng = new Random();
            Thread.Sleep(rng.Next(10000, 30000));
        }

        private void LookForChair()
        {
            while(bar.chairs.IsEmpty) { }
            bar.chairs.TryDequeue(out seat);
        }
    }
}