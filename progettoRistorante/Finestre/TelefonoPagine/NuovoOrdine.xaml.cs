using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace progettoRistorante.Finestre.TelefonoPagine
{
    /// <summary>
    /// Logica di interazione per NuovoOrdine.xaml
    /// </summary>
    public partial class NuovoOrdine : Page
    {
        public Frame frame;
        static public Tavolo tavolo=new Tavolo();
        int numeroPiatti=0;
        bool hasChanged_=true;
        public NuovoOrdine(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            caricaMenu();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            frame.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);

            lbl_numero_piatti.Content = tavolo.ordine.Count;
            numeroPiatti = tavolo.ordine.Count;
            cmb_tavoli.Items.Clear();
            for (int i = 1; i <= MainWindow.tavoli.Count; i++)
            {
                if (MainWindow.tavoli.ElementAt(i - 1).ordine.Count == 0)
                {
                    cmb_tavoli.Items.Add(i.ToString());
                }
            }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
        }

        private void caricaMenu()
        {
            foreach (PiattoMenu piatto in MainWindow.menu)
            {
                if (piatto.quantita > 0)
                {
                    if (piatto.tipo == 1)
                    {
                        lb_primi.Items.Add(piatto.desc);
                    }
                    if (piatto.tipo == 2)
                    {
                        lb_secondi.Items.Add(piatto.desc);
                    }
                    if (piatto.tipo == 4)
                    {
                        lb_bevande.Items.Add(piatto.desc);
                    }
                }
            }
        }


        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
           // btn_avanti.Foreground = (Brush);
           // btn_avanti.Background = (Brush)Application.Current.MainWindow.FindResource("Verde scuro");
            string piattoNome = "";
            if (lb_primi.SelectedIndex != -1)
            {
                piattoNome = (string)lb_primi.SelectedItem;

            }
            if (lb_secondi.SelectedIndex != -1)
            {
                piattoNome = (string)lb_secondi.SelectedItem;

            }
            if (lb_bevande.SelectedIndex != -1)
            {
                piattoNome = (string)lb_bevande.SelectedItem;
            }
            if (lb_primi.SelectedIndex == -1 && lb_secondi.SelectedIndex == -1 && lb_bevande.SelectedIndex == -1 || cmb_tavoli.SelectedIndex==-1)
            {
                return;
            }
            foreach (PiattoMenu piatto in MainWindow.menu)
            {
                if (piatto.desc.Equals(piattoNome)&&piatto.quantita>0)
                {
                    piatto.Disponibile();
                    tavolo.aggiungiPiatto(piatto);
                    btn_avanti.Visibility = Visibility.Visible;
                    btn_avanti.IsEnabled = true;
                    if (hasChanged_)
                    {
                        btn_avanti.Opacity = 0;

                        DoubleAnimation doubleAnimation = new DoubleAnimation();
                        doubleAnimation.From = 0;
                        doubleAnimation.To = 1;
                        doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                        doubleAnimation.AutoReverse = false;

                        btn_avanti.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
                        hasChanged_ = false;
                    }
                    piatto.quantita--;
                    lbl_ultimoAggiunto.Content = piattoNome;
                    numeroPiatti++;
                    lbl_numero_piatti.Content = numeroPiatti;
                }
            }

        }
           
        private void lb_dolci_GetFocus(object sender, RoutedEventArgs e)
        {
            btn_aggiungi.IsEnabled = true;
            lb_primi.SelectedIndex = -1;
            lb_secondi.SelectedIndex = -1;
        }

        private void lb_secondi_GetFocus(object sender, RoutedEventArgs e)
        {
            btn_aggiungi.IsEnabled = true;
            lb_bevande.SelectedIndex = -1;
            lb_primi.SelectedIndex = -1;
        }

        private void lb_primi_GetFocus(object sender, RoutedEventArgs e)
        {
            btn_aggiungi.IsEnabled = true;
            lb_bevande.SelectedIndex = -1;
            lb_secondi.SelectedIndex = -1;
        }

        private void cmb_tavoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            popup.Visibility = Visibility.Hidden;
            btn_aggiungi.IsEnabled = true;
            popup.IsEnabled = false;
            griglia_menu.Effect = null;
            griglia_menu.IsEnabled = true;
            if (cmb_tavoli.SelectedIndex != -1)
            {
                tavolo.numeroTavolo = int.Parse(cmb_tavoli.SelectedItem.ToString());
            }
            lbl_numero_piatti.Content = tavolo.ordine.Count;
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            frame.GoBack();
        }

        private void btn_avanti_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Riepilogo(frame);
        }


    }
}
