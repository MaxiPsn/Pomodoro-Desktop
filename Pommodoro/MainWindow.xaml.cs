using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Text.RegularExpressions;

namespace Pomodoro
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

            bloque = new BloquesTiempo();

            bloque.EstadoBloque = (int)Clases.Estado.Espera;
            

            //Seteo inicial del temporizador principal
            ModoAutomatico = true;
            temporizador = new Temporizador();
            
            temporizador.SetFunc(TiempoCumplido);


            //Seteo de propiedades del temporizador grafico
            TemporizadorGrafico = new TemporizadorGrafico();

            TemporizadorGrafico.TextBoxHora = Horas;
            TemporizadorGrafico.TextBoxMinuto = Minutos;
            TemporizadorGrafico.TextBoxSegundo = Segundos;

            Contador.DataContext = bloque;
            this.Dispatcher.Invoke(ActualizarLabelEstado);
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

                    temporizador.Segundos = 0;
                    temporizador.Minutos = bloque.MinutosProductivos;
                    temporizador.Horas = 0;

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

                temporizador.Segundos = 0;
                temporizador.Minutos = bloque.MinutosDescanso;
                temporizador.Horas = 0;

                temporizador.Start();
                this.Dispatcher.Invoke(TemporizadorGrafico.StartTemporizador);

                bloque.EstadoBloque = (int)Clases.Estado.Descanso;
                this.Dispatcher.Invoke(ActualizarLabelEstado);
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
                    Estado.Foreground = this.Resources["Estado_pausa"] as SolidColorBrush;
                    break;
                case (int)Clases.Estado.Espera:
                    Estado.Content = "En espera";
                    Estado.Foreground = this.Resources["Estado_espera"] as SolidColorBrush;
                    break;
            }
        }

        private bool EsValido(string input)
        {
            Regex regex = new Regex(@"^\d{1,4}");

            if (regex.IsMatch(input))
            {
                if (Int32.Parse(input) != 0)
                    return true;
            }

            return false;
        }

        //-----------------Eventos ui-------------------//


        //Inicia los temporizadores con los valores introducidos en los campos de texto.

        private void Comenzar_click(object sender, RoutedEventArgs e)
        {
            bool tiempoProductivoValido = EsValido(TiempoProductivomTextBox.Text);
            bool tiempoDescansoValido = EsValido(TiempoDescansomTextBox.Text);
            bloque.ResetBloque();
            if (temporizador.Enabled()) { temporizador.Stop(); }
            if (TemporizadorGrafico.IsEnabled()) { TemporizadorGrafico.StopTemporizador(); }

            if(tiempoProductivoValido && tiempoDescansoValido)
            {
                bloque.MinutosProductivos = Int32.Parse(TiempoProductivomTextBox.Text);
                bloque.MinutosDescanso = Int32.Parse(TiempoDescansomTextBox.Text);

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
                if (!bloque.ProductivoCumplido)
                {
                    temporizador.Minutos = bloque.MinutosProductivos;
                }
                else
                {
                    temporizador.Minutos = bloque.MinutosDescanso;
                }

            (sender as Button).IsEnabled = false;
                (sender as Button).Visibility = Visibility.Hidden;

                temporizador.Start();
                TemporizadorGrafico.StartTemporizador();

                bloque.EstadoBloque = (int)Clases.Estado.Productivo;
                this.Dispatcher.Invoke(ActualizarLabelEstado);

                PausaContBtn.IsEnabled = true;
                PausaContBtn.Visibility = Visibility.Visible;
                PausaContBtn.ToolTip = "Pausar el contador";

                DetenerBtn.IsEnabled = true;
                DetenerBtn.Visibility = Visibility.Visible;

                TiempoProductivomTextBox.IsEnabled = false;
                TiempoDescansomTextBox.IsEnabled = false;

            }
            else
            {
                if(!tiempoProductivoValido)
                {
                    ErrorProd.Visibility = Visibility.Visible;
                    PreviewLblTiempoProd.Content = "";
                }
                if(!tiempoDescansoValido)
                {
                    ErrorDesc.Visibility = Visibility.Visible;
                    PreviewLblTiempoDesc.Content = "";
                }
            }

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

        private void TextBoxPreviewInput(object sender, TextCompositionEventArgs e)
        {
            
            int c = Convert.ToInt32(Convert.ToChar(e.Text));

            if (c >= 48 && c <= 57) e.Handled = false;

            else e.Handled = true;
        }

        //Actualiza los labels que previsualizan el ajuste de tiempo de los textbox de input para descanso y productivo. 
        private void TextBoxTiempo_KeyUp(object sender, KeyEventArgs e)
        {
            if (ErrorProd.Visibility == Visibility.Visible) ErrorProd.Visibility = Visibility.Hidden;
            if (ErrorDesc.Visibility == Visibility.Visible) ErrorDesc.Visibility = Visibility.Hidden;
            TextBox tx = sender as TextBox;

            if(tx.Text.Length == 0)
            {
                if (tx.Name.Equals("TiempoProductivomTextBox"))
                {
                    PreviewLblTiempoProd.Content = "";
                }
                else if (tx.Name.Equals("TiempoDescansomTextBox"))
                {
                    PreviewLblTiempoDesc.Content = "";
                }

            }
            else
            {


                int mins = Int32.Parse(tx.Text);
                string contenido = "";
                int h = (int)Math.Floor((decimal)(mins / 60));

                if (h < 10) { contenido = "0" + h.ToString(); }
                else { contenido = h.ToString(); }

                if (mins % 60 < 10) { contenido += ":0" + mins % 60; }
                else { contenido += ":" + mins % 60; }

                if (tx.Name.Equals("TiempoProductivomTextBox"))
                {
                    PreviewLblTiempoProd.Content = contenido;
                }
                else if (tx.Name.Equals("TiempoDescansomTextBox"))
                {
                    PreviewLblTiempoDesc.Content = contenido;
                }

            }


        }

        private void Pausa_Click(object sender, RoutedEventArgs e)
        {
            if(temporizador.Enabled() && TemporizadorGrafico.IsEnabled())
            {
                temporizador.Stop();
                TemporizadorGrafico.StopTemporizador();
                PausaContBtn.Content = "Continuar";
                PausaContBtn.ToolTip = "Reanudar el contador";

                bloque.EstadoBloque = (int)Clases.Estado.Pausa;
                Dispatcher.Invoke(ActualizarLabelEstado);
            }
            else if (!temporizador.Enabled() && !TemporizadorGrafico.IsEnabled())
            {       
                PausaContBtn.Content = "Pausa";
                PausaContBtn.ToolTip = "Reanudar el contador";

                temporizador.Segundos = TemporizadorGrafico.Segundo;
                temporizador.Minutos = TemporizadorGrafico.Minuto;
                temporizador.Horas = TemporizadorGrafico.Hora;

                temporizador.Start();

                TemporizadorGrafico.Reanudar();

                if (bloque.ProductivoCumplido)
                {
                    bloque.EstadoBloque = (int)Clases.Estado.Descanso;
                }
                else
                {
                    bloque.EstadoBloque = (int)Clases.Estado.Productivo;
                }


                Dispatcher.Invoke(ActualizarLabelEstado);
            }

        }

        private void Detener_Click(object sender, RoutedEventArgs e)
        {
            if (temporizador.Enabled() && TemporizadorGrafico.IsEnabled())
            {
                temporizador.Stop();
                TemporizadorGrafico.StopTemporizador();
            }
                TemporizadorGrafico.TextBoxHora.Text = "00";
                TemporizadorGrafico.TextBoxMinuto.Text = "00";
                TemporizadorGrafico.TextBoxSegundo.Text = "00";

                TemporizadorGrafico.Hora = 0;
                TemporizadorGrafico.Minuto = 0;
                TemporizadorGrafico.Segundo = 0;

                bloque.ResetBloque();
                bloque.EstadoBloque = (int)Clases.Estado.Espera;
                this.Dispatcher.Invoke(ActualizarLabelEstado);

                PausaContBtn.IsEnabled = false;
                DetenerBtn.IsEnabled = false;

                PausaContBtn.Visibility = Visibility.Hidden;
                DetenerBtn.Visibility = Visibility.Hidden;

                ComenzarBtn.Visibility = Visibility.Visible;
                ComenzarBtn.IsEnabled = true;

                TiempoProductivomTextBox.IsEnabled = true;
                TiempoDescansomTextBox.IsEnabled = true;
            
            if(!PausaContBtn.Content.Equals("Pausa"))
            {
                PausaContBtn.Content = "Pausa";
            }

            
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }



    }
}
