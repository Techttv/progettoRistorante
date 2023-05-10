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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace progettoRistorante.Finestre.TelefonoPagine
{
    /// <summary>
    /// Interaction logic for Riepilogo.xaml
    /// </summary>
    public partial class Riepilogo : Page
    {
        public Frame frame;
        double totale=0;
        public Riepilogo(Frame frame)
        {
            InitializeComponent();
            this.frame = frame; 
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            foreach(Piatto piatto in NuovoOrdine.tavolo.ordine)
            {
                switch (piatto.tipo){
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

            lbl_totale.Content = totale +"€";
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

        private void btn_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            string piattoNome = "";
            int[] selectIndex= { 0, 0 };
            if (lb_primi.SelectedIndex != -1)
            {
                selectIndex[0] = 1;
                selectIndex[1] = lb_primi.SelectedIndex;
                piattoNome = (string)lb_primi.SelectedItem;
                lb_primi.Items.Remove(lb_primi.SelectedItem);

            }
            if (lb_secondi.SelectedIndex != -1)
            {         
                selectIndex[0] = 2;
                selectIndex[1] = lb_secondi.SelectedIndex;
                piattoNome = (string)lb_secondi.SelectedItem;
                lb_secondi.Items.Remove(lb_secondi.SelectedItem);

            }
            if (lb_bevande.SelectedIndex != -1)
            {
                selectIndex[0] = 4;
                selectIndex[1] = lb_bevande.SelectedIndex;
                piattoNome = (string)lb_bevande.SelectedItem;
                lb_bevande.Items.Remove(lb_bevande.SelectedItem);

            }

            if (lb_primi.SelectedIndex != -1 && lb_secondi.SelectedIndex != -1 && lb_bevande.SelectedIndex != -1)
            {
                ricarica();
            }
            foreach (PiattoMenu piatto in MainWindow.menu)
            {
                if (piatto.desc.Equals(piattoNome))
                {
                    piatto.quantita++;
                    NuovoOrdine.tavolo.rimuoviPiatto(piatto);
                    totale -=piatto.prezzo;
                    ricarica();
                    switch(selectIndex[0])
                    {
                        case 1:
                            lb_primi.SelectedIndex = selectIndex[1];
                            break;
                        case 2:
                            lb_secondi.SelectedIndex = selectIndex[1];
                            break;
                        case 3:
                            lb_bevande.SelectedIndex= selectIndex[1];
                            break;  
                    }
                }

            }
            if (NuovoOrdine.tavolo.ordine.Count == 0)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 0;
                doubleAnimation.To = 1;
                doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                doubleAnimation.AutoReverse = false;

                btn_avanti.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);

                btn_avanti.Visibility = Visibility.Hidden;
                btn_avanti.IsEnabled = false;
            }
            lbl_totale.Content = totale+"€";
        }

        private void btn_avanti_Click(object sender, RoutedEventArgs e)
        {
            


            frame.Content = new ConfermaOrdine(frame,"provvisorio");
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
    }
}
