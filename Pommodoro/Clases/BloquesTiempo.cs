using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Pomodoro.Clases
{
    enum Estado {Espera,Productivo,Descanso,Pausa};

    ///Contiene los pares de tiempo productivo y de descanso, un contador de bloques terminados y las acciones a realizar.
    public class BloquesTiempo : INotifyPropertyChanged
    {

        private static int bloquesCumplidos;
        private int tiempoProductivo;
        private int tiempoDescanso;
        private int estado;

        public event PropertyChangedEventHandler PropertyChanged;

        //Propiedades

        public int BloquesCumplidos {
            get { return bloquesCumplidos; } 
            protected set 
            { 
                bloquesCumplidos = value;
                OnPropertyChange();
            } 
        }
        public bool ProductivoCumplido { get; set; }
        public bool DescansoCumplido { get; set; }
        public int MinutosProductivos
        {
            get { return tiempoProductivo; }
            set
            {
                tiempoProductivo = value;
            }
        }
        public int MinutosDescanso
        {
            get { return tiempoDescanso; }
            set
            {
                tiempoDescanso = value;
            }
        }

        public int EstadoBloque
        {
            get { return estado; }
            set { 
                if(value >= 0 && value <= 3)
                { 
                    estado = value;
                   
                }
            }
        }
        

        //Constructores
        public BloquesTiempo()
        {
            ProductivoCumplido = false;
            DescansoCumplido = false;
            MinutosProductivos = 30;
            MinutosDescanso = 10;
            BloquesCumplidos = 0;
        }

        public BloquesTiempo(int tiempoProductivo, int tiempoDescanso)
        {
            MinutosProductivos = tiempoProductivo;
            MinutosDescanso = tiempoDescanso;
            ProductivoCumplido = false;
            DescansoCumplido = false;
 
        }

        public void TiempoCumplido()
        {
            if(ProductivoCumplido)
            {
                DescansoCumplido = true;
                BloquesCumplidos += 1;
            }
            else
            {
                ProductivoCumplido = true;
            }
        }

        public void ResetBloque()
        {
            ProductivoCumplido = false;
            DescansoCumplido = false;
        }

        private void OnPropertyChange(string propertyname = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

    }
}
