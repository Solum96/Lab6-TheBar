using System;
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
                while (bar.isOpen)
                {
                    LetInPatron();
                }
            });
        }

        public void LetInPatron()
        {
            Thread.Sleep(rng.Next(3000, 10001));
            new Patron(bar);
        }
    }
}