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
using Pomodoro.Clases;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO : Cambiar textblocks para configurar tiempo para que solo reciban numeros y dividirlos en uno para hora y otro para minutos.
        
        BloquesTiempo bloque;
        Temporizador temporizador;
        TemporizadorGrafico TemporizadorGrafico;

        bool ModoAutomatico { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            bloque = new BloquesTiempo();

            bloque.EstadoBloque = (int)Clases.Estado.Espera;
            this.Dispatcher.Invoke(ActualizarLabelEstado);
            //Seteo inicial del temporizador principal
            ModoAutomatico = true;
            temporizador = new Temporizador();
            
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
                
                if (ModoAutomatico)
                {                    
                    temporizador.Minutos = bloque.MinutosProductivos;

                    if (bloque.MinutosProductivos > 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosProductivos / 60)); }
                    TemporizadorGrafico.Minuto = bloque.MinutosProductivos % 60;

                    temporizador.Start();
                    this.Dispatcher.Invoke(TemporizadorGrafico.StartTemporizador);

                    bloque.EstadoBloque = (int)Clases.Estado.Productivo;
                    this.Dispatcher.Invoke(ActualizarLabelEstado);
                }
            }
            else if(bloque.ProductivoCumplido) //Si solo esta cumplido TiempoProductivo se inicia temporizador con TiempoDescanso
            {
                temporizador.Minutos = bloque.MinutosDescanso;

                if (bloque.MinutosDescanso > 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosDescanso / 60)); }
                TemporizadorGrafico.Minuto = bloque.MinutosDescanso % 60;

                temporizador.Start();
                this.Dispatcher.Invoke(TemporizadorGrafico.StartTemporizador);
                bloque.EstadoBloque = (int)Clases.Estado.Descanso;
                this.Dispatcher.Invoke(ActualizarLabelEstado);
            }

        }

        //Inicia los temporizadores con los valores introducidos en los campos de texto.
        private void Comenzar_click(object sender, RoutedEventArgs e)
        {
            bloque.ResetBloque();
            if (temporizador.timer.Enabled) { temporizador.Stop(); }
            if (TemporizadorGrafico.IsEnabled()) { TemporizadorGrafico.StopTemporizador(); }
            
            bloque.MinutosProductivos = Int32.Parse(TiempoProductivohTextBox.Text) * 60 + Int32.Parse(TiempoProductivomTextBox.Text);
           
            bloque.MinutosDescanso = Int32.Parse(TiempoDescansohTextBox.Text) * 60 + Int32.Parse(TiempoDescansomTextBox.Text);
            

            //Setup hora temporizador grafico
            if (!bloque.ProductivoCumplido)
            {
                if (bloque.MinutosProductivos >= 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosProductivos / 60)); }
                else { TemporizadorGrafico.Hora = 0; }

                TemporizadorGrafico.Minuto = bloque.MinutosProductivos % 60;
            }
            else
            {
                if (bloque.MinutosDescanso >= 60) { TemporizadorGrafico.Hora = (int)Math.Floor((decimal)(bloque.MinutosDescanso / 60)); }
                else { TemporizadorGrafico.Hora = 0; }

                TemporizadorGrafico.Minuto = bloque.MinutosDescanso % 60;
            }

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
            bloque.EstadoBloque = (int)Clases.Estado.Productivo;
            this.Dispatcher.Invoke(ActualizarLabelEstado);
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (RbAuto.IsChecked == true)
            {
                ModoAutomatico = true;
            }
            else if(RbManual.IsChecked == true)
            {
                ModoAutomatico = false;
            }
        }

        private void ActualizarLabelEstado()
        {
            switch (bloque.EstadoBloque)
            {
                case (int)Clases.Estado.Productivo:
                    Estado.Content = "Tiempo productivo";
                    Estado.Foreground = this.Resources["Estado_productivo"] as SolidColorBrush;
                    break;
                case (int)Clases.Estado.Descanso:
                    Estado.Content = "Tiempo de descanso";
                    Estado.Foreground = this.Resources["Estado_descanso"] as SolidColorBrush;
                    break;
                case (int)Clases.Estado.Pausa:
                    Estado.Content = "En pausa";
                    break;
                case (int)Clases.Estado.Espera:
                    Estado.Content = "";
                    break;
            }
        }

        private void TextBoxPreviewInput(object sender, TextCompositionEventArgs e)
        {
            TextBox s = sender as TextBox;
            if (e.Text.Length > 2) e.Handled = false;

            int c = Convert.ToInt32(Convert.ToChar(e.Text));

            if (c >= 48 && c <= 57) e.Handled = false;

            else e.Handled = true;
        }

    }
}
