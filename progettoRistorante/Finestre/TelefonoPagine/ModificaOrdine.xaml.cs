using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Riepilogo.xaml
    /// </summary>
    public partial class ModificaOrdine : Page
    {
        public Frame frame;
        double totale=0;
        public static Tavolo tavolo = new Tavolo();
        private bool hasChanged_;
        private int numeroPiatti;
        public static bool ritorno;

        public ModificaOrdine(Frame frame)
        {
            InitializeComponent();
            popup.Visibility = Visibility.Visible;
            popup.IsEnabled = true;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 20;
            griglia_menu.Effect = blurEffect;
            griglia_menu.IsEnabled = false;
            btn_aggiungi.IsEnabled = false;
            btn_rimuovi.IsEnabled = false;
            this.frame = frame; 
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
        }
        private void cmb_tavoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_tavoli.SelectedIndex != -1)
            {
                tavolo.numeroTavolo = int.Parse(cmb_tavoli.SelectedItem.ToString());
            }
            ricarica();
            
        }

        private void ricarica()
        {
            totale = 0;
            //Clear label
            lb_primi.Items.Clear();
            lb_secondi.Items.Clear();
            lb_dolci.Items.Clear();
            lb_bevande.Items.Clear();

            //se la combo cambia e l'index è diverso da -1
            if (cmb_tavoli.SelectedIndex != -1)
            {
                //Toglie il popup, mostra l'ordine e abilita i bottoni
                popup.Visibility = Visibility.Hidden;
                popup.IsEnabled = false;
                griglia_menu.Effect = null;
                griglia_menu.IsEnabled = true;
                btn_aggiungi.IsEnabled = true;
                btn_rimuovi.IsEnabled = true;

                //copia il tavolo negli ordini già esistenti
                tavolo = MainWindow.tavoli.ElementAt(tavolo.numeroTavolo-1);
                tavolo.numeroTavolo = int.Parse((string)cmb_tavoli.SelectedItem);
                lbl_numero_piatti.Content = tavolo.ordine.Count;

                //aggiunge i piatti dell'ordine nelle listbox
                foreach (Piatto piatto in tavolo.ordine)
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
                numeroPiatti = tavolo.ordine.Count;
                //animazione di cambiamento
               // animazione(frame);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            animazione(frame);

            cmb_tavoli.Items.Clear();
            for (int i = 1; i <= MainWindow.tavoli.Count; i++)
            {
                if (MainWindow.tavoli.ElementAt(i - 1).ordine.Count > 0)
                {
                    cmb_tavoli.Items.Add(i.ToString());
                }
            }

            if (ritorno)
            {
                cmb_tavoli.SelectedItem = tavolo.numeroTavolo.ToString();
            }
            ricarica();
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            frame.GoBack();
        }

        private void btn_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            int[] selectedIndex = { 0,2};
            string piattoNome = "";
            if (lb_primi.SelectedIndex != -1)
            {
                selectedIndex[0] = 1;
                selectedIndex[1] = lb_primi.SelectedIndex;
                piattoNome = (string)lb_primi.SelectedItem;
                lb_primi.Items.Remove(lb_primi.SelectedItem);


            }
            if (lb_secondi.SelectedIndex != -1)
            {
                selectedIndex[0] = 2;
                selectedIndex[1] = lb_secondi.SelectedIndex;
                piattoNome = (string)lb_secondi.SelectedItem;
               lb_secondi.Items.Remove(lb_secondi.SelectedItem);

            }
            if (lb_dolci.SelectedIndex != -1)
            {
                selectedIndex[0] = 3;
                selectedIndex[1] = lb_dolci.SelectedIndex;
                piattoNome = (string)lb_dolci.SelectedItem;
                lb_dolci.Items.Remove(lb_dolci.SelectedItem);

            }
            if (lb_bevande.SelectedIndex != -1)
            {
                selectedIndex[0] = 4;
                selectedIndex[1] = lb_bevande.SelectedIndex;
                piattoNome = (string)lb_bevande.SelectedItem;
                lb_bevande.Items.Remove(lb_bevande.SelectedItem);

            }
            foreach (PiattoMenu piatto in MainWindow.menu)
            {
                if (piatto.desc.Equals(piattoNome))
                {
                    
                    foreach (Tavolo tavolo in MainWindow.tavoli)
                    {
                        if (tavolo.numeroTavolo == int.Parse(cmb_tavoli.SelectedItem.ToString()))
                        {
                            piatto.quantita++;
                            tavolo.ordine.Remove(piatto);
                            totale -= piatto.prezzo;
                            numeroPiatti--;
                            lbl_numero_piatti.Content = numeroPiatti;
                            ricarica();

                            switch (selectedIndex[0])
                            {
                                case 1:
                                    lb_primi.SelectedIndex = selectedIndex[1];
                                    break;
                                case 2:
                                    lb_secondi.SelectedIndex = selectedIndex[1];
                                    break;
                                case 3:
                                    lb_dolci.SelectedIndex = selectedIndex[1];
                                    break;
                                case 4:
                                    lb_bevande.SelectedIndex = selectedIndex[1];
                                    break;
                            }

                        }
                    }
                }
            }

            
        }

        private void btn_avanti_Click(object sender, RoutedEventArgs e)
        {
            NuovoOrdine.tavolo = tavolo;
            frame.Content = new ConfermaOrdine(frame,"provvisorio");
            tavolo= new Tavolo();
        }

        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            string nomepiatto = "";


            if (lb_primi.SelectedIndex != -1)
            {
                nomepiatto = lb_primi.SelectedItem.ToString();    
            }
            if (lb_secondi.SelectedIndex != -1)
            {
                nomepiatto = lb_secondi.SelectedItem.ToString();            
            }
            if (lb_dolci.SelectedIndex != -1)
            {
                nomepiatto = lb_dolci.SelectedItem.ToString();
            }
            if (lb_bevande.SelectedIndex != -1)
            {
                nomepiatto = lb_bevande.SelectedItem.ToString();
            }
            if(lb_primi.SelectedIndex != -1&& lb_secondi.SelectedIndex != -1&& lb_dolci.SelectedIndex != -1&& lb_bevande.SelectedIndex != -1)
            {
                ricarica();
            }

            foreach (PiattoMenu elemento in MainWindow.menu)
            {
                if (elemento.desc.Equals(nomepiatto)&&elemento.quantita>0)
                {
                    elemento.quantita--;
                    tavolo.ordine.Add(elemento);
                    numeroPiatti++;
                    btn_avanti.Visibility = Visibility.Visible;
                    btn_avanti.IsEnabled = true;
                    if (hasChanged_)
                    {
                        btn_avanti.Opacity = 0;
                        animazione(btn_avanti);
                        hasChanged_ = false;
                    }

                     if (lb_primi.SelectedIndex != -1)
                       {                      
                           lb_primi.Items.Add(lb_primi.SelectedItem);
                       }
                       if (lb_secondi.SelectedIndex != -1)
                       {             
                           lb_secondi.Items.Add(lb_secondi.SelectedItem);
                       }
                       if (lb_dolci.SelectedIndex != -1)
                       {
                           lb_dolci.Items.Add(lb_dolci.SelectedItem);
                       }
                       if (lb_bevande.SelectedIndex != -1)
                       {
                           lb_bevande.Items.Add(lb_bevande.SelectedItem);
                       }

                    lbl_numero_piatti.Content = numeroPiatti;
                }
            }

        }

        private void lb_dolci_GetFocus(object sender, RoutedEventArgs e)
        {

            lb_primi.SelectedIndex = -1;
            lb_secondi.SelectedIndex = -1;
            lb_bevande.SelectedIndex = -1;
        }

        private void lb_secondi_GetFocus(object sender, RoutedEventArgs e)
        {

            lb_dolci.SelectedIndex = -1;
            lb_primi.SelectedIndex = -1;
            lb_bevande.SelectedIndex = -1;
        }

        private void lb_primi_GetFocus(object sender, RoutedEventArgs e)
        {

            lb_dolci.SelectedIndex = -1;
            lb_secondi.SelectedIndex = -1;
            lb_bevande.SelectedIndex = -1;
        }

        private void lb_bevande_GetFocus(object sender, RoutedEventArgs e)
        {
            lb_primi.SelectedIndex = -1;
            lb_secondi.SelectedIndex = -1;
            lb_dolci.SelectedIndex = -1;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            popupCibo.IsOpen = true;
        }

        private void btn_cibo_MouseLeave(object sender, MouseEventArgs e)
        {
            popupCibo.IsOpen = false;
        }

        private void btn_cibo_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new ModificaNuovoOrdine(frame);
            ModificaNuovoOrdine.tavolo=tavolo;
        }

        public void animazione(UIElement element)
        {

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            element.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
        }
    }


}
