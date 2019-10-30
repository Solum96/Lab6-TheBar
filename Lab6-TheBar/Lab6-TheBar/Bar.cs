using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab6_TheBar
{
    internal class Bar
    {
        Bouncer bouncer;
        Bartender bartender;
        int numberOfGlass = 8;
        int numberOfChairs = 9;
        public const int guestCapacity = 50;
        public ConcurrentStack<Glass> glasses = new ConcurrentStack<Glass>();
        public ConcurrentQueue<Chair> chairs = new ConcurrentQueue<Chair>();
        public ConcurrentQueue<Patron> waitingGuests = new ConcurrentQueue<Patron>();
        public ConcurrentBag<Patron> servedPatrons = new ConcurrentBag<Patron>();
        public bool IsOpen { get; set; }

        public Bar(MainWindow mainWindow)
        {
            for (int i = 0; i < numberOfGlass; i++)
            {
                glasses.Push(new Glass());
            }
            for (int i = 0; i < numberOfChairs; i++)
            {
                chairs.Enqueue(new Chair());
            }
        }

        public void OpenBar()
        {
            Task.Run(() => 
            {
                this.IsOpen = true;
                this.bouncer = new Bouncer(this);
                this.bartender = new Bartender(this);
                Thread.Sleep(120 * 1000);
                CloseBar();
            });
        }
        public void CloseBar()
        {
            this.IsOpen = false;
        }
    }
}