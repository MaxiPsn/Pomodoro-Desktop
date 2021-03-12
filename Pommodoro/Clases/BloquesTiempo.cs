using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Pommodoro.Clases
{
    ///Contiene los pares de tiempo productivo y de descanso, un contador de bloques terminados y las acciones a realizar.
    public class BloquesTiempo
    {
        public static int BloquesCumplidos;
        private int tiempoProductivo;
        private int tiempoDescanso;

        //Propiedades
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

        //Constructores
        public BloquesTiempo()
        {
            ProductivoCumplido = false;
            DescansoCumplido = false;
            MinutosProductivos = 30;
            MinutosDescanso = 10;
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



    }
}
