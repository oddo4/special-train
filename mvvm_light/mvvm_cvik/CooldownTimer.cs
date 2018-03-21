using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace mvvm_cvik
{
    class CooldownTimer
    {
        public DispatcherTimer Timer;
        public int Tick;

        public CooldownTimer(MainWindowViewModel model)
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            Timer.Tick += (sender, args) => { TimerTick(model); };
        }

        private void TimerTick(MainWindowViewModel model)
        {
            model.ClickCount--;
            Tick++;
            if (model.ClickCount <= 0)
            {
                TimerStop();
                model.Info = "Game over!";
            }
            //LowerInterval();
        }

        private void LowerInterval()
        {
            if (Tick % 10 == 0)
            {
            }
        }

        public void TimerStart()
        {
            Timer.Start();
        }
        public void TimerStop()
        {
            Timer.Stop();
        }
    }
}
