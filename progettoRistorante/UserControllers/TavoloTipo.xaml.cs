using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using progettoRistorante.Classes;
using progettoRistorante.Finestre;

namespace progettoRistorante.UserControllers
{
    /// <summary>
    /// Interaction logic for TavoloTipo.xaml
    /// </summary>
    public partial class TavoloTipo : UserControl
    {
        public DispatcherTimer timer;
        int numeroTavolo;
        public TavoloTipo()
        {
            InitializeComponent();
        }

        public void cambiaTavoloNumero(int numero)
        {
            if (numero == -1)
            {
                
                lbl_numeroTavolo.Content = "";
                grid.RowDefinitions.RemoveAt(1);
                lbl_numeroTavolo.Visibility = Visibility.Hidden;
                btn_skip.Visibility = Visibility.Hidden;
            }
            else {
                numeroTavolo = numero;
                lbl_numeroTavolo.Content = "Tavolo n°" + numero; }

        }

        public void bottone(bool enabled)
        {
            btn_skip.IsEnabled = enabled;
        }

        public void cambiaTipo(int numero)
        {
            String s;
            switch (numero)
            {
                case 1:
                    s = "• Primo";
                    break;
                case 2:
                    s = "• Secondo";
                    break;
                case 3:
                    s = "• Dolci";
                    break;
                default:
                    s = "";
                    grid.RowDefinitions.RemoveAt(0);
                    lbl_tipoPasto.Visibility = Visibility.Hidden;
                    break;
            }

            lbl_tipoPasto.Content = s;
        }
        public void coloreVerde()
        {
            lbl_numeroTavolo.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#609966"));
            lbl_tipoPasto.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#609966"));
        }

        private void btn_skip_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            foreach(Tavolo tavolo in MainWindow.tavoli)
            {
                if(tavolo.numeroTavolo== numeroTavolo)
                {
                    tavolo.skip();
                }
            }
        }

    }
}
