using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Pomodoro.Clases
{
    public class Temporizador
    {
        private long ticks;//Setter transforma el valor introducido en su valor en ticks (1s = 1000ticks, 1min = 60000ticks, 1h = 3600000)
        private int horas, minutos, segundos;
        private Timer timer { get; }

        public int Segundos
        {
            get { return segundos; }
            set 
            { 
                if(value >= 0) { segundos = value; }
                else { throw new ArgumentOutOfRangeException("Solo se admiten valores mayores a 0"); }
            }
        }
        public int Minutos
        {
            get { return minutos; }
            set
            {
                if (value >= 0) { minutos = value; }
                else { throw new ArgumentOutOfRangeException("Solo se admiten valores mayores a 0"); }
            }
        }
        public int Horas
        {
            get { return horas; }
            set
            {
                if (value >= 0) { horas = value; }
                else { throw new ArgumentOutOfRangeException("Solo se admiten valores mayores a 0"); }
            }
        }
        //Constructor
        public Temporizador()
        {
            timer = new Timer();
            timer.AutoReset = false;
            timer.Enabled = false;
            this.horas = 0;
            this.minutos = 0;
            this.segundos = 0;
            
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

        public bool Enabled()
        {
            return timer.Enabled;
        }
        public void SetFunc(Action method)
        {
            timer.Elapsed += (s, e) => { method?.Invoke(); };
        }

        private long SetTicks()
        {
            return horas * 3600000 + minutos * 60000 + segundos * 1000;
        }
    }
}
