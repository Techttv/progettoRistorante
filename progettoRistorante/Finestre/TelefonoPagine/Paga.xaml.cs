using progettoRistorante.Classes;
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
using System.Windows.Media.Effects;
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

        public static Tavolo tavolo= new Tavolo();
        public Frame frame;
        double totale = 0;
        public Paga(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

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
            if (cmb_tavoli.SelectedIndex != -1)
            {
                foreach (Piatto piatto in MainWindow.tavoli.ElementAt(int.Parse(cmb_tavoli.SelectedItem.ToString()) - 1).ordine)
                {
                    tavolo.aggiungiPiatto(piatto);
                }
                tavolo.numeroTavolo = MainWindow.tavoli.ElementAt(int.Parse(cmb_tavoli.SelectedItem.ToString()) - 1).numeroTavolo;


                frame.Content = new Pagamento(frame);
            }   
        }

        private void ricarica()
        {
            if (cmb_tavoli.SelectedIndex > -1)
            {
                totale = 0;
                //Clear label
                lb_primi.Items.Clear();
                lb_dolci.Items.Clear();
                lb_secondi.Items.Clear();
                lb_bevande.Items.Clear();
                //aggiunge i piatti dell'ordine nelle listbox
                foreach (Piatto piatto in MainWindow.tavoli.ElementAt(int.Parse(cmb_tavoli.SelectedItem.ToString())-1).ordine)
                {
                    switch (piatto.tipo)
                    {
                        case 1:
                            lb_primi.Items.Add(piatto.desc);
                            break;
                        case 2:
                            lb_secondi.Items.Add(piatto.desc);
                            break;
                        case 3:
                            lb_dolci.Items.Add(piatto.desc);
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

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            cmb_tavoli.Items.Clear();
            for (int i = 1; i <= MainWindow.tavoli.Count; i++)
            {
                if (MainWindow.tavoli.ElementAt(i - 1).ordine.Count > 0)
                {
                    cmb_tavoli.Items.Add(i.ToString());
                }
            }
        }

        private void cmb_tavoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_tavoli.SelectedItem!=null)
            {
                btn_paga.IsEnabled = true;
                btn_paga.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Hidden;
                popup.IsEnabled = false;
                stack_ordine.Effect = null;
                stack_ordine.IsEnabled = true;
            }
            else
            {
                btn_paga.IsEnabled = false;
                btn_paga.Visibility = Visibility.Hidden;
                popup.Visibility = Visibility.Visible;
                BlurEffect blur = new BlurEffect();
                blur.Radius = 20;
                popup.IsEnabled = true;
                stack_ordine.Effect = blur;
                stack_ordine.IsEnabled = false;
            }
            ricarica();
        }
    }
}
