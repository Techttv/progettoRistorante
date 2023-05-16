using progettoRistorante.Classes;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace progettoRistorante.Finestre.TelefonoPagine
{
    /// <summary>
    /// Interaction logic for Paga.xaml
    /// </summary>
    public partial class Paga : Page
    {
        public Frame frame;
        double totale = 0;
        public Paga(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (Piatto piatto in NuovoOrdine.tavolo.ordine)
            {
                switch (piatto.tipo)
                {
                    case 1:
                        lb_primi.Items.Add(piatto.desc);
                        break;
                    case 2:
                        lb_secondi.Items.Add(piatto.desc);
                        break;
                    case 4:
                        lb_bevande.Items.Add(piatto.desc);
                        break;
                    default:
                        break;
                }
                totale += piatto.prezzo;
            }

            lbl_totale.Content = totale + "€";
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            frame.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);


        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            frame.GoBack();
        }

       

        private void btn_avanti_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Pagamento(frame);
        }

        private void ricarica()
        {
            totale = 0;
            //Clear label
            lb_primi.Items.Clear();
            lb_secondi.Items.Clear();
            lb_bevande.Items.Clear();

            //aggiunge i piatti dell'ordine nelle listbox
            foreach (Piatto piatto in NuovoOrdine.tavolo.ordine)
            {
                switch (piatto.tipo)
                {
                    case 1:
                        lb_primi.Items.Add(piatto.desc);
                        break;
                    case 2:
                        lb_secondi.Items.Add(piatto.desc);
                        break;
                    case 4:
                        lb_bevande.Items.Add(piatto.desc);
                        break;
                    default:
                        break;
                }
                totale += piatto.prezzo;
            }
            lbl_totale.Content = totale + "€";
            //animazione di cambiamento
            // animazione(frame);
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            cmb_tavoli.Items.Clear();
            for (int i = 1; i <= MainWindow.tavoli.Count; i++)
            {
                if (MainWindow.tavoli.ElementAt(i - 1).ordine.Count == 0)
                {
                    cmb_tavoli.Items.Add(i.ToString());
                }
            }
        }

        private void cmb_tavoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            popup.Visibility = Visibility.Hidden;
            popup.IsEnabled = false;
            stack_ordine.Effect = null;
            stack_ordine.IsEnabled = true;
            ricarica();
        }
    }
}
