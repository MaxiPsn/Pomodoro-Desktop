using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Pomodoro.Clases
{
    public class Temporizador
    {
        private long ticks;//Setter transforma el valor introducido en su valor en ticks (1s = 1000ticks, 1min = 60000ticks, 1h = 3600000)
        private Timer timer { get; }

        public int Segundos
        {
            get { return Segundos; }
            set 
            { 
                if(value >= 0) { Segundos = value; }
                else { throw new ArgumentOutOfRangeException("Solo se admiten valores mayores a 0"); }
            }
        }
        public int Minutos
        {
            get { return Minutos; }
            set
            {
                if (value >= 0) { Minutos = value; }
                else { throw new ArgumentOutOfRangeException("Solo se admiten valores mayores a 0"); }
            }
        }
        public int Horas
        {
            get { return Horas; }
            set
            {
                if (value >= 0) { Horas = value; }
                else { throw new ArgumentOutOfRangeException("Solo se admiten valores mayores a 0"); }
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
            ticks = SetTicks();
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

        private long SetTicks()
        {
            return Horas * 3600000 + Minutos * 60000 + Segundos * 1000;
        }
    }
}
