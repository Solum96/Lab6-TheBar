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
            Task.Run(() => 
            {
                Thread.Sleep(3000);
                new Patron(bar);
            });
        }
    }
}