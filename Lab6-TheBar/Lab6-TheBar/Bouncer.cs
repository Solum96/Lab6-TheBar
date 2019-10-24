using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Bouncer
    {
        Bar bar;

        public Bouncer(Bar bar)
        {
            this.bar = bar;
        }

        public void LetInPatron()
        {
            Random rng = new Random();
            while (bar.isOpen)
            {
                Task.Run(() => 
                {
                    Thread.Sleep(rng.Next(3000, 10001));
                    new Patron(bar);
                });
            }
        }
    }
}