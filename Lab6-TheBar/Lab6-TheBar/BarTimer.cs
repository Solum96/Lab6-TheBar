using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lab6_TheBar
{
    internal class BarTimer
    {
        Bar bar;
        MainWindow mainWindow;

        public BarTimer(Bar bar, MainWindow mainWindow)
        {
            this.bar = bar;
            this.mainWindow = mainWindow;
        }

        public void RunTimer(int openingSeconds)
        {
            int timer = openingSeconds;
            Task.Run(() =>
            {
                while (timer > 0)
                {
                    Thread.Sleep(1000 / mainWindow.Speed);
                    timer--;
                    mainWindow.UpdateTimeLabel(timer);
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