using CCStatusOrder;
using progettoRistorante.Classes;
using progettoRistorante.Finestre;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Timer = System.Timers.Timer;
using progettoRistorante.UserControllers;
using PdfSharp.Fonts;

namespace progettoRistorante
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<PiattoMenu> menu = new List<PiattoMenu>();
        public static List<Tavolo> tavoli = new List<Tavolo>();
        public bool open = false;
        public static StackPanel ordiniGrid, colonnaPrimi, colonnaSecondi, colonnaDolci, stack_fornelliMain;
        public static WrapPanel wraptavoli;
        private ModificaTavoli CCModificaTavoli;
        TelefonoOrdinazioni telefonoOrdinazioni;
        VistaCucina vistaCucina;

        public MainWindow()
        {
            GlobalFontSettings.FontResolver = new MyFontResolver();
            InitializeComponent();
            CCModificaTavoli = new ModificaTavoli(tavoli.Count, Home.Height, Home.Width) { f1 = this };

            CCModificaTavoli.Visibility = Visibility.Hidden;
            MainWindowGrid.Children.Add(CCModificaTavoli);

            vistaCucina = new VistaCucina();
            vistaCucina.Show();

            telefonoOrdinazioni = new TelefonoOrdinazioni();
            telefonoOrdinazioni.Show();
            caricaMenu();
            ordiniGrid = ordini_grid; colonnaPrimi = scroll_colonnaPrimi; colonnaSecondi = scroll_colonnaSecondi; colonnaDolci = scroll_colonnaDolci;
            wraptavoli = wrap_tavoli;
            stack_fornelliMain = stack_fornelli;
            ricarica();
        }

        private void Home_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void Home_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Status_grid.Height = this.Height - 230;
            ordini_grid.Height = this.Height - 290;
        }

        private void Home_Initialized(object sender, EventArgs e)
        {
            int nuovonumero = Properties.Settings.Default.NumeroTavoli;
            TavoloTipo TavoloTipo;
            CCTavoli.IconaTavolo userControl11Tavoli;
            Tavolo tavolo;
            for (int i = tavoli.Count + 1; i <= nuovonumero; i++)
            {
                tavolo = new Tavolo();

                userControl11Tavoli = new CCTavoli.IconaTavolo();
                userControl11Tavoli.cambiaNumero(i);
                tavolo.icona = userControl11Tavoli;
                tavolo.numeroTavolo = i;
                wrap_tavoli.Children.Add(userControl11Tavoli);
                TavoloTipo = new TavoloTipo();
                tavolo.numeroTavolo = i;
                TavoloTipo.cambiaTavoloNumero(i);
                TavoloTipo.cambiaTipo(-1);
                tavoli.Add(tavolo);
                ordini_grid.Children.Add(TavoloTipo);
            }
            

        }

        private void btn_ordini_Click(object sender, RoutedEventArgs e)
        {
            showOrdini();
        }

        private void btn_tavoli_Click(object sender, RoutedEventArgs e)
        {
            showTavoli();
        }

        private void rdb_ordini_Checked(object sender, RoutedEventArgs e)
        {
            showOrdini();
        }

        private void rdb_tavoli_Checked(object sender, RoutedEventArgs e)
        {
            showTavoli();
        }

        private void btn_moditavoli_Click(object sender, RoutedEventArgs e)
        {
            if (CCModificaTavoli.Visibility == Visibility.Hidden)
            {
                CCModificaTavoli.Visibility = Visibility.Visible;
            }
            else
            {
                CCModificaTavoli.Visibility = Visibility.Hidden;
            }

        }

        public void aggiungiTavoli(int nuovonumero)
        {
            Tavolo tavolo;

            CCTavoli.IconaTavolo userControl11Tavoli;
            TavoloTipo TavoloTipo;
            if (tavoli.Count < nuovonumero)
            {
                for (int i = tavoli.Count + 1; i <= nuovonumero; i++)
                {
                    tavolo = new Tavolo();

                    userControl11Tavoli = new CCTavoli.IconaTavolo();
                    userControl11Tavoli.cambiaNumero(i);
                    tavolo.icona = userControl11Tavoli;
                    tavolo.numeroTavolo = i;
                    wrap_tavoli.Children.Add(userControl11Tavoli);
                    TavoloTipo = new TavoloTipo();
                    TavoloTipo.cambiaTavoloNumero(i);
                    TavoloTipo.cambiaTipo(-1);
                    tavoli.Add(tavolo);
                    ordini_grid.Children.Add(TavoloTipo);
                }
            }
            else if (tavoli.Count > nuovonumero)
            {
                for (int i = tavoli.Count - 1; i >= nuovonumero; i--)
                {
                    tavoli.RemoveAt(i);
                    wrap_tavoli.Children.RemoveAt(i);
                    ordini_grid.Children.RemoveAt(i);
                }
            }

            Properties.Settings.Default.NumeroTavoli = nuovonumero;
            Properties.Settings.Default.Save();
        }

        public void showTavoli()
        {
            btn_ordini.Tag = "false";
            wrap_tavoli.Visibility = Visibility.Visible;
            btn_tavoli.Tag = "true";
            scroll_ordini.Visibility = Visibility.Hidden;
            rdb_ordini.IsChecked = false;
            rdb_tavoli.IsChecked = true;
        }

        public void showOrdini()
        {
            btn_tavoli.Tag = "false";
            btn_ordini.Tag = "true";
            rdb_ordini.IsChecked = true;
            rdb_tavoli.IsChecked = false;
            scroll_ordini.Visibility = Visibility.Visible;
            wrap_tavoli.Visibility = Visibility.Hidden;
        }

        private void btn_apri_Click(object sender, RoutedEventArgs e)
        {
            if(!vistaCucina.IsLoaded)
            {
                vistaCucina = new VistaCucina();
                vistaCucina.Show();
            }
            if (!telefonoOrdinazioni.IsLoaded)
            {
                telefonoOrdinazioni = new TelefonoOrdinazioni();
                telefonoOrdinazioni.Show();
            }
            
        }

        public void caricaMenu()
        {
            StreamReader sr = new StreamReader(File.OpenRead(@"menu.csv"));
            string line;
            PiattoMenu piatto;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] colonne = line.Split(';');
                piatto = new PiattoMenu(colonne[1], int.Parse(colonne[0]), int.Parse(colonne[3]), int.Parse(colonne[4]));
                piatto.quantita = int.Parse(colonne[5]);
                piatto.prezzo = double.Parse(colonne[2]);
                piatto.id = int.Parse(colonne[6]);
                menu.Add(piatto);

            }
            sr.Close();
        }
        public static void ricarica()
        {
            TavoloTipo tavoloTipo;
            piattoStatus piattoStatus;
            wraptavoli.Children.Clear();
            ordiniGrid.Children.Clear();
            colonnaPrimi.Children.Clear();
            colonnaSecondi.Children.Clear();
            colonnaDolci.Children.Clear();
            stack_fornelliMain.Children.Clear();
            foreach (Tavolo tavolo in tavoli)
            {
                tavoloTipo = new TavoloTipo();
                tavoloTipo.bottone(tavolo.canSkip);

                tavoloTipo.cambiaTavoloNumero(tavolo.numeroTavolo);
                tavoloTipo.cambiaTipo(-1);
                ordiniGrid.Children.Add(tavoloTipo);
                tavolo.icona.cambiaNumero(tavolo.numeroTavolo);
                wraptavoli.Children.Add(tavolo.icona);
                bool _primi = false, _secondi = false, _dolci = false;
                foreach (Piatto piatto in tavolo.ordine)
                {
                    if (piatto.Status == 0)
                    {
                        continue;
                    }
                    piattoStatus = new piattoStatus();
                    piattoStatus.CambiaTesto(piatto.desc);
                    switch (piatto.Status)
                    {
                        case 1:
                            piattoStatus.Pronto();
                            break;
                        case 2:
                            piattoStatus.InCorso();
                            break;
                        case 3:
                            piattoStatus.Disponibile();
                            break;
                    }

                    if (piatto.tipo == 1)
                    {
                        if (!_primi)
                        {
                            tavoloTipo = new TavoloTipo();
                            tavoloTipo.cambiaTipo(1);

                            tavoloTipo.cambiaTavoloNumero(-1);
                            _primi = true;
                            ordiniGrid.Children.Add(tavoloTipo);
                        }
                        ordiniGrid.Children.Add(piattoStatus);
                    }
                }
                foreach (Piatto piatto in tavolo.ordine)
                {
                    if (piatto.Status == 0)
                    {
                        continue;
                    }
                    piattoStatus = new piattoStatus();
                    piattoStatus.CambiaTesto(piatto.desc);
                    switch (piatto.Status)
                    {
                        case 1:
                            piattoStatus.Pronto();
                            break;
                        case 2:
                            piattoStatus.InCorso();
                            break;
                        case 3:
                            piattoStatus.Disponibile();
                            break;
                    }

                    if (piatto.tipo == 2)
                    {
                        if (!_secondi)
                        {
                            tavoloTipo = new TavoloTipo();
                            tavoloTipo.cambiaTipo(2);

                            tavoloTipo.cambiaTavoloNumero(-1);
                            _secondi = true;
                            ordiniGrid.Children.Add(tavoloTipo);
                        }
                        ordiniGrid.Children.Add(piattoStatus);
                    }
                }
                foreach (Piatto piatto in tavolo.ordine)
                {
                    if (piatto.Status == 0)
                    {
                        continue;
                    }
                    piattoStatus = new piattoStatus();
                    piattoStatus.CambiaTesto(piatto.desc);
                    switch (piatto.Status)
                    {
                        case 1:
                            piattoStatus.Pronto();
                            break;
                        case 2:
                            piattoStatus.InCorso();
                            break;
                        case 3:
                            piattoStatus.Disponibile();
                            break;
                    }

                    if (piatto.tipo == 3)
                    {
                        if (!_dolci)
                        {
                            tavoloTipo = new TavoloTipo();
                            tavoloTipo.cambiaTipo(3);

                            tavoloTipo.cambiaTavoloNumero(-1);
                            ordiniGrid.Children.Add(tavoloTipo);
                            _dolci = true;
                        }
                        ordiniGrid.Children.Add(piattoStatus);
                    }
                }
                

            }
            foreach (fornelloVista FornelloVista in VistaCucina.fornelli)
            {
                fornello fornelloNuovo = new fornello();
                switch (FornelloVista.status)
                {
                    case 0:
                        fornelloNuovo.Disponibile();
                        break;
                    case 1:
                        fornelloNuovo.inPreparazione();
                        break;
                    case 2:
                        fornelloNuovo.Finito();
                        break;
                }
                fornelloNuovo.cambiaDesc(FornelloVista.lbl_desc.Content.ToString());
                fornelloNuovo.cambiaTavolo(FornelloVista.tavRef);
                stack_fornelliMain.Children.Add(fornelloNuovo);
            }

            foreach (Tavolo tavolo1 in findTavoliOccupati())
            {
                tavoloTipo = new TavoloTipo();
                tavoloTipo.cambiaTipo(-1);

                tavoloTipo.cambiaTavoloNumero(tavolo1.numeroTavolo);
                tavoloTipo.coloreVerde();
                bool _primi = false; bool _secondi = false, _dolci = false;
                foreach (Piatto piatto in tavolo1.ordine)
                {
                    if (piatto.Status == 0)
                    {
                        continue;
                    }
                    piattoStatus = new piattoStatus();
                    piattoStatus.CambiaTesto(piatto.desc);
                    piattoStatus.coloreVerde();
                    switch (piatto.Status)
                    {
                        case 1:
                            piattoStatus.Pronto();
                            break;
                        case 2:
                            piattoStatus.InCorso();
                            break;
                        case 3:
                            piattoStatus.Disponibile();
                            break;
                    }

                    switch (piatto.tipo)
                    {
                        case 1:
                            if (!_primi)
                            {
                                tavoloTipo = new TavoloTipo();
                                tavoloTipo.cambiaTipo(-1);

                                tavoloTipo.cambiaTavoloNumero(tavolo1.numeroTavolo);
                                tavoloTipo.coloreVerde();
                                _primi = true;
                                colonnaPrimi.Children.Add(tavoloTipo);
                            }
                            colonnaPrimi.Children.Add(piattoStatus);

                            break;
                        case 2:
                            if (!_secondi)
                            {
                                tavoloTipo = new TavoloTipo();
                                tavoloTipo.cambiaTipo(-1);
                                tavoloTipo.cambiaTavoloNumero(tavolo1.numeroTavolo);
                                tavoloTipo.coloreVerde();
                                _secondi = true;
                                colonnaSecondi.Children.Add(tavoloTipo);
                            }
                            colonnaSecondi.Children.Add(piattoStatus);
                            break;
                        case 3:
                            if (!_dolci)
                            {
                                tavoloTipo = new TavoloTipo();
                                tavoloTipo.cambiaTipo(-1);

                                tavoloTipo.cambiaTavoloNumero(tavolo1.numeroTavolo);
                                tavoloTipo.coloreVerde();
                                _dolci = true;
                                colonnaDolci.Children.Add(tavoloTipo);
                            }
                            colonnaDolci.Children.Add(piattoStatus);
                            break;
                    }
                }
            }

        }

        private void Home_GotFocus(object sender, RoutedEventArgs e)
        {
            ricarica();
        }

        public static List<Tavolo> findTavoliOccupati()
        {
            List<Tavolo> tavolos = new List<Tavolo>();
            foreach (Tavolo tavolo in tavoli)
            {
                if (tavolo.ordine.Count > 0)
                {
                    tavolos.Add(tavolo);
                }
            }
            return tavolos;
        }

        
    }


}
