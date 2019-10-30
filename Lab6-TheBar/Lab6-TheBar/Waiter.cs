using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Waiter
    {
        Bar bar;
        Glass[] dirtyGlasses = new Glass[6];
        public Waiter(Bar bar)
        {
            this.bar = bar;
            Task.Run(() => 
            {
                while (bar.IsOpen)
                {
                    if (!bar.dirtyGlasses.IsEmpty)
                    {
                        CollectGlass();
                        CleanGlass();
                    }
                }
            });
        }

        private void CollectGlass()
        {
            for(int i = 0; i < dirtyGlasses.Length; i++)
            {
                bar.dirtyGlasses.TryTake(out dirtyGlasses[i]);
            }
        }

        private void CleanGlass()
        {
            for(int i = 0; i < dirtyGlasses.Length; i++)
            {
                if(dirtyGlasses[i] != null)
                {
                    Thread.Sleep(15000);
                    bar.glasses.Push(dirtyGlasses[i]);
                    dirtyGlasses[i] = null;
                }
            }
        }
    }
}