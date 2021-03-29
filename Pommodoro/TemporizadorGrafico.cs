using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using System.Windows.Controls;


namespace Pomodoro
{
    /// <summary>
    /// Actualiza texto de elementos graficos cada 1 segundo.
    /// </summary>
    public class TemporizadorGrafico
    {
        private DispatcherTimer timer;

        private protected TextBlock tbHora;
        private protected TextBlock tbMinuto;
        private protected TextBlock tbSegundo;
        private protected int hora, minuto, segundo;


        //Propiedades

        public TextBlock TextBoxHora { get { return tbHora; } set { tbHora = value; } }
        public TextBlock TextBoxMinuto { get { return tbMinuto; } set { tbMinuto = value; } }
        public TextBlock TextBoxSegundo { get { return tbSegundo; } set { tbSegundo = value; } }

        public int Hora
        {
            get { return hora; }
            set
            {
                if (value >= 0 && value < 100) { hora = value; }
                else { throw new ArgumentException("El valor introducido debe ser mayor a 0 y menor que 99"); }
            }
        }

        public int Minuto
        {
            get { return minuto; }
            set
            {
                if (value >= 0 && value <= 60) { minuto = value; }
                else { throw new ArgumentException("El valor introducido debe ser mayor a 0 y menor o igual a 60"); }
            }
        }
        public int Segundo
        {
            get { return segundo; }
            set
            {
                if (value >= 0) { segundo = value; }
                else { throw new ArgumentException("El valor introducido debe ser mayor o igual a 0 y menor o igual a 60"); }
            }
        }


        //Constructores
        public TemporizadorGrafico()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Tick += Timer_tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            hora = 0;
            minuto = 0;
            segundo = 0;

        }


        //Metodos privados

        public void StartTemporizador()
        {
            SetSegundoTextBoxTxt();
            SetMinutoTextBoxTxt();
            SetHoraTextBoxTxt();

            timer.Start();

        }
        public void StopTemporizador()
        {
            timer.Stop();
        }
        
        public void Reanudar()
        {
            timer.Start();
        }

        public bool IsEnabled()
        {
            return timer.IsEnabled;
        }


        //Metodos privados
        private void Timer_tick(object sender, EventArgs e)
        {
            if(segundo > 0)
            {
                segundo--;
                SetSegundoTextBoxTxt();
                if (segundo < 0 && minuto == 0 && hora == 0) StopTemporizador();
            }
            else if(segundo == 0 && minuto > 0)
            {
                minuto--;
                segundo = 59;

                SetMinutoTextBoxTxt();
                SetSegundoTextBoxTxt();    
            }
            else if(segundo == 0 && minuto == 0 && hora >= 1)
            {
                hora--;
                minuto = 59;
                segundo = 59;

                SetHoraTextBoxTxt();
                SetMinutoTextBoxTxt();
                SetSegundoTextBoxTxt();
            }
        }

        private bool EsTiempoValido() => hora != 0 && minuto != 0 && segundo != 0;

        private void SetSegundoTextBoxTxt()
        {
            if (segundo >= 10) tbSegundo.Text = segundo.ToString();
            else tbSegundo.Text = "0" + segundo.ToString();
        }

        private void SetMinutoTextBoxTxt()
        {
            if (minuto >= 10) tbMinuto.Text = minuto.ToString();
            else tbMinuto.Text = "0" + minuto.ToString();
        }

        private void SetHoraTextBoxTxt()
        {
            if (hora >= 10) tbHora.Text = hora.ToString();
            else tbHora.Text = "0" + hora.ToString();
        }
    }
}
