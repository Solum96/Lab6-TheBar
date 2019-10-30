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
        ConcurrentQueue<Patron> patronQueue = new ConcurrentQueue<Patron>();

        public Bouncer(Bar bar)
        {
            this.bar = bar;
            while (bar.IsOpen)
            {
                Task.Run(() => 
                {
                    Thread.Sleep(rng.Next(2000, 5000));
                    patronQueue.Enqueue(new Patron(bar));
                });
                Task.Run(() => 
                {
                    if(!patronQueue.IsEmpty) LetInPatron();
                });
            }
        }

        public void LetInPatron()
        {
            if(bar.waitingGuests.Count + bar.servedPatrons.Count < Bar.guestCapacity)
            {
                Patron temp;
                patronQueue.TryDequeue(out temp);
                Thread.Sleep(rng.Next(3000, 10001));
                bar.waitingGuests.Enqueue(temp);
            }
        }
    }
}