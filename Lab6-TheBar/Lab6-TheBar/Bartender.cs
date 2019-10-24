using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Bartender
    {
        Patron currentPatron;
        Glass currentGlass;
        Bar bar;

        public Bartender(Bar bar)
        {
            this.bar = bar;
            Task.Run(() => 
            {
                while(bar.isOpen)
                {
                    WaitForPatron();
                    Thread.Sleep(3000);
                    WaitForGlass();
                    Thread.Sleep(3000);
                    ServePatron();
                }
            });
        }

        private void ServePatron()
        {
            //TODO: Serve Patron
        }

        private void WaitForGlass()
        {
            //TODO: Wait for glass
        }

        private void WaitForPatron()
        {
            // TODO: Wait for patron
        }
    }
}