using System;
using System.Diagnostics;
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
    public partial class ModificaNuovoOrdine : Page
    {
        public Frame frame;
        static public Tavolo tavolo=new Tavolo();
        int numeroPiatti=0;
        bool hasChanged_=true;
        public ModificaNuovoOrdine(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            caricaMenu();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //animazione di entrata
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            frame.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);

            Trace.WriteLine(tavolo.numeroTavolo);

            //La label col numero dei piatti è uguale ai piatti al suo interno che però dovrebbe essere vuoto
            lbl_numero_piatti.Content = tavolo.ordine.Count;
            numeroPiatti = tavolo.ordine.Count;
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
                    }
                }
            }
        }


        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
           // btn_avanti.Foreground = (Brush);
           // btn_avanti.Background = (Brush)Application.Current.MainWindow.FindResource("Verde scuro");
            string piattoNome = "";
            int[] selectedIndex = { 0, 2 };
            if (lb_primi.SelectedIndex != -1)
            {
                selectedIndex[0] = 1;
                selectedIndex[1] = lb_primi.SelectedIndex;
                piattoNome = (string)lb_primi.SelectedItem;

            }
            if (lb_secondi.SelectedIndex != -1)
            {
                selectedIndex[0] = 2;
                selectedIndex[1] = lb_secondi.SelectedIndex;
                piattoNome = (string)lb_secondi.SelectedItem;

            }
            if (lb_bevande.SelectedIndex != -1)
            {
                selectedIndex[0] = 4;
                selectedIndex[1] = lb_bevande.SelectedIndex;
                piattoNome = (string)lb_bevande.SelectedItem;

            }
            if (lb_dolci.SelectedIndex != -1)
            {
                selectedIndex[0] = 3;
                selectedIndex[1] = lb_dolci.SelectedIndex;
                piattoNome = (string)lb_dolci.SelectedItem;

            }
            foreach (PiattoMenu piatto in MainWindow.menu)
            {
                if (piatto.desc.Equals(piattoNome)&&piatto.quantita>0)
                {
                    numeroPiatti++;
                    piatto.quantita--;
                    lbl_numero_piatti.Content = numeroPiatti;
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
                    lbl_ultimoAggiunto.Content = piattoNome;
                    lbl_numero_piatti.Content = numeroPiatti;
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

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            frame.GoBack();
        }

        private void btn_avanti_Click(object sender, RoutedEventArgs e)
        {
            
            frame.GoBack();
            ModificaOrdine.ritorno = true;
            ModificaOrdine.tavolo = tavolo;
            tavolo = new Tavolo();
        }

    }
}
