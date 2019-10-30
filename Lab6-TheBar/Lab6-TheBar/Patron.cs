using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Patron
    {
        string name;
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
            Task.Run(() => 
            {
                name = nameArray[nameRandomizer.Next(0, nameArray.Length)];
                while (bar.IsOpen)
                {
                    Thread.Sleep(1000);
                    LookForChair();
                    Thread.Sleep(4000);
                    BuyBeer();
                    LeaveBar();
                }
            });
        
        }

        private void LeaveBar()
        {
            //BuyBeer
        }

        private void BuyBeer()
        {
            //BuyBeer
        }

        private void LookForChair()
        {
            //Look for table
        }
    }
}