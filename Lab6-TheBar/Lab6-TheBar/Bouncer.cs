using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Bouncer
    {
        Bar bar;
        Random rng = new Random();

        public Bouncer(Bar bar)
        {
            this.bar = bar;
            Task.Run(() => 
            {
                while (bar.IsOpen)
                {
                    LetInPatron();
                }
            });
        }

        public void LetInPatron()
        {
            if(!bar.chairs.IsEmpty)
            {
                Thread.Sleep(rng.Next(3000, 10001));
                bar.waitingGuests.Enqueue(new Patron(bar));
            }
        }
    }
}