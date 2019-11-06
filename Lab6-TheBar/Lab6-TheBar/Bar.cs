﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab6_TheBar
{
    internal class Bar
    {
        MainWindow mainWindow;
        Bouncer bouncer;
        Bartender bartender;
        Waiter waiter;
        int numberOfGlass = 8;
        int numberOfChairs = 9;
        public const int guestCapacity = 50;
        public ConcurrentStack<Glass> glasses = new ConcurrentStack<Glass>();
        public ConcurrentBag<Glass> dirtyGlasses = new ConcurrentBag<Glass>();
        public ConcurrentQueue<Chair> chairs = new ConcurrentQueue<Chair>();
        public ConcurrentQueue<Patron> waitingGuests = new ConcurrentQueue<Patron>();
        public ConcurrentDictionary<Patron, Patron> servedPatrons = new ConcurrentDictionary<Patron, Patron>();
        
        public bool waiterWorking { get; set; }
        public bool IsOpen { get; set; }
        public BarTimer openingTimer { get; set; }

        public Bar(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
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
            this.IsOpen = true;
            this.openingTimer = new BarTimer(this, mainWindow);
            this.bouncer = new Bouncer(this, mainWindow);
            this.bartender = new Bartender(this, mainWindow);
            this.waiter = new Waiter(this, mainWindow, bartender);

            bartender.Work();
            bouncer.Work();
            waiter.Work();
            openingTimer.RunTimer(120);
        }
        public void CloseBar()
        {
            this.IsOpen = false;
        }
    }
}