using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6_TheBar
{
    internal class Patron
    {
        public Patron(Bar bar)
        {
            
            
            Task.Run(() => 
            {
                while (bar.isOpen)
                {
                    LookForTable();
                    Thread.Sleep(5000);
                }
            });
        
        }

        private void LookForTable()
        {
            //Look for table
        }
    }
}