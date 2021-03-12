using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Pommodoro.Clases;

namespace Pommodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BloquesTiempo bloque;
        Temporizador temporizador;
        TemporizadorGrafico TemporizadorGrafico;
        bool ModoAutomatico { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            

            //Seteo inicial del temporizador principal
            ModoAutomatico = true;
            temporizador = new Temporizador();
            bloque = new BloquesTiempo();
            temporizador.SetFunc(TiempoCumplido);


            //Seteo de propiedades del temporizador grafico
            TemporizadorGrafico = new TemporizadorGrafico();

            TemporizadorGrafico.TextBoxHora = Horas;
            TemporizadorGrafico.TextBoxMinuto = Minutos;
            TemporizadorGrafico.TextBoxSegundo = Segundos;


        }

        private void TiempoCumplido()
        {
            bloque.TiempoCumplido();

            //Si se cumplio el bloque completo (propiedades ProductivoCumplido y DescansoCumplido = true) y modo auto = true, se reinicia el temporizador
            if (bloque.ProductivoCumplido && bloque.DescansoCumplido) 
            {
                bloque.ResetBloque();
                MessageBox.Show("Tiempo de descanso cumplido");
                if (ModoAutomatico)
                {                    
                    temporizador.Minutos = bloque.MinutosProductivos;

                    if (bloque.MinutosProductivos > 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosProductivos / 60)); }
                    TemporizadorGrafico.Minuto = bloque.MinutosProductivos % 60;

                    temporizador.Start();
                    TemporizadorGrafico.StartTemporizador();
                }
            }
            else if(bloque.ProductivoCumplido) //Si solo esta cumplido TiempoProductivo se inicia temporizador con TiempoDescanso
            {
                MessageBox.Show("Tiempo productivo cumplido");
                temporizador.Minutos = bloque.MinutosDescanso;

                if (bloque.MinutosDescanso > 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosDescanso / 60)); }
                TemporizadorGrafico.Minuto = bloque.MinutosDescanso % 60;

                temporizador.Start();
                TemporizadorGrafico.StartTemporizador();
            }

        }

        private void Comenzar_click(object sender, RoutedEventArgs e)
        {
            bloque.ResetBloque();
            if (temporizador.timer.Enabled) { temporizador.Stop(); }
            if (TemporizadorGrafico.IsEnabled()) { TemporizadorGrafico.StopTemporizador(); }
            

            bloque.MinutosProductivos = Int32.Parse(TiempoProductivoTextBox.Text);
            bloque.MinutosDescanso = Int32.Parse(TiempoDescansoTextBox.Text);


            //Setup hora temporizador grafico
            if (!bloque.ProductivoCumplido)
            {
                if (bloque.MinutosProductivos > 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosProductivos / 60)); }
                TemporizadorGrafico.Minuto = bloque.MinutosProductivos % 60;
            }
            else
            {
                if (bloque.MinutosDescanso > 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosDescanso / 60)); }
                TemporizadorGrafico.Minuto = bloque.MinutosDescanso % 60;
            }

            //MessageBox.Show("prod " + bloque.MinutosProductivos + " descanso " + bloque.MinutosDescanso);
            //Setup temporizador
            if(!bloque.ProductivoCumplido)
            {
                temporizador.Minutos = bloque.MinutosProductivos;
            }
            else
            {
                temporizador.Minutos = bloque.MinutosDescanso;
            }
            
            temporizador.Start();
            TemporizadorGrafico.StartTemporizador();
        }

    }
}
