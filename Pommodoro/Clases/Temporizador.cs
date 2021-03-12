using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Pommodoro.Clases
{
    public class Temporizador
    {
        private long ticks;
        public Timer timer { get; }

        public long Minutos//Setter transforma el valor introducido en minutos en el valor en ticks (1s = 1000ticks, 1min = 60000ticks)
        {
            get { return ticks; } 
            set
            {
                ticks = value * 60000;
            }
        }

        //Constructor
        public Temporizador()
        {
            timer = new Timer();
            timer.AutoReset = false;
            timer.Enabled = false;
            
        }

        public void Start()
        {
            timer.Interval = ticks;
            timer.Enabled = true;
        }

        public void Stop()
        {
            timer.Enabled = false;
        }

        public void SetFunc(Action method)
        {
            timer.Elapsed += (s, e) => { method?.Invoke(); };
        }

    }
}
