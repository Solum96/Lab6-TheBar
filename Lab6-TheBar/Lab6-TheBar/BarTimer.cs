using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lab6_TheBar
{
    internal class BarTimer
    {
        DateTime startTime;
        DateTime closeTime;
        Bar bar;
        MainWindow mainWindow;

        public BarTimer(Bar bar, MainWindow mainWindow)
        {
            this.bar = bar;
            this.mainWindow = mainWindow;
        }

        public void RunTimer(double openingSeconds)
        {
            this.startTime = DateTime.Now;
            this.closeTime = startTime.AddSeconds(openingSeconds);
            Task.Run(() =>
            {
                while (DateTime.Now < closeTime)
                {
                    Thread.Sleep(100);
                    mainWindow.UpdateTimeLabel(closeTime);
                }
                bar.CloseBar();
            });
            Task.Run(() => 
            {
                while (true)
                {
                    Thread.Sleep(100);
                    mainWindow.UpdateInfoLabels();
                }
            });
        }
    }
}